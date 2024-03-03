---
title: Template method
category: Behavioral
language: tr
tag:
 - Gang of Four
---

## Amaç

Bir algoritmanın iskeletini bir işlemde tanımlayarak bazı adımları alt sınıflara ertelemek. Şablon Yöntemi, alt sınıfların algoritmanın yapısını değiştirmeden belirli adımları yeniden tanımlamasına olanak tanır.

## Açıklama

Gerçek dünya örneği

> Bir öğe çalmak için genel adımlar aynıdır. İlk olarak, hedefi seçersiniz, sonra onu bir şekilde şaşırtırsınız ve son olarak öğeyi çalarsınız. Ancak, bu adımları uygulamanın birçok yolu vardır.

Basit bir ifadeyle

> Template method deseni, ana sınıfta genel adımları belirler ve somut alt sınıf uygulamalarının ayrıntıları tanımlamasına izin verir.

Wikipedia diyor ki

> Nesne yönelimli programlamada, template method, Gamma ve diğerleri tarafından "Design Patterns" kitabında tanımlanan davranışsal tasarım desenlerinden biridir. Template method, genellikle soyut bir üst sınıf olan bir süper sınıfta bulunan bir yöntemdir ve bir işlemin iskeletini bir dizi yüksek seviyeli adıma göre tanımlar. Bu adımlar, template method aynı sınıfta ek yardımcı yöntemler tarafından uygulanır.

**Programatik Örnek**

Önce template method sınıfını ve onun somut uygulamalarını tanıtalım.
Alt sınıfların template method geçersiz kılmasını önlemek için template method (bizim durumumuzda `steal` yöntemi) `final` olarak tanımlanmalıdır, aksi takdirde temel sınıfta tanımlanan iskelet alt sınıflarda geçersiz kılınabilir.

```csharp
public abstract class StealingMethod {
    protected internal abstract String PickTarget();
    protected internal abstract void ConfuseTarget(String target);
    protected internal abstract void StealTheItem(String target);

    public void Steal() {
        String target = PickTarget();
        ConfuseTarget(target);
        StealTheItem(target);
    }
}

public class SubtleMethod : StealingMethod {
    protected internal override String PickTarget() {
        return "Select an unsuspecting merchant";
    }

    protected internal override void ConfuseTarget(String target) {
        Logger.Info("Etkileyici bir hikaye ile {0}'yi dikkatini dağıtın!", target);
    }

    protected internal override void StealTheItem(String target) {
        Logger.Info("While the {0} is distracted, relieve them of their valuables.", target);
    } 
}

public class HitAndRunMethod : StealingMethod {
    protected internal override String PickTarget() {
        return "Identify a wealthy traveler";
    }

    protected internal override void ConfuseTarget(String target) {
        Logger.Info("Create a diversion to disorient the {0}.", target);
    }

    protected internal override void StealTheItem(String target) {
        Logger.Info("Seize the opportune moment to snatch the {0}'s belongings and disappear!", target);
    }
}
```

```csharp
public class HalflingThief {
    private StealingMethod method;

    public HalflingThief(StealingMethod method) {
        this.method = method;
    }

    public void Steal() {
        this.method.Steal();
    }

    public void ChangeMethod(StealingMethod method) {
        this.method = method;
    }
}
```


```csharp
HalflingThief thief = new(new HitAndRunMethod());
thief.Steal();
thief.ChangeMethod(new SubtleMethod());
thief.Steal();
```

Template Method deseni şu durumlarda kullanılmalıdır:

* Bir algoritmanın değişebilen davranışını alt sınıflara bırakarak algoritmanın sabit kısımlarını bir kez uygulamak için kullanılır.
* Alt sınıflar arasında ortak davranışın kod tekrarını önlemek ve birleştirmek için kullanılır. Bu, Opdyke ve Johnson tarafından açıklanan "genelleştirmek için yeniden düzenleme"nin iyi bir örneğidir. İlk olarak, mevcut kodun farklılıklarını belirleyip bu farklılıkları yeni işlemlere ayırırsınız. Son olarak, farklı kodu bu yeni işlemlerden birini çağıran bir şablon yöntemiyle değiştirirsiniz.
* Alt sınıfların genişlemelerini kontrol etmek için kullanılır. Belirli noktalarda "hook" işlemlerini çağıran bir şablon yöntemi tanımlayarak sadece bu noktalarda genişlemelere izin verir.