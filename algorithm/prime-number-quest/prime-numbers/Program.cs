using prime_numbers;
using System.Diagnostics;

Action test = () => {
    Int32 number = default;
    Boolean status = default;
    String statusText = String.Empty, result = String.Empty;

    while(true) {
        Console.Write($"Bir sayı giriniz: ");
        try {
            number = Convert.ToInt32(Console.ReadLine());
            if(number < 0)
                throw new OverflowException();

            Stopwatch stopwatch = new();
            stopwatch.Start();
            status = PrimeNumber.IsPrime(number);
            stopwatch.Stop();
            Console.WriteLine($"--> {stopwatch.ElapsedTicks}");
            statusText = status ? "Asal sayıdır" : "Asal sayı değildir";
            result = $"{number}: {statusText}";
        }
        catch(OverflowException) {
            result = $"\nGirilen sayı 0 - {Int32.MaxValue} aralığında olmalıdır\n";
        }

        Console.WriteLine(result);
    }
};

test.Invoke();