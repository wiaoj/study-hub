---
title: Ambassador
category: Structural
language: tr
tag:
  - Decoupling
  - Cloud distributed
---

## Amaç

Müşteriye yardımcı bir hizmet örneği sağlamak ve ortak bir kaynaktan ortak işlevselliği dışarıya taşımak.

## Diğer adları

* Sidecar

## Açıklama

Gerçek dünya örneği

> Uzak bir hizmet, birçok istemcinin eriştiği bir işlev sağlamaktadır. Hizmet, eski bir uygulama olup güncellenmesi imkansızdır. 
> Kullanıcıların büyük sayıda isteği bağlantı sorunlarına neden olmaktadır. İstek sıklığı için yeni kurallar, gecikme kontrolü ve 
> istemci tarafı günlükleme ile birlikte uygulanmalıdır.

Basit bir ifadeyle

> `Ambassador` deseni ile istemcilerden daha az sıklıkta anket yapabilir, gecikme kontrolü ve günlükleme yapabiliriz.

Microsoft belgelerine göre

> Bir `Ambassador` hizmeti, istemciyle birlikte çalışan bir dış süreçli proxy olarak düşünülebilir. Bu desen, dil bağımsız bir şekilde 
> izleme, günlükleme, yönlendirme, güvenlik (TLS gibi) ve esneklik desenleri gibi ortak istemci bağlantı görevlerini dışarıya 
> taşımak için kullanışlı olabilir. Genellikle, değiştirilmesi zor olan eski uygulamalarla veya diğer uygulamalarla birlikte 
> kullanılarak ağ yeteneklerini genişletmek için kullanılır. Ayrıca, özel bir ekibin bu özellikleri uygulamasına olanak sağlayabilir.

**Programlama Örneği**

Yukarıdaki tanıtımı göz önünde bulundurarak, bu örnekteki işlevselliği taklit edeceğiz. Uzak hizmet tarafından uygulanan bir arayüz 
ve `Ambassador` hizmeti bulunmaktadır:

```csharp
public interface IRemoteService {
    Int64 DoRemoteFunction(Int32 value);
}
```

Bir tekil nesne olarak temsil edilen uzak hizmetler.

```csharp
public class RemoteService : IRemoteService {
    private const Int32 Threshold = 200;
    private readonly RandomProvider randomProvider;
    private static readonly Lazy<RemoteService> service = new(() => new RemoteService(), true);
    public static RemoteService Service => service.Value;

    private RemoteService() : this(() => Random.Shared.NextDouble()) { }

    public RemoteService(RandomProvider randomProvider) {
        this.randomProvider = randomProvider;
    }

    public Int64 DoRemoteFunction(Int32 value) {
        Int64 waitTime = (Int64)Math.Floor(this.randomProvider() * 1000);

        try {
            Thread.Sleep((Int32)waitTime);
        }
        catch(ThreadInterruptedException) {
            Thread.CurrentThread.Interrupt();
        }
        return waitTime <= Threshold ? value * 10 : RemoteServiceStatus.Failure.StatusCode();
    }
}
```

Günlükleme, gecikme kontrolü gibi ek özellikler ekleyen bir hizmet `Ambassadorsi`

```csharp
public class ServiceAmbassador : IRemoteService {
    private const Int32 RETRIES = 3;
    private const Int32 DELAY_MS = 3000;

    public Int64 DoRemoteFunction(Int32 value) {
        return SafeCall(value);
    }

    private Int64 SafeCall(Int32 value) {
        Int32 retries = 0;
        Int64 result = RemoteServiceStatus.Failure.StatusCode();

        for(Int32 i = 0; i < RETRIES; i++) {
            if(retries >= RETRIES) {
                return RemoteServiceStatus.Failure.StatusCode();
            }

            if((result = CheckLatency(value)) == RemoteServiceStatus.Failure.StatusCode()) {

                retries++;

                try {
                    Thread.Sleep(DELAY_MS);
                }
                catch(ThreadInterruptedException) {
                    Thread.CurrentThread.Interrupt();
                }
            }
            else {
                break;
            }
        }

        return result;
    }

    private static Int64 CheckLatency(Int32 value) {
        Stopwatch stopwatch = Stopwatch.StartNew();
        Int64 result = RemoteService.Service.DoRemoteFunction(value);
        stopwatch.Stop();

        Int64 timeTaken = stopwatch.ElapsedMilliseconds;

        return result;
    }
}
```

Bir istemcinin, uzak hizmetle etkileşimde bulunmak için yerel bir hizmet `Ambassadorsi` bulunur:

```csharp
public class Client {
    private readonly ServiceAmbassador serviceAmbassador = new();

    public Int64 UseService(Int32 value) {
        Int64 result = this.serviceAmbassador.DoRemoteFunction(value);
        return result;
    }
}
```

Here are two clients using the service.

```csharp
    Client host1 = new();
    Client host2 = new();
    host1.useService(12);
    host2.useService(73);
```

Here's the output for running the example:

```csharp
Time taken (ms): 111
Service result: 120
Time taken (ms): 931
Failed to reach remote: (1)
Time taken (ms): 665
Failed to reach remote: (2)
Time taken (ms): 538
Failed to reach remote: (3)
Service result: -1
```

## Class diagram

"-"

## Uygulanabilirlik

* Bulut Tabanlı ve Mikro Hizmet Mimarileri: Özellikle izlemek, günlük tutmak ve hizmetler arası iletişimi güvence altına almak önemli olduğu dağıtık sistemlerde kullanışlıdır.
* Eski Sistem Entegrasyonu: Gerekli ancak çekirdek işlevlere ait olmayan işlevleri ele alarak yeni hizmetlerle iletişimi kolaylaştırır.
* Performans Geliştirme: Sonuçları önbelleğe almak veya iletişim verimliliğini artırmak için veriyi sıkıştırmak için kullanılabilir.

Tipik kullanım durumları şunları içerir:

* Başka bir nesneye erişimi kontrol etmek
* Günlükleme uygulamak
* Devre kesme uygulamak
* Uzak hizmet görevlerini dışarıya aktarmak
* Ağ bağlantısını kolaylaştırmak

## Sonuçlar

Faydalar:

* Sorumlulukların Ayrıştırılması: Hizmet mantığından ayrı olarak, kesimler arası endişeleri dışarıya aktararak daha temiz ve bakımı daha kolay bir kod elde edilir.
* Yeniden Kullanılabilir Altyapı Mantığı: `Ambassador` deseni, aynı mantığın (örneğin, günlükleme, izleme) birden çok hizmette yeniden kullanılmasını sağlar.
* İyileştirilmiş Güvenlik: SSL sonlandırma veya kimlik doğrulama gibi güvenlik özelliklerini merkezileştirerek yapılandırma hatalarının riskini azaltır.
* Esneklik: Altyapı endişelerini güncelleme veya değiştirme ihtiyacı duyulduğunda hizmet kodunu değiştirmek daha kolay hale gelir.

Ticaret-off'lar:

* Artan Karmaşıklık: Sisteme başka bir katman ekler, bu da sistem tasarımını ve hata ayıklamayı karmaşıklaştırabilir.
* Potansiyel Performans Etkisi: Ek ağ geçidi, özellikle optimize edilmediyse gecikme ve performans etkisi getirebilir.
* Dağıtım Gereksinimleri: `Ambassador` hizmetlerinin dağıtımı ve ölçeklendirilmesi için ek kaynaklar ve yönetim gerektirir.

## Bilinen Kullanımlar

* Servis Ağı Uygulamaları: Istio veya Linkerd gibi bir servis ağı mimarisinde, `Ambassador` deseni genellikle hizmetler arası iletişimi yöneten bir yan konteyner proxy olarak kullanılır. Bu, hizmet keşfi, yönlendirme, yük dengeleme, telemetri (metrikler ve izleme) ve güvenlik (kimlik doğrulama ve yetkilendirme) gibi görevleri içerir.
* API Geçitleri: API geçitleri, `Ambassador` desenini kullanarak hız sınırlama, önbelleğe alma, istek şekillendirme ve kimlik doğrulama gibi ortak işlevleri kapsayabilir. Bu, arka uç hizmetlerin bu kesen konularla uğraşmadan çekirdek iş mantığına odaklanmasını sağlar.
* Günlükleme ve İzleme: Bir `Ambassador`, çeşitli hizmetlerden gelen günlükleri ve metrikleri toplayabilir ve bunları Prometheus veya ELK Stack (Elasticsearch, Logstash, Kibana) gibi merkezi izleme araçlarına iletebilir. Bu, her hizmet için günlükleme ve izleme kurulumunu basitleştirir ve sistemin sağlığına birleşik bir görünüm sağlar.
* Güvenlik: SSL/TLS sonlandırma, kimlik doğrulama ve şifreleme gibi güvenlikle ilgili işlevler bir `Ambassador` tarafından yönetilebilir. Bu, hizmetler arasında tutarlı güvenlik uygulamalarını sağlar ve yanlış yapılandırmalardan kaynaklanan güvenlik ihlallerinin olasılığını azaltır.
* Dayanıklılık: `Ambassador`, devre kesiciler, yeniden denemeler ve zaman aşımı gibi dayanıklılık desenlerini uygulayabilir. Örneğin, Netflix'in Hystrix kütüphanesi, mikro hizmetler ekosisteminde kademeli hataları önlemek için bir `Ambassador` içinde kullanılabilir.
* Veritabanı Proxy: `Ambassadorler`, veritabanı bağlantıları için bağlantı havuzu, replikalar için okuma/yazma ayrıştırma ve sorgu önbelleğe alma gibi işlevleri sağlayarak önemli karmaşıklığı uygulama hizmetlerinden uzaklaştırır.
* Eski Sistem Entegrasyonu: Modern mikro hizmetlerin eski sistemlerle iletişim kurması gereken senaryolarda, bir `Ambassador`, protokolleri çeviren, gerekli dönüşümleri yöneten ve modern güvenlik uygulamalarını uygulayan bir aracı olarak hizmet verebilir, entegrasyon sürecini kolaylaştırır.
* Ağ Optimizasyonu: Farklı coğrafi konumlara veya bulut bölgelerine dağıtılan hizmetler için `Ambassadorler`, veriyi sıkıştırma, istekleri toplama veya hatta gecikmeyi ve maliyetleri azaltmak için akıllı yönlendirme uygulama gibi iletişimi optimize edebilir.
* [Kubernetes-native API gateway for microservices](https://github.com/datawire/ambassador)

## Related patterns

* [Proxy](../proxy/): Shares similarities with the proxy pattern, but the ambassador pattern specifically focuses on offloading ancillary functionalities.
* Sidecar: A similar pattern used in the context of containerized applications, where a sidecar container provides additional functionality to the main application container.
* [Decorator](https://csharp-design-patterns.com/patterns/decorator/): The decorator pattern is used to add functionality to an object dynamically, while the ambassador pattern is used to offload functionality to a separate object.