---
title: Factory
category: Creational
language: tr
tag:
 - Gang of Four
---

## Ayrıca bilinmesi gerekenler

* Simple Factory
* Static Factory Method

## Amaç

Factory olarak adlandırılan bir sınıf içinde statik bir metod sağlayarak, uygulama mantığını gizlemek ve müşteri kodunun yeni nesneleri başlatma yerine kullanımına odaklanmasını sağlamak.

## Açıklama

Gerçek dünya örneği

> Bir simyacı, madeni para üretmek üzere olduğunu düşünün. Simyacının hem altın hem de bakır paraları üretebilmesi ve mevcut kaynak kodunu değiştirmeden aralarında geçiş yapabilmesi gerekir. Factory deseni, ilgili parametrelerle çağrılabilecek statik bir yapı metodu sağlayarak bunu mümkün kılar.

Wikipedia diyor ki

> Factory, diğer nesneleri oluşturan bir nesnedir – resmi olarak fabrika, değişen prototip veya sınıftan nesneler döndüren bir fonksiyon veya metottur.

**Programatik Örnek**

`Coin` adında bir arayüzümüz ve `GoldCoin` ile `CopperCoin` adında iki uygulamamız var.

```csharp
public interface ICoin {
    String Description { get; }
}

public class GoldCoin : ICoin {
    public string Description => FactoryConstants.GoldCoinDescription;
}

public class CopperCoin : ICoin {
    public string Description => FactoryConstants.CopperCoinDescription;
}
```

Desteklediğimiz para türlerini temsil eden bir enumeration (enum) (`GoldCoin` ve `CopperCoin`).

```csharp
public enum CoinTypes {
    COPPER,
    GOLD
}
```

`CoinFactory` sınıfı içinde kapsüllenmiş `CreateCoin` adında statik bir metod ile para nesneleri oluşturuyoruz.

```csharp
public static ICoin CreateCoin(CoinTypes type) {
    return type switch {
        CoinTypes.COPPER => new CopperCoin(),
        CoinTypes.GOLD => new GoldCoin(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}
```

Now on the client code we can create different types of coins using the factory class.

```csharp
Console.WriteLine("The alchemist begins his work.");
var coin1 = CoinFactory.CreateCoin(CoinType.Copper);
var coin2 = CoinFactory.CreateCoin(CoinType.Gold);
Console.WriteLine(coin1.Description);
Console.WriteLine(coin2.Description);
```

```csharp
[Fact]
public void ShouldReturnCopperCoinInstance() {
    ICoin copperCoin = CoinFactory.CreateCoin(CoinTypes.COPPER);
    Assert.IsType<CopperCoin>(copperCoin);
}

[Fact]
public void ShouldReturnGoldCoinInstance() {
    ICoin goldCoin = CoinFactory.CreateCoin(CoinTypes.GOLD);
    Assert.IsType<GoldCoin>(goldCoin);
}
```

Program output:

```
The alchemist begins his work.
This is a copper coin.
This is a gold coin.
```

## Class Diagram

"-"

## Uygulanabilirlik

Bir nesnenin oluşturulmasıyla ilgilendiğinizde ancak nasıl oluşturulup yönetileceğiyle ilgilenmediğinizde fabrika desenini kullanın.

Artılar

* Tüm nesne oluşturma işlemlerini tek bir yerde tutar ve kod tabanı boyunca 'new' anahtar kelimesinin yayılmasını önler.
* Gevşek bağlı kod yazmaya olanak tanır. Ana avantajları arasında daha iyi test edilebilirlik, kolay anlaşılır kod, değiştirilebilir bileşenler, ölçeklenebilirlik ve izole edilmiş özellikler bulunur.

Cons

* Kod, olması gerekenden daha karmaşık hale gelir. 

## İlgili Desenler

* [Factory Method](https://java-design-patterns.com/patterns/factory-method/)
* [Factory Kit](https://java-design-patterns.com/patterns/factory-kit/)
* [Abstract Factory](../abstract-factory)