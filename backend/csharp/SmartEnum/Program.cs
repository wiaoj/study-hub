using SmartEnum;
/* 
public enum CreditCard {
    Standard = 1,
    Premium = 2,
    Platinum = 3
}

CreditCard creditCard = CreditCard.Platinum;

Double discount = creditCard switch {
    CreditCard.Standard => 0.01,
    CreditCard.Premium => 0.05,
    CreditCard.Platinum => 0.1
};  
*/



CreditCard? creditCard = CreditCard.FromName(CreditCard.Premium.Name);
CreditCard platinumCard = CreditCard.FromEnumeration(CreditCard.Platinum);

Console.WriteLine($"Discount for {creditCard} is {creditCard?.Discount:P}");
Console.WriteLine($"Discount for {platinumCard} is {platinumCard.Discount:P}");

Console.ReadKey();
