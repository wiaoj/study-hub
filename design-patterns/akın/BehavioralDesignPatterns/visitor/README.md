---
title: Visitor
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Amaç

Bir nesne yapısının elemanları üzerinde gerçekleştirilecek bir işlemi temsil etmek. Ziyaretçi, 
bu işlemi yapmadan önce elemanların sınıflarını değiştirmenize gerek kalmadan yeni bir işlem 
tanımlamanıza olanak sağlar.

## Açıklama

Gerçek dünya örneği

> Komutanın altında iki çavuşun ve her çavuşun altında üç askerin bulunduğu bir ağaç yapısı düşünün. 
> Hiyerarşi ziyaretçi desenini uyguladığından, komutan, çavuşlar, askerler veya hepsiyle etkileşim 
> kurabilen yeni nesneler kolayca oluşturabiliriz.

Basit bir ifadeyle

> Ziyaretçi deseni, veri yapısının düğümlerinde gerçekleştirilebilecek işlemleri tanımlar.

Wikipedia diyor ki

> Nesne yönelimli programlama ve yazılım mühendisliğinde, ziyaretçi tasarım deseni, üzerinde 
> çalıştığı nesne yapısını bir algoritmadan ayırmanın bir yoludur. Bu ayrımın pratik bir sonucu, 
> yapıları değiştirmeden mevcut nesne yapılarına yeni işlemler ekleyebilme yeteneğidir.

**Programatik Örnek**

Yukarıdaki ordu birimi örneğini ele alarak, öncelikle Unit ve UnitVisitor temel tiplerine sahibiz.

```csharp
public abstract class Unit {
    private readonly Unit[] children;

    protected Unit(params Unit[] children) {
        this.children = children;
    }

    public virtual void Accept(IUnitVisitor visitor) {
        foreach(Unit child in this.children)
            child.Accept(visitor);
    }
}

public interface IUnitVisitor {
    void Visit(Soldier soldier);
    void Visit(Sergeant sergeant);
    void Visit(Commander commander);
}
```

Ardından somut birimlere sahibiz.

```csharp
public class Commander(params Unit[] children) : Unit(children) {
    public override void Accept(IUnitVisitor visitor) {
        visitor.Visit(this);
        base.Accept(visitor);
    }

    public override String ToString() {
        return nameof(Commander);
    }
}

public class Sergeant(params Unit[] children) : Unit(children) {
    public override void Accept(IUnitVisitor visitor) {
        visitor.Visit(this);
        base.Accept(visitor);
    }

    public override String ToString() {
        return nameof(Sergeant);
    }
}

public class Soldier(params Unit[] children) : Unit(children) {
    public override void Accept(IUnitVisitor visitor) {
        visitor.Visit(this);
        base.Accept(visitor);
    }

    public override String ToString() {
        return nameof(Soldier);
    }
}
```

İşte bazı somut ziyaretçiler.

```csharp
public class CommanderVisitor : IUnitVisitor {
    public void Visit(Soldier soldier) { }

    public void Visit(Sergeant sergeant) { }

    public void Visit(Commander commander) {
        Func<String> action = () => "Good to see you Commander";
    }
}

public class SergeantVisitor : IUnitVisitor {
    public void Visit(Soldier soldier) { }

    public void Visit(Sergeant sergeant) {
        Func<String> action = () => "Hello Sergeant";
    }

    public void Visit(Commander commander) { }
}

public class SoldierVisitor : IUnitVisitor {
    public void Visit(Soldier soldier) {
        Func<String> action = () => "Greetings Soldier";
    }

    public void Visit(Sergeant sergeant) { }

    public void Visit(Commander commander) { }
}
```

Son olarak, ziyaretçilerin gücünü gösterebiliriz.

```csharp
commander.Accept(new SoldierVisitor());
commander.Accept(new SergeantVisitor());
commander.Accept(new CommanderVisitor());
```

Program output:

```
Greetings Soldier
Greetings Soldier
Greetings Soldier
Greetings Soldier
Greetings Soldier
Greetings Soldier
Hello Sergeant
Hello Sergeant
Good to see you Commander
```

## Class diagram

"-"

## Uygulanabilirlik

Ziyaretçi desenini aşağıdaki durumlarda kullanın:

* Bir nesne yapısı, farklı arabirimlere sahip nesne sınıflarını içerir ve bu nesneler üzerinde, beton sınıflarına bağlı olan işlemleri gerçekleştirmek istersiniz.
* Nesne yapısındaki nesneler üzerinde birçok farklı ve ilişkisiz işlem gerçekleştirilmesi gerekiyor ve bu işlemleri sınıflarını "kirletmek" istemiyorsunuz. Ziyaretçi, ilgili işlemleri bir sınıfta tanımlayarak bunları bir arada tutmanızı sağlar. Nesne yapısı birçok uygulama tarafından paylaşılıyorsa, ihtiyaç duyan uygulamalarda bu işlemleri kullanmak için Ziyaretçi desenini kullanın.
* Nesne yapısını tanımlayan sınıflar nadiren değişir, ancak yapının üzerinde yeni işlemler tanımlamak istersiniz. Nesne yapısı sınıflarını değiştirmek, tüm ziyaretçilere olan arabirimi yeniden tanımlamayı gerektirir ve bu potansiyel olarak maliyetli olabilir. Nesne yapısı sınıfları sık sık değişiyorsa, işlemleri bu sınıflarda tanımlamak daha iyidir.