---
title: Command
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Ayrıca bilinmesi gerekenler

Action, Transaction

## Amaç

Bir isteği bir nesne olarak kapsülleyerek farklı isteklerle müşterileri parametreleme, istekleri sıraya koyma veya kaydetme ve geri alınabilir işlemleri destekleme imkanı sağlar.

## Açıklama

Gerçek dünya örneği

> Bir büyücü, bir gobline büyüler yapmaktadır. Büyüler, birer birer goblin üzerinde gerçekleştirilir.
> İlk büyü goblini küçültür ve ikincisi onu görünmez yapar. Sonra büyücü, büyüleri birer birer tersine çevirir.
> Burada her bir büyü, geri alınabilir bir komut nesnesidir.

Sadece sözcüklerle

> İstekleri komut nesneleri olarak saklamak, bir işlemi daha sonra gerçekleştirmeyi veya geri almaya izin verir.

Wikipedia diyor ki

> Nesne yönelimli programlamada, komut deseni, bir işlemi gerçekleştirmek veya daha sonra bir olayı tetiklemek için gerekli tüm bilgileri kapsülleyen bir nesnenin kullanıldığı davranışsal bir tasarım desenidir.

**Programatik Örnek**

İşte büyücü ve goblin ile örnek kod. `Wizard` sınıfından başlayalım.

```csharp
public class Wizard {
    private readonly LinkedList<Action> undoStack = new();
    private readonly LinkedList<Action> redoStack = new();

    public void CastSpell(Action spell) {
        spell.Invoke();
        this.undoStack.AddLast(spell);
    }

    public void UndoLastSpell() {
        if(this.undoStack.Count > 0) {
            Action previousSpell = this.undoStack.Last!.Value;
            this.undoStack.RemoveLast();
            this.redoStack.AddLast(previousSpell);
            previousSpell.Invoke();
        }
    }

    public void RedoLastSpell() {
        if(this.redoStack.Count > 0) {
            Action previousSpell = this.redoStack.Last!.Value;
            this.redoStack.RemoveLast();
            this.undoStack.AddLast(previousSpell);
            previousSpell.Invoke();
        }
    }

    public override String ToString() {
        return nameof(Wizard);
    }
}
```

Sonra, büyülerin hedefi olan goblinimiz var.

```csharp
public abstract class Target {
    private Size size;
    private Visibility visibility;

    public Size Size => this.size;
    public Visibility Visibility => this.visibility;

    protected Target(Size size, Visibility visibility) {
        this.size = size;
        this.visibility = visibility;
    }

    public void ChangeSize() {
        Size oldSize = this.size == Size.SMALL ? Size.NORMAL : Size.SMALL;
        this.size = oldSize;
    }

    public void ChangeVisibility() {
        Visibility visible = this.visibility == Visibility.VISIBLE
                ? Visibility.INVISIBLE : Visibility.VISIBLE;
        this.visibility = visible;
    }

    public String Status() {
        return $"{this}, [size={this.size}] [visibility={this.visibility}]";
    }

    public sealed override String ToString() {
        return GetType().Name;
    }
}

public sealed class Goblin() : Target(Size.NORMAL, Visibility.VISIBLE);
```

Finally, we have the wizard in the main function casting spells.

```csharp
  Wizard wizard = new();
  Goblin goblin = new();

  // casts shrink/unshrink spell
  wizard.CastSpell(goblin.ChangeSize);

  // casts visible/invisible spell
  wizard.CastSpell(goblin.ChangeVisibility);

  // undo and redo casts
   wizard.UndoLastSpell();
   wizard.RedoLastSpell();
```

Here's the whole example in action.

```csharp
[Fact]
public void TestCommand() {
    Wizard wizard = new();
    Goblin goblin = new();

    wizard.CastSpell(goblin.ChangeSize);
    VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.VISIBLE);

    wizard.CastSpell(goblin.ChangeVisibility);
    VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.INVISIBLE);

    wizard.UndoLastSpell();
    VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.VISIBLE);

    wizard.UndoLastSpell();
    VerifyGoblin(goblin, GOBLIN, Size.NORMAL, Visibility.VISIBLE);

    wizard.RedoLastSpell();
    VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.VISIBLE);

    wizard.RedoLastSpell();
    VerifyGoblin(goblin, GOBLIN, Size.SMALL, Visibility.INVISIBLE);
}

private void VerifyGoblin(Goblin goblin, String expectedName, Size expectedSize, Visibility expectedVisibility) {
    Assert.Equal(expectedName, goblin.ToString());
    Assert.Equal(expectedSize, goblin.Size);
    Assert.Equal(expectedVisibility, goblin.Visibility);
    output.WriteLine(goblin.Status());
}
```

Here's the program output:

```
Goblin, [size=SMALL] [visibility=VISIBLE]
Goblin, [size=SMALL] [visibility=INVISIBLE]
Goblin, [size=SMALL] [visibility=VISIBLE]
Goblin, [size=NORMAL] [visibility=VISIBLE]
Goblin, [size=SMALL] [visibility=VISIBLE]
Goblin, [size=SMALL] [visibility=INVISIBLE]
```

## Class diagram

"-"

## Uygulanabilirlik

Komut desenini şu durumlarda kullanmak isteyebilirsiniz:

* Nesneleri gerçekleştirecekleri bir eylemle parametrele. Bu tür bir parametreleme, bir geri çağırma fonksiyonu ile prosedürel bir dilde ifade edilebilir, yani, daha sonra çağrılacak bir yerde kaydedilen bir fonksiyon. Komutlar, geri çağırmalar için nesne yönelimli bir yerinedir.

* Farklı zamanlarda istekleri belirt, sıraya koy ve gerçekleştir. Bir Komut nesnesi, orijinal istekten bağımsız bir yaşama sahip olabilir. Bir isteğin alıcısı, adres uzayından bağımsız bir şekilde temsil edilebiliyorsa, istek için bir komut nesnesini farklı bir sürece aktarabilir ve orada isteği yerine getirebilirsiniz.

* Geri alma işlevini destekle. Komutun execute operasyonu, etkilerini tersine çevirmek için durumu kendinde saklayabilir. Komut arayüzü, önceki bir execute çağrısının etkilerini tersine çeviren bir un-execute operasyonu eklenmelidir. Gerçekleştirilen komutlar, bir geçmiş listesinde saklanır. Sınırsız seviye geri alma ve yeniden yapma işlevselliği, bu listeyi geriye ve ileriye doğru gezinerek sırasıyla un-execute ve execute çağırarak elde edilir.

* Sistem çöktüğünde yeniden uygulanabilmesi için değişiklikleri kaydetme işlevini destekle. Komut arayüzüne yükleme ve saklama işlemleri ekleyerek, değişikliklerin kalıcı bir günlüğünü tutabilirsiniz. Bir çökmeden kurtulmak, kaydedilmiş komutları diskten yeniden yüklemeyi ve execute operasyonu ile onları yeniden çalıştırmayı içerir.

* Sistemi, ilkel işlemler üzerine kurulu yüksek seviyeli işlemler etrafında yapılandır. Bu tür bir yapı, işlemleri destekleyen bilgi sistemlerinde yaygındır. Bir işlem, bir dizi veri değişikliğini kapsülle. Komut deseni, işlemleri modellemek için bir yol sunar. Komutlar, hepsini aynı şekilde çağırmanıza olanak tanıyan ortak bir arayüze sahiptir. Desen, sistemde yeni işlemlerle kolayca genişletilebilir.

* İsteklerin bir geçmişini tut.
* Geri çağırma işlevselliğini uygula.
* Geri alma işlevselliğini uygula.