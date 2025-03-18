---
title: Iterator
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Ayrıca bilinmesi gerekenler

Cursor

## Amaç

Bir toplu nesnenin elemanlarına, altındaki temsili açığa çıkarmadan sıralı bir şekilde erişim sağlamak için bir yol sunar.

## Açıklama

Gerçek dünya örneği

> Hazine sandığı, yüzükler, iksirler ve silahlar gibi çeşitli türde sihirli eşyalar içerir. Sandıktaki eşyalar, sandığın sağladığı bir yineleyici (iterator) kullanılarak türe göre gözden geçirilebilir.

Sadece sözcüklerle

> Konteynerlar, elemanlara erişim sağlamak için temsil bağımsız bir yineleyici arayüzü sağlayabilir.

Wikipedia diyor ki

> Nesne yönelimli programlamada, yineleyici deseni, bir konteyneri dolaşmak ve konteynerin elemanlarına erişmek için bir yineleyicinin kullanıldığı bir tasarım desenidir.

**Programatik Örnek**

Örneğimizdeki ana sınıf, içinde eşyalar bulunduran `TreasureChest` sınıfıdır.

```csharp
public class TreasureChest {
    private readonly List<Item> items;
    public IReadOnlyList<Item> Items => this.items.AsReadOnly();

    public TreasureChest() {
        this.items = [
            new(ItemTypes.POTION, "Elixir of Bravery"),
            new(ItemTypes.RING, "Shadow Band"),
            new(ItemTypes.POTION, "Wisdom Brew"),
            new(ItemTypes.POTION, "Blood Essence"),
            new(ItemTypes.WEAPON, "Silver Blade +1"),
            new(ItemTypes.POTION, "Decay Elixir"),
            new(ItemTypes.POTION, "Healing Salve"),
            new(ItemTypes.RING, "Armor Ringlet"),
            new(ItemTypes.WEAPON, "Halberd of Steel"),
            new(ItemTypes.WEAPON, "Poisoned Dagger")
        ];
    }

    public IIterator<Item> Iterator(ItemTypes itemType) {
        return new TreasureChestItemIterator(this, itemType);
    }
}
```

İşte `Item` sınıfı:

```csharp
public class Item {
    private ItemTypes type;
    private readonly String name;

    public ItemTypes Type => this.type;

    public Item(ItemTypes type, String name) {
        SetType(type);
        this.name = name;
    }

    public void SetType(ItemTypes type) {
        this.type = type;
    }

    public override String ToString() {
        return this.name;
    }
}

public enum ItemTypes {
    ANY,
    WEAPON,
    RING,
    POTION
}
```

`IIterator` arayüzü son derece basittir.

```csharp
public interface IIterator<out T> {
    Boolean HasNext();
    T? Next();
}
```

Aşağıdaki örnekte, sandıktaki yüzük türündeki eşyalar üzerinden yineleme yapılıyor.

```csharp
var itemIterator = new TreasureChest().Iterator(ItemTypes.RING);
while (itemIterator.HasNext()) {
  Console.WriteLine(itemIterator.Next().ToString());
}
```

Program output:

```
Shadow Band
Armor Ringlet
```

## Class diagram

"-"

## Uygulanabilirlik

Iterator desenini kullanın

* Bir toplu nesnenin içeriğine, iç temsiline maruz kalmadan erişmek için.
* Toplu nesnelerin birden fazla gezinmesini desteklemek için.
* Farklı toplu yapıların gezinmesi için birleşik bir arayüz sağlamak için.