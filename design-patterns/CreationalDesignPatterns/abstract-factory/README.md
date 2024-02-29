---
title: Abstract Factory
category: Creational
language: tr
tag:
 - Abstraction
 - Decoupling
 - Gang of Four
---

## Ayrıca bilinmesi gerekenler

Kit

## Amaç

Soyut Fabrika tasarım deseni, somut sınıflarını belirtmeden ilgili nesne ailelerini oluşturmanın bir yolunu sağlar. Bu, kodun kullandığı belirli nesne sınıflarından bağımsız olmasına olanak tanır, esnekliği ve bakım kolaylığını teşvik eder.

## Açıklama

Gerçek dünya örneği

> Bir krallık yaratmak için ortak bir tema ile nesnelere ihtiyacımız var. Elfler krallığı bir elf kralı, elf kalesi ve elf ordusuna ihtiyaç duyarken, orklar krallığı bir ork kralı, ork kalesi ve ork ordusuna ihtiyaç duyar. Krallıktaki nesneler arasında bir bağımlılık vardır.

Basitçe söylemek gerekirse

> Fabrikaların fabrikası; bireysel ancak ilgili/bağımlı fabrikaları somut sınıflarını belirtmeden bir araya getiren bir fabrika.

Wikipedia diyor ki

> Soyut fabrika deseni, somut sınıflarını belirtmeden ortak bir tema altında bireysel fabrikaları kapsüllemek için bir yol sağlar

**Programatik Örnek**

Yukarıdaki krallık örneğini uygulayalım. Öncelikle, krallıktaki nesneler için bazı arayüzler ve uygulamalarımız var.

```csharp
public interface IKing {
    String Description { get; }
}

public interface ICastle {
    String Name { get; }
}

public interface IArmy {
    Int32 Size { get; }
}

// Elflerin uygulamaları ->
public sealed class ElfKing : IKing {
    public String Description => AbstractFactoryConstants.Elf.KingDescription;

    internal ElfKing() { }
}

public sealed class ElfCastle : ICastle {
    public String Name => AbstractFactoryConstants.Elf.CastleName;

    internal ElfCastle() { }
}

public sealed class ElfArmy : IArmy {
    public Int32 Size => AbstractFactoryConstants.Elf.ArmySize;
    internal ElfArmy() { }
}

// Diğer uygulamalar benzer şekilde -> ...
```

Sonra krallık fabrikası için soyutlama ve uygulamalarımız var.

```csharp
public interface IKingdomFactory {
    ICastle CreateCastle();
    IKing CreateKing();
    IArmy CreateArmy();
}

public sealed class ElfKingdomFactory : IKingdomFactory {
    internal ElfKingdomFactory() { }

    public IArmy CreateArmy() {
        return new ElfArmy();
    }

    public ICastle CreateCastle() {
        return new ElfCastle();
    }

    public IKing CreateKing() {
        return new ElfKing();
    }
}

public sealed class HumanKingdomFactory : IKingdomFactory {
    internal HumanKingdomFactory() { }

    public IArmy CreateArmy() {
        return new HumanArmy();
    }

    public ICastle CreateCastle() {
        return new HumanCastle();
    }

    public IKing CreateKing() {
        return new HumanKing();
    }
}

public sealed class OrcKingdomFactory : IKingdomFactory {
    internal OrcKingdomFactory() { }

    public IArmy CreateArmy() {
        return new OrcArmy();
    }

    public ICastle CreateCastle() {
        return new OrcCastle();
    }

    public IKing CreateKing() {
        return new OrcKing();
    }
}
```

Şimdi ilgili nesnelerin bir ailesini yapmamıza izin veren soyut fabrikamız var, yani elf krallığı fabrikası elf kalesi, kral ve ordu vb. yaratır.

```csharp
var factory = new ElfKingdomFactory();
var castle = factory.CreateCastle();
var king = factory.CreateKing();
var army = factory.CreateArmy();

king.Description;
castle.Name;
army.Size;
```

Output:

```
This is the Elven King!
Elven Castle
1000
```

Şimdi, farklı krallık fabrikalarımız için bir fabrika tasarlayabiliriz. Bu örnekte, `FactoryMaker` oluşturduk, bu `ElfKingdomFactory`, `HumanKingdomFactory` veya `OrcKingdomFactory` örneklerinden birini döndürmekten sorumludur. Müşteri, `FactoryMaker` kullanarak istenen somut fabrikayı oluşturabilir ve bu da sırayla (`Army`, `King`, `Castle` türetilmiş) farklı somut nesneler üretecektir. Bu örnekte, müşterinin hangi tip krallık fabrikasını isteyeceğini parametreleştirmek için bir enum da kullandık.

```csharp
public static class FactoryMaker {
    public enum KingdomTypes : Byte {
        HUMAN,
        ELF,
        ORC
    }

    public static IKingdomFactory MakeFactory(KingdomTypes type) {
        return type switch {
            KingdomTypes.HUMAN => new HumanKingdomFactory(),
            KingdomTypes.ELF => new ElfKingdomFactory(),
            KingdomTypes.ORC => new OrcKingdomFactory(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}

[Fact]
public void VerifyHumanKingdomCreation() {
    Kingdom kingdom = CreateKingdom(KingdomTypes.HUMAN);

    Assert.IsType<HumanKing>(kingdom.King);
    Assert.Equal(AbstractFactoryConstants.Human.KingDescription, kingdom.King.Description);
    Assert.IsType<HumanCastle>(kingdom.Castle);
    Assert.Equal(AbstractFactoryConstants.Human.CastleName, kingdom.Castle.Name);
    Assert.IsType<HumanArmy>(kingdom.Army);
    Assert.Equal(AbstractFactoryConstants.Human.ArmySize, kingdom.Army.Size);
}

private static Kingdom CreateKingdom(KingdomTypes kingdomType) {
    IKingdomFactory kingdomFactory = MakeFactory(kingdomType);
    return new(kingdomFactory.CreateKing(), kingdomFactory.CreateCastle(), kingdomFactory.CreateArmy());
}
```

## Class diagram

"-"

## Uygulanabilirlik
Soyut Fabrika desenini şu durumlarda kullanın

* Sistemin ürünlerinin nasıl yaratıldığı, birleştirildiği ve temsil edildiği konusunda bağımsız olması gerektiğinde.

* Sistem, birden fazla ürün ailesiyle yapılandırılmalı olduğunda
* İlgili ürün nesneleri ailesinin birlikte kullanılması tasarlandığında ve bu kısıtlamayı zorlamak istediğinizde
* Ürünlerin bir sınıf kütüphanesini sunmak istediğinizde ve sadece arayüzlerini, uygulamalarını değil, açığa çıkarmak istediğinizde
* Bağımlılığın ömrü, tüketicinin ömründen kavramsal olarak daha kısa olduğunda.
* Belirli bir bağımlılığı oluşturmak için çalışma zamanında bir değere ihtiyacınız olduğunda
* Çalışma zamanında hangi ürünün çağrılacağına karar vermek istediğinizde.
* Çalışma zamanında bilinmesi gereken bir veya daha fazla parametreyi sağlamanız gerektiğinde
* Ürünler arasında tutarlılık gerektiğinde
* Programa yeni ürünler veya ürün aileleri eklerken mevcut kodu değiştirmek istemediğinizde.

## Örnek kullanım durumları

* Çalışma zamanında FileSystemAcmeService, DatabaseAcmeService veya NetworkAcmeService'in uygun uygulamasını çağırmak.
* Birim testi yazımı çok daha kolay hale gelir
* Farklı işletim sistemleri için UI araçları

## Sonuçlar

#### Faydaları

* Esneklik: Ürün aileleri arasında kod değişiklikleri yapmadan kolayca geçiş yapabilme.

* Bağımsızlık: Müşteri kodu sadece soyut arayüzlerle etkileşimde bulunur, taşınabilirliği ve bakım kolaylığını teşvik eder.

* Yeniden kullanılabilirlik: Soyut fabrikalar ve ürünler, projeler arası bileşen yeniden kullanımını kolaylaştırır.

* Bakım kolaylığı: Bireysel ürün ailelerindeki değişiklikler yerelleştirilir, güncellemeleri basitleştirir.



#### Trade-offs

* Karmaşıklık: Soyut arayüzler ve somut fabrikalar tanımlamak başlangıçta ek yük getirir.

* Dolaylılık: Müşteri kodu, fabrikalar aracılığıyla ürünlerle dolaylı olarak etkileşimde bulunur, potansiyel olarak şeffaflığı azaltabilir.


## İlgili Desenler

* [Factory Method](https://java-design-patterns.com/patterns/factory-method/)
* [Factory Kit](https://java-design-patterns.com/patterns/factory-kit/)