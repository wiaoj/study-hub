---
title: Specification
category: Behavioral
language: tr
tag:
 - Data access
---

## Ayrıca bilinen adları

Filter, Criteria

## Amaç

Specification deseni, bir adayın nasıl eşleşeceğini, eşleştirileceği aday nesnesinden ayırır. Seçimdeki kullanışlılığının yanı sıra, doğrulama ve sipariş oluşturma için de değerlidir.

## Açıklama

Gerçek dünya örneği

> Farklı yaratıklardan oluşan bir havuzumuz var ve genellikle onların bir alt kümesini seçmemiz gerekiyor. Uçabilen yaratıklar, 500 kilogramdan ağır yaratıklar gibi arama özelliklerimizi yazabilir ve ardından filtrelemeyi gerçekleştirecek tarafa verebiliriz.

Basit bir ifadeyle

> Specification deseni, arama kriterlerini aramayı gerçekleştiren nesneden ayırmamıza olanak tanır.

Wikipedia diyor ki

> Bilgisayar programlamada, specification deseni, iş kurallarının boolean mantık kullanarak birleştirilerek yeniden birleştirilebileceği belirli bir yazılım tasarım desenidir.

**Programatik Örnek**

Yukarıdaki yaratık havuzu örneğimize baktığımızda, belirli özelliklere sahip yaratıklarımız var. Bu özellikler, burada (Size, Movement ve Color enumları tarafından temsil edilen) önceden tanımlanmış, sınırlı bir küme olabilir; ancak sürekli değerler de olabilir (örneğin, bir Yaratığın kütlesi). Bu durumda, "parametreli specification" olarak adlandırdığımız, Yaratık oluşturulurken özellik değerinin bir argüman olarak verilebileceği daha esnek bir yaklaşım kullanmak daha uygundur. Üçüncü bir seçenek, önceden tanımlanmış ve/veya parametreli özellikleri boolean mantık kullanarak birleştirerek neredeyse sonsuz seçim olasılıklarına izin verir (bu "kompozit specification" olarak adlandırılır, aşağıya bakınız). Her yaklaşımın artıları ve eksileri bu belgenin sonundaki tabloda detaylı olarak açıklanmıştır.

```csharp
public interface ICreature {
    String Name { get; }
    Size Size { get; }
    Movement Movement { get; }
    Color Color { get; }
    Mass Mass { get; }
}
```

Ve `Dragon` uygulaması şu şekildedir.

```csharp
public sealed class Dragon : AbstractCreature {
    private const Double DefaultMass = 39300.0D;

    public Dragon() : this(new Mass(DefaultMass)) { }
    public Dragon(Mass mass) : base(nameof(Dragon), Size.Large, Movement.Flying, Color.Red, mass) { }
}
```

Şimdi onlardan bazılarını seçmek istediğimizde, seçicileri kullanırız. Uçan yaratıkları seçmek için `MovementSelector`'ı kullanmalıyız.

```csharp
public sealed class MovementSpecification : Specification<ICreature> {
    private readonly Movement movement;

    public MovementSpecification(Movement movement) {
        this.movement = movement;
    }

    public override Boolean IsSatisfied(ICreature item) {
        return item.Movement.Equals(this.movement);
    }
}
```

Öte yandan, belirli bir miktarın üzerindeki yaratıkları seçerken `MassGreaterThanSelector`'ı kullanırız.

```csharp
public sealed class MassGreaterThanSpecification : Specification<ICreature> {
    private readonly Mass mass;

    public MassGreaterThanSpecification(Double mass) : this(new Mass(mass)) { }
    public MassGreaterThanSpecification(Mass mass) {
        this.mass = mass;
    }

    public override Boolean IsSatisfiedBy(ICreature item) {
        return item.Mass.GreaterThan(this.mass);
    }
}
```

Bu yapı taşlarıyla birlikte, kırmızı yaratıklar için bir arama yapabiliriz:

```csharp
    var redCreatures = creatures.Where(new ColorSpecification(Color.Red).IsSatisfiedBy)
      .ToList();
```

Ancak parametreli seçiciyi de şu şekilde kullanabiliriz:

```csharp
    var heavyCreatures = creatures.Where(new MassGreaterThanSpecification(500.0).IsSatisfiedBy)
      .ToList();
```

Üçüncü seçenek olarak, birden fazla seçiciyi bir araya getirebiliriz. Özel yaratıklar için bir arama yapmak (kırmızı, uçan ve küçük olmayan) aşağıdaki gibi yapılabilir:

```csharp
    var specialCreaturesSelector = new ColorSpecification(Color.Red)
        .And(new MovementSpecification(Movement.Flying))
        .And(new SizeSpecification(Size.Small).not());

    var specialCreatures = creatures.Where(specialCreaturesSelector)
      .ToList();
```

**Kompozit Özellik Hakkında Daha Fazla Bilgi**

Kompozit Özellik'te, üç temel mantıksal operatörü kullanarak diğer seçicileri ("leaf" olarak adlandırılır) birleştirerek özel `Specification` örnekleri oluşturacağız. Bu operatörler `AndSpecification`, `OrSpecification` ve `NotSpecification` içinde uygulanmıştır.

```csharp
public abstract class Specification<T> : ISpecification<T> where T : class {
    public abstract Boolean IsSatisfiedBy(T item);
    public Specification<T> And(Specification<T> other) {
        ArgumentNullException.ThrowIfNull(other);
        return new AndSpecification<T>(this, other);
    }

    public Specification<T> Or(Specification<T> other) {
        ArgumentNullException.ThrowIfNull(other);
        return new OrSpecification<T>(this, other);
    }

    public Specification<T> Not() {
        return new NotSpecification<T>(this);
    }
}
```

```csharp
public class AndSpecification<T> : Specification<T> where T : class {
    private readonly Specification<T> leftSpecification;
    private readonly Specification<T> rightSpecification;

    public AndSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification) {
        this.leftSpecification = leftSpecification;
        this.rightSpecification = rightSpecification;
    }

    public override bool IsSatisfiedBy(T item) {
        return leftSpecification.IsSatisfiedBy(item) && rightSpecification.IsSatisfiedBy(item);
    }
}
```

Yapmamız gereken tek şey, mümkün olduğunca genel olan yaprak seçiciler (sabit veya parametreli olanlar) 
oluşturmak ve yukarıda örneklendiği gibi herhangi bir miktarda seçiciyi birleştirerek ``Specification`` 
sınıfını örneklendirebilmektir. Ancak, birçok mantıksal operatörü birleştirirken hata yapmak kolaydır; 
özellikle işlemlerin önceliğine dikkat etmeliyiz. Genel olarak, Kompozit Özellik, her filtreleme işlemi 
için bir Seçici sınıfı oluşturma ihtiyacı olmadığından daha yeniden kullanılabilir kod yazmanın harika bir 
yoludur. Bunun yerine, genel "yaprak" seçicilerimizi ve bazı temel boolean mantığı kullanarak ``Specification`` 
sınıfının bir örneğini "anında" oluştururuz.

**Farklı yaklaşımların karşılaştırılması**

| Desen                      | Kullanım                                                                                                                                        | Artıları                                   | Eksileri                                                        |
| -------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------ | --------------------------------------------------------------- |
| Sabit Şart Belirleme       | Seçim kriterleri az ve önceden biliniyor                                                                                                        | + Kolay uygulanabilir                      | - Esnek değil                                                   |
|                            |                                                                                                                                                 | + İfade yeteneği                           |
| Parametreli Şart Belirleme | Seçim kriterleri geniş bir değer aralığına sahip (ör. kütle, hız,...)                                                                           | + Bir miktar esneklik                      | - Hala özel amaçlı sınıflar gerektirir                          |
| Kompozit Şart Belirleme    | Birçok seçim kriteri vardır ve bunlar birden çok şekilde birleştirilebilir, bu nedenle her bir seçici için bir sınıf oluşturmak mümkün değildir | + Çok esnek, birçok özel sınıf gerektirmez | - Biraz daha anlaşılması zor                                    |
|                            |                                                                                                                                                 | + Mantıksal operasyonları destekler        | - Yaprak olarak kullanılan temel sınıfları oluşturmanız gerekir |

## Class diagram

"-"

## Uygulanabilirlik

Özellik deseni kullanılabilir durumdayken:

* Belirli bir kriterlere göre nesnelerin bir alt kümesini seçmeniz ve seçimi çeşitli zamanlarda yenilemeniz gerekiyorsa.
* Belirli bir rol için uygun nesnelerin kullanıldığını kontrol etmeniz gerekiyorsa (doğrulama).

## İlgili desenler

* [Repository](../../ArchitecturalPatterns/repository/)