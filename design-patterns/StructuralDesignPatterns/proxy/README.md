---
title: Proxy
category: Structural
language: tr
tag:
 - Gang Of Four
 - Decoupling
---

## Diğer adıyla

Surrogate

## Amaç

Başka bir nesnenin yerine geçen veya ona erişimi kontrol eden bir yer tutucu sağlamak.

## Açıklama

Gerçek dünya örneği

> Büyücülerin büyüleri öğrenmek için gittikleri bir kule hayal edin. İvory kulesine sadece ilk üç büyücü 
> girebilir. Kuleye sadece bu işlevi temsil eden bir vekil aracılığıyla erişilebilir. 

Basit bir ifadeyle

> Vekil deseni kullanılarak bir sınıf, başka bir sınıfın işlevselliğini temsil eder.

Wikipedia diyor ki

> Bir vekil, en genel anlamıyla, başka bir şeye arayüz olarak işlev gören bir sınıftır. 
> Bir vekil, istemci tarafından arka planda gerçek hizmet veren nesneye erişmek için çağrılan bir 
> sarmalayıcı veya aracı nesnedir. Vekil, gerçek nesneye yönlendirme yapabilir veya ek mantık sağlayabilir. 
> Vekil, örneğin gerçek nesne üzerindeki işlemler kaynak yoğun olduğunda önbellekleme yapabilir veya 
> gerçek nesne üzerindeki işlemlerden önce önkoşulları kontrol edebilir.

**Programatik Örnek**

Yukarıdaki büyücü kulesi örneğimizi ele alalım. İlk olarak `WizardTower` arayüzü ve 
`IvoryTower` sınıfımız var.

Ardından basit bir `Wizard` sınıfı gelir.

```csharp
public class Wizard {
    private readonly String name;
    public String Name => this.name;
    public Wizard(String name) {
        this.name = name;
    }

    public override String ToString() {
        return this.name;
    }
}
```

Ardından `WizardTowerProxy` sınıfı gelir, `WizardTower` üzerinde erişim kontrolü eklemek için kullanılır.

```csharp
public class WizardTowerProxy : IWizardTower {
    private const Int32 NUM_WIZARDS_ALLOWED = 3;
    private Int32 numWizards;
    private readonly IWizardTower tower;

    public WizardTowerProxy(IWizardTower tower) {
        this.tower = tower;
    }


    public void Enter(Wizard wizard) {
        if(!CanAddMoreWizards()) {
            Logger.Information("{0} is not allowed to enter!", wizard);
            return;
        }

        this.tower.Enter(wizard);
        this.numWizards++;
    }
}
```

Ve işte kuleye giriş senaryosu.

```csharp
WizardTowerProxy proxy = new(new IvoryTower());
proxy.enter(new Wizard("Red wizard"));
proxy.enter(new Wizard("White wizard"));
proxy.enter(new Wizard("Black wizard"));
proxy.enter(new Wizard("Green wizard"));
proxy.enter(new Wizard("Brown wizard"));
```

Program output:

```
Red wizard enters the tower.
White wizard enters the tower.
Black wizard enters the tower.
Green wizard is not allowed to enter!
Brown wizard is not allowed to enter!
```

## Class diagram

"-"

## Uygulanabilirlik

Proxy deseni, basit bir işaretçiden daha esnek veya sofistike bir referansa ihtiyaç duyulduğunda uygundur. İşte Proxy deseninin uygulanabileceği birkaç yaygın durum:

* Uzak proxy, farklı bir adres alanındaki bir nesne için yerel bir temsilci sağlar.
* Sanal proxy, maliyetli nesneleri talep üzerine oluşturur.
* Koruma proxy, orijinal nesneye erişimi kontrol eder. Koruma proxy'leri, nesnelerin farklı erişim haklarına sahip olması gerektiğinde kullanışlıdır.

Genellikle, proxy deseni şunlar için kullanılır:

* Başka bir nesneye erişimi kontrol etmek
* Tembel başlatma (lazy initialization)
* Günlükleme (logging) uygulamak
* Ağ bağlantısını kolaylaştırmak
* Bir nesnenin referans sayısını saymak

## İlgili tasarımlar

* [Ambassador](../ambassador/)