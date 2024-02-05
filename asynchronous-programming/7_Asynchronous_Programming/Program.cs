
// Async Factory Method

/*
Foo foo = await Foo.CreateAsync();

public class Foo {
    private Foo() { }

    public async Task<Foo> InitAsync() {
        await Task.Delay(1000);
        return this;
    }

    public static Task<Foo> CreateAsync() {
        Foo result = new();
        return result.InitAsync();
    }
}
*/

//Asynchronous Initialization Pattern

//MyClass myClass = new();
//MyOtherClass myOtherClass = new(myClass);

//await myOtherClass.InitAsync;

//public interface IAsyncInit {
//    Task InitAsync { get; }
//}

//public class MyClass : IAsyncInit {
//    public Task InitAsync { get; }

//    public MyClass() {
//        this.InitAsync = Init();
//    }


//    private async Task Init() {
//        await Task.Delay(1000);
//    }
//}

//public class MyOtherClass : IAsyncInit {
//    private readonly MyClass myClass;
//    public Task InitAsync { get; }

//    public MyOtherClass(MyClass myClass) {
//        this.myClass = myClass;
//        this.InitAsync = Init();
//    }


//    private async Task Init() {
//        if(this.myClass is IAsyncInit asyncInit)
//            await asyncInit.InitAsync;

//        await Task.Delay(1000);
//    }
//}

//Asynchronous Lazy Initialization

public class Stuff {
    private static Int32 value;

    private readonly Lazy<Task<Int32>> AutoIncValue = new(async () => {
        await Task.Delay(1000).ConfigureAwait(false);
        return value++;
    });

    private readonly Lazy<Task<Int32>> AutoIncValue2 = new(Task.Run(async () => {
        await Task.Delay(1000);
        return value++;
    }));


    ////Nito.AsyncEx
    //private static AsyncLazy<Int32> AutoIncValue3 = new(async () => {
    //    await Task.Delay(1000);
    //    return value++;
    //});


    public async Task UseValue() {
        int value = await AutoIncValue.Value;
    }
    
    public async Task UseValue2() {
        int value = await AutoIncValue2.Value;
    }
}