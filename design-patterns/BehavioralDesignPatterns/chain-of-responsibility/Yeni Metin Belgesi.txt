---
title: Chain of responsibility
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Amaç

Bir isteğin göndericisini alıcısıyla bağlantısını koparmak için isteği işleyecek birden fazla nesneye şans vererek isteği zincirleme olarak işlemektir. İstek zinciri boyunca isteği işleyecek bir nesne bulana kadar istek nesneleri zincir boyunca geçer.

## Açıklama

Gerçek dünya örneği

> Orc Kralı ordusuna yüksek sesle emirler verir. En yakın tepki veren komutan, ardından bir subay ve ardından bir askerdir. Komutan, subay ve asker bir sorumluluk zinciri oluşturur.

Basit bir ifadeyle

> Nesnelerin bir zincirini oluşturmaya yardımcı olur. Bir istek bir uçtan girer ve uygun bir işleyici bulana kadar bir nesneden diğerine geçer.

Wikipedia diyor ki

> Nesne tabanlı tasarımda, sorumluluk zinciri deseni, komut nesnelerinin bir kaynağı ve bir dizi işleme nesnesinden oluşan bir tasarım desenidir. Her işleme nesnesi, işleyebileceği komut nesnelerinin türlerini tanımlayan mantığı içerir; geri kalanlar zincirdeki bir sonraki işleme nesnesine iletilir.

**Programatik Örnek**

Yukarıdaki orklarla olan örneğimizi çevirerek. İlk olarak, `Request` sınıfımızı ele alalım:

```csharp
public class Request {
    private readonly RequestType requestType;
    private readonly String requestDescription;
    private Boolean handled;

    public String RequestDescription => this.requestDescription;
    public RequestType RequestType => this.requestType;
    public Boolean IsHandled => this.handled;

    public Request(RequestType requestType, String requestDescription) {
        this.requestType = requestType;
        this.requestDescription = requestDescription;
    }

    public void MarkHandled() {
        this.handled = true;
    }

    public override String ToString() {
        return this.RequestDescription;
    }
}

public enum RequestType {
    DefendCastle,
    TorturePrisoner,
    CollectTax
}
```

Daha sonra, istek işleyici hiyerarşisini gösteriyoruz.

```csharp
public interface IRequestHandler {
    Int32 Priority { get; }
    String Name { get; }
    Boolean CanHandleRequest(Request request);
    void Handle(Request request);
}

public class OrcCommander : IRequestHandler {
    public Int32 Priority => 2;
    public String Name => "Orc Commander";

    public Boolean CanHandleRequest(Request request) {
        return request.RequestType == RequestType.DefendCastle;
    }

    public void Handle(Request request) {
        request.MarkHandled();
    }
}

// OrcOfficer and OrcSoldier are defined similarly as OrcCommander

```

Orc Kralı emirleri verir ve zinciri oluşturur.

```csharp
public class OrcKing {
    private readonly List<IRequestHandler> handlers = [];

    public OrcKing() {
        BuildChain();
    }

    private void BuildChain() {
        this.handlers.Add(new OrcCommander());
        this.handlers.Add(new OrcOfficer());
        this.handlers.Add(new OrcSoldier());
    }


    public void MakeRequest(Request request) {
        this.handlers
            .OrderBy(handler => handler.Priority)
            .FirstOrDefault(handler => handler.CanHandleRequest(request))?
            .Handle(request);
    }
}
```

The chain of responsibility in action.

```csharp
[Theory]
[InlineData(RequestType.DefendCastle, "Don't let the barbarians enter my castle!!")]
[InlineData(RequestType.TorturePrisoner, "Don't just stand there, tickle him!")]
[InlineData(RequestType.CollectTax, "Don't steal, the King hates competition...")]
public void TestMakeRequestWithDifferentRequests(RequestType requestType, String requestDescription) {
    OrcKing king = new();
    Request request = new(requestType, requestDescription);

    king.MakeRequest(request);

    Assert.True(request.IsHandled, $"Expected request of type {requestType} to be handled, but it was not!");
}
```

## Class diagram

"-"

## Uygulanabilirlik

Chain of Responsibility kullanın:

* Birden fazla nesne bir isteği işleyebilir ve işleyici önceden bilinmiyorsa. İşleyici otomatik olarak belirlenmelidir.
* Alıcıyı açıkça belirtmeden bir isteği birkaç nesneye göndermek istiyorsunuz.
* Bir isteği işleyebilecek nesnelerin kümesi dinamik olarak belirlenmelidir.