using System.Diagnostics;

namespace prime_numbers;
public static class PrimeNumber {
    private const Int32 BASE_PRIME = 0x2;
    private const Int32 START_DIVISOR = 0x3;

    public static Boolean IsPrime(Int32 number) {
        if(IsBasePrime(number))
            return true;

        return !IsLessThanBasePrime(number) && !IsEven(number) && IsPrimeByDivisors(number);
    }

    private static Boolean IsBasePrime(Int32 number) {
        return number is BASE_PRIME;
    }

    private static Boolean IsLessThanBasePrime(Int32 number) {
        return number < BASE_PRIME;
    }

    private static Boolean IsEven(Int32 number) {
        return number % BASE_PRIME is default(Int32);
    }

    private static Boolean IsPrimeByDivisors(Int32 number) {
        Double sqrtNumber = Math.Sqrt(number); 

        for(Int32 divisor = START_DIVISOR; divisor <= sqrtNumber; divisor += BASE_PRIME)
            if(number % divisor is default(Int32))
                return false;

        return true;
    }
}