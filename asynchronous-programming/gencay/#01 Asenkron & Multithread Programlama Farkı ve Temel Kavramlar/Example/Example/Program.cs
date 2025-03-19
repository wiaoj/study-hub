using System.Threading.Tasks;

X();
Y();

Console.ReadLine();

async Task X() {
   await Task.Run(() => {
        for(int i = 0; i < 1000; i++) {
            Console.WriteLine($"X - {i}");
        }
    });
}

async Task Y() {
    await Task.Run(() => {
        for(int i = 0; i < 1000; i++) {
        Console.WriteLine($"Y - {i}");
        }
    });
}