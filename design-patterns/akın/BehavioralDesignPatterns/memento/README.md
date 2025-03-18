---
title: Memento
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Diğer adıyla

Token

## Amaç

Kapsüllemeyi ihlal etmeden, bir nesnenin iç durumunu yakalayıp dışa aktararak nesnenin daha sonra bu 
duruma geri döndürülmesini sağlamaktır.

## Açıklama

Gerçek dünya örneği

> Astroloji uygulaması üzerinde çalışıyoruz ve zaman içinde yıldız özelliklerini analiz etmemiz 
> gerekiyor. Memento deseni kullanarak yıldız durumlarının anlık görüntülerini oluşturuyoruz.

Basit bir ifadeyle

> Memento deseni, nesnenin iç durumunu yakalayarak nesnelerin herhangi bir zamanda depolanmasını ve 
> geri yüklenmesini kolaylaştırır.

Wikipedia diyor ki

> Memento deseni, bir nesneyi önceki durumuna geri döndürme yeteneği sağlayan bir yazılım tasarım 
> desenidir (geri alma ile geri al).

**Programatik Örnek**

Öncelikle ele alabileceğimiz yıldız türlerini tanımlayalım.

```csharp
public enum StarType {
    SUN,
    RED_GIANT,
    WHITE_DWARF,
    SUPERNOVA,
    DEAD
}

public static class StartTypeExtensions {
    public static String Title(this StarType type) {
        return type switch {
            StarType.SUN => "Sun",
            StarType.RED_GIANT => "Red giant",
            StarType.WHITE_DWARF => "White dwarf",
            StarType.SUPERNOVA => "Supernova",
            StarType.DEAD => "Dead star",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
```

Sonraki olarak, doğrudan esaslarına geçelim. İşte `Star` sınıfı ve manipüle etmemiz gereken memento'lar. 
Özellikle `getMemento` ve `setMemento` metodlarına dikkat edin.

```csharp
public interface IStarMemento;

public partial class Star {
    private StarType type;
    private Int32 ageYears;
    private Int32 massTons;

    public Star(StarType startType, Int32 startAge, Int32 startMass) {
        this.type = startType;
        this.ageYears = startAge;
        this.massTons = startMass;
    }

    public void TimePasses() {
        this.ageYears *= 2;
        this.massTons *= 8;

        switch(this.type) {
            case StarType.RED_GIANT:
                this.type = StarType.WHITE_DWARF;
                break;
            case StarType.SUN:
                this.type = StarType.RED_GIANT;
                break;
            case StarType.SUPERNOVA:
                this.type = StarType.DEAD;
                break;
            case StarType.WHITE_DWARF:
                this.type = StarType.SUPERNOVA;
                break;
            case StarType.DEAD:
                this.ageYears *= 2;
                this.massTons = 0;
                break;
        }
    }

    public IStarMemento GetMemento() {
        return new StarMementoInternal(this.type, this.ageYears, this.massTons);
    }

    public void SetMemento(IStarMemento memento) {
        if(memento is StarMementoInternal state) {
            this.type = state.Type;
            this.ageYears = state.AgeYears;
            this.massTons = state.MassTons;
        }
    }

    public override String ToString() {
        return $"{this.type} age: {this.ageYears} years mass: {this.massTons} tons";
    }
}

public partial class Star {
    private sealed class StarMementoInternal : IStarMemento {
        public StarType Type { get; }
        public Int32 AgeYears { get; }
        public Int32 MassTons { get; }

        public StarMementoInternal(StarType type, Int32 ageYears, Int32 massTons) {
            this.Type = type;
            this.AgeYears = ageYears;
            this.MassTons = massTons;
        }
    }
}
```

Ve son olarak, mementoları kullanarak yıldız durumlarını depolamak ve geri yüklemek için aşağıdaki gibi kullanırız.

```csharp
    Stack<IStarMemento> states = new();
    Star star = new(StarType.SUN, 10000000, 500000);
    Console.WriteLine(star);
    states.Push(star.GetMemento());
    star.TimePasses();
    Console.WriteLine(star);
    states.Push(star.GetMemento());
    star.TimePasses();
    Console.WriteLine(star);
    states.Push(star.GetMemento());
    star.TimePasses();
    Console.WriteLine(star);
    states.Push(star.GetMemento());
    star.TimePasses();
    Console.WriteLine(star);

    while (states.Count > 0) {
        star.SetMemento(states.Pop());
        Console.WriteLine(star);
    }
```

Program output:

```
sun age: 10000000 years mass: 500000 tons
red giant age: 20000000 years mass: 4000000 tons
white dwarf age: 40000000 years mass: 32000000 tons
supernova age: 80000000 years mass: 256000000 tons
dead star age: 160000000 years mass: 2048000000 tons
supernova age: 80000000 years mass: 256000000 tons
white dwarf age: 40000000 years mass: 32000000 tons
red giant age: 20000000 years mass: 4000000 tons
sun age: 10000000 years mass: 500000 tons
```

## Class diagram

"-"

## Uygulanabilirlik

Memento desenini aşağıdaki durumlarda kullanın:

* Bir nesnenin durumunun bir anlık görüntüsü kaydedilmeli ve daha sonra o duruma geri dönebilmeliyse ve
* Durumu elde etmek için doğrudan bir arabirim, uygulama ayrıntılarını ortaya çıkarır ve nesnenin kapsüllemesini bozar.