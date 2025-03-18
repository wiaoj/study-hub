---
title: Pipeline
category: Behavioral
language: tr
tag:
 - Decoupling
---

## Amaç

Verilerin bir dizi aşamada işlenmesine olanak tanır. Başlangıç girdisini alır ve işlenmiş çıktıyı bir sonraki aşamada kullanmak üzere geçirir.

## Açıklama

Pipeline deseni, sıralı aşamaları kullanarak bir dizi giriş değerini işlemek için kullanılır. Her uygulanan görev, pipeline'ın bir aşamasını temsil eder. Pipeline'ları, fabrikadaki montaj hatlarına benzer şekilde düşünebilirsiniz, burada montaj hattındaki her öğe aşamalarda inşa edilir. Kısmen monte edilmiş öğe bir montaj aşamasından diğerine geçirilir. Montaj hattının çıktıları, girişlerin sırasıyla aynı sırayla oluşur.

Gerçek dünya örneği

> Bir dizeyi bir dizi filtreleme aşamasından geçirip son aşamada bir karakter dizisine dönüştürmek istediğimizi düşünelim.

Basit bir ifadeyle

> Pipeline deseni, kısmi sonuçların bir aşamadan diğerine geçtiği bir montaj hattıdır.

Wikipedia diyor ki

> Yazılım mühendisliğinde, bir pipeline, her bir öğenin çıktısının bir sonrakinin girdisi olduğu bir işleme zinciri (işlemler, iş parçacıkları, koşut işlemler, işlevler vb.) dizisinden oluşur; adı fiziksel bir boruya benzetmeyle alınmıştır.

**Programatik Örnek**

Pipeline'ın aşamalarına `Handler` denir.

```csharp
public interface IHandler<in TInput, out TOutput> {
    TOutput Process(TInput input);
}
```

İşleme örneğimizde 3 farklı somut `Handler` bulunmaktadır.

```csharp
public sealed class RemoveAlphabetsHandler : IHandler<String, String> {
  ...
}

public sealed class RemoveDigitsHandler : IHandler<String, String> {
  ...
}

public sealed class ConvertToCharArrayHandler : IHandler<String, Char[]> {
  ...
}
```

İşte, handler'ları toplayacak ve tek tek çalıştıracak olan `Pipeline`.

```csharp
public sealed class Pipeline<TInput, TOutput> {
    private readonly Func<TInput, TOutput> currentHandler;

    public Pipeline(Func<TInput, TOutput> currentHandler) {
        this.currentHandler = currentHandler;
    }

    public Pipeline(IHandler<TInput, TOutput> currentHandler) : this(x => currentHandler.Process(x)) { }

    public Pipeline<TInput, TNextOutput> Then<TNextOutput>(Func<TOutput, TNextOutput> nextHandler) {
        Func<TInput, TNextOutput> combinedHandler = input => nextHandler(this.currentHandler(input));
        return new Pipeline<TInput, TNextOutput>(combinedHandler);
    }

    public Pipeline<TInput, TNextOutput> Then<TNextOutput>(IHandler<TOutput, TNextOutput> nextHandler) {
        Func<TInput, TNextOutput> combinedHandler = input => nextHandler.Process(this.currentHandler(input));
        return new Pipeline<TInput, TNextOutput>(combinedHandler);
    }

    public TOutput Execute(TInput input) {
        return this.currentHandler(input);
    }
}
```

Ve işte dizeyi işleyen `Pipeline`ın çalışması.

```csharp
    Pipeline<String, Char[]> filters = new Pipeline<String, String>(new RemoveAlphabetsHandler())
    .Then(new RemoveDigitsHandler())
    .Then(new ConvertToCharArrayHandler());
    filters.Execute("filtered123!"); // => !

// Or

    Pipeline<String, String> filters = new(x => x.ToUpper());
    filters.Execute("filtered");
```

## Class diagram

"-"

## Uygulanabilirlik

Pipeline desenini aşağıdaki durumlarda kullanabilirsiniz:

* Son bir değer üreten aşamaları gerçekleştirmek istediğinizde.
* Karmaşık işlemlerin okunabilirliğini sağlamak için akıcı bir yapı oluşturarak kullanmak istediğinizde.
* Kodun test edilebilirliğini artırmak istediğinizde, çünkü aşamalar genellikle tek bir işi yapacak şekilde tasarlanır ve 
[Single Responsibility Principle (SRP)](https://csharp-design-patterns.com/principles/#single-responsibility-principle) prensibine uygun olacaktır.