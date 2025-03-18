---
title: Observer
category: Behavioral
language: en
tag:
 - Gang Of Four
 - Reactive
---

## Diğer adıyla

Bağımlılar, Yayın-Abone

## Amaç

Nesneler arasında birbirine bağımlılık oluşturarak, bir nesne durumunu değiştirdiğinde tüm bağımlılarının otomatik olarak bildirilmesini ve güncellenmesini sağlamak.

## Açıklama

Gerçek dünya örneği

> Uzak bir diyarda hobbitler ve orklar yaşar. Her ikisi de genellikle açık havada oldukları için hava değişikliklerini yakından takip ederler. Biri sürekli olarak hava durumunu gözlemliyor diyebiliriz.

Basit bir ifadeyle

> Bir nesnenin durum değişikliklerini almak için bir gözlemci olarak kaydolun.

Wikipedia diyor ki

> Gözlemci deseni, bir nesnenin (konu) bağımlılarının (gözlemciler) bir listesini tutarak, genellikle onların yöntemlerinden birini çağırarak herhangi bir durum değişikliğini otomatik olarak bildirir.

**Programatik Örnek**

Öncelikle `WeatherObserver` arayüzünü ve ırklarımızı, `Orcs` ve `Hobbits`'i tanıtalım.

```csharp
public interface IWeatherObserver {
    String Update(WeatherType currentWeather);
}

public class Orcs : IWeatherObserver {
    public String Update(WeatherType currentWeather) {
        return $"The orcs are facing {currentWeather.GetDescription()} weather now";
    }
}

public class Hobbits : IWeatherObserver {
    public String Update(WeatherType currentWeather) {
        return $"The hobbits are facing {currentWeather.GetDescription()} weather now";
    }
}
```

Ardından sürekli değişen `Weather` (Hava Durumu) şöyledir.

```csharp
public class Weather {
    private WeatherType currentWeather;
    private readonly List<IWeatherObserver> weatherObservers;

    public WeatherType Current => currentWeather;

    public Weather() {
        this.weatherObservers = [];
        this.currentWeather = WeatherType.Sunny;
    }

    public void AddObserver(IWeatherObserver weatherObserver) {
        this.weatherObservers.Add(weatherObserver);
    } 
    
    public void AddObservers(IEnumerable<IWeatherObserver> weatherObservers) {
        this.weatherObservers.AddRange(weatherObservers);
    }

    public void RemoveObserver(IWeatherObserver weatherObserver) {
        this.weatherObservers.Remove(weatherObserver);
    }

    public void TimePasses() {
        ChangeCurrentWeather();
        NotifyObservers();
    }

    private void ChangeCurrentWeather() {
        this.currentWeather = (WeatherType)(((Int32)this.currentWeather + 1) % Enum.GetValues(typeof(WeatherType)).Length);
    }

    private void NotifyObservers() {
        this.weatherObservers.ForEach(weatherObserver => weatherObserver.Update(this.currentWeather));
    }
}
```

Aşağıda tam örneği görebilirsiniz.

```csharp
    Weather weather = new();
    weather.addObserver(new Orcs());
    weather.addObserver(new Hobbits());
    weather.timePasses();
    weather.timePasses();
    weather.timePasses();
    weather.timePasses();
```

Program output:

```
The weather changed to rainy.
The orcs are facing rainy weather now
The hobbits are facing rainy weather now
The weather changed to windy.
The orcs are facing windy weather now
The hobbits are facing windy weather now
The weather changed to cold.
The orcs are facing cold weather now
The hobbits are facing cold weather now
The weather changed to sunny.
The orcs are facing sunny weather now
The hobbits are facing sunny weather now
```

## Class diagram

"-"

## Uygulanabilirlik

Gözlemci desenini aşağıdaki durumlardan herhangi birinde kullanın:

* Bir soyutlama, birbirine bağımlı iki yönü olan durumlarda. Bu yönleri ayrı nesnelerde kapsülleyerek, bunları bağımsız olarak değiştirebilir ve yeniden kullanabilirsiniz.
* Bir nesnenin değişiklik yapılması diğer nesnelerin değiştirilmesini gerektiriyorsa ve kaç nesnenin değiştirilmesi gerektiğini bilmiyorsanız.
* Bir nesnenin, bu nesnelerin kim olduğu hakkında varsayımlar yapmadan diğer nesneleri bildirebilmesi gerekiyorsa. Başka bir deyişle, bu nesnelerin sıkı bir şekilde bağlı olmasını istemiyorsunuz.