---
title: State
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Diğer adıyla

Objects for States

## Amaç

Bir nesnenin iç durumu değiştiğinde davranışını değiştirmesine izin vermek. Nesne, sınıfını değiştirmiş gibi görünecektir.

## Açıklama

Gerçek dünya örneği

> Bir mamutu doğal yaşam alanında gözlemlediğinizde, duruma bağlı olarak davranışını değiştirdiği görülür. İlk başta sakin görünebilir, ancak zamanla tehdit algıladığında öfkelenir ve çevresine tehlikeli olur.

Basit bir ifadeyle

> Durum deseni bir nesnenin davranışını değiştirmesine olanak sağlar.

Wikipedia diyor ki

> Durum deseni, bir nesnenin iç durumu değiştiğinde davranışını değiştirmesine olanak sağlayan bir davranışsal yazılım tasarım desenidir. Bu desen, sonlu durum makineleri kavramına yakındır. Durum deseni, desenin arayüzünde tanımlanan yöntemlerin çağrıları aracılığıyla bir stratejiyi değiştirebilen bir strateji deseni olarak yorumlanabilir.

**Programatik Örnek**

İşte durum arayüzü ve onun somut uygulamaları.

```csharp
public interface IState {
    void OnEnterState();
    void Observe(); 
}

public class PeacefulState : IState {
    private readonly Mammoth mammoth;
    
    public PeacefulState(Mammoth mammoth) {
        this.mammoth = mammoth;
    }

    public void Observe() {
        Logger.Information($"{this.mammoth} is grazing peacefully.");
    }

    public void OnEnterState() {
        Logger.Information($"{this.mammoth} sighs contentedly.");
    }
}

public class AngryState : IState {
    private readonly Mammoth mammoth;

    public AngryState(Mammoth mammoth) {
        this.mammoth = mammoth;
    }

    public void Observe() {
        Logger.Information($"{this.mammoth} is in a rage!");
    }

    public void OnEnterState() {
        Logger.Information($"{this.mammoth} roars in anger!");
    }
}
```

Ve işte durumu içeren mamut.

```csharp
public class Mammoth {
    private IState state;

    public Mammoth() {
        this.state = new PeacefulState(this);
    }

    public void TimePasses() {
        if(this.state is PeacefulState) {
            ChangeStateTo(new AngryState(this));
        }
        else {
            ChangeStateTo(new PeacefulState(this));
        }
    }

    private void ChangeStateTo(IState newState) {
        this.state = newState;
        this.state.OnEnterState();
    }

    public sealed override String ToString() {
        return "The mammoth";
    }

    public void Observe() {
        this.state.Observe();
    }
}
```

İşte mamutun zamanla nasıl davrandığına dair tam örnek.

```csharp
    Mammoth mammoth = new();
    mammoth.Observe();
    mammoth.TimePasses();
    mammoth.Observe();
    mammoth.TimePasses();
    mammoth.Observe();
```

Program output:

```csharp
    The mammoth is grazing peacefully.
    The mammoth roars in anger!
    The mammoth is in a rage!
    The mammoth sighs contentedly.
    The mammoth is grazing peacefully.
```

## Class diagram

"-"

## Uygulanabilirlik

State deseni aşağıdaki durumlardan herhangi birinde kullanılabilir:

* Bir nesnenin davranışı durumuna bağlıdır ve bu duruma bağlı olarak çalışma zamanında davranışını değiştirmesi gerekmektedir.
* İşlemler, nesnenin durumuna bağlı olarak büyük, çok parçalı koşullu ifadeler içerir. Bu durum genellikle bir veya daha fazla numaralı sabit tarafından temsil edilir. Sıklıkla, birkaç işlem bu aynı koşullu yapısını içerir. State deseni, koşullu yapının her bir dalını ayrı bir sınıfa yerleştirir. Bu, nesnenin durumunu bağımsız olarak değişebilen ayrı bir nesne olarak ele almanızı sağlar.