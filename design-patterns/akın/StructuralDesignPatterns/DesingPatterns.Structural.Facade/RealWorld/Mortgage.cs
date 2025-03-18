namespace DesingPatterns.Structural.Facade.RealWorld;
public sealed class Mortgage {
    private readonly Bank bank = new();
    private readonly Loan loan = new();
    private readonly Credit credit = new();

    public Boolean IsEligible(Customer customer, Int32 amount) {
        Console.WriteLine("{0} applies for {1:C} loan\n", customer.Name, amount);

        Boolean eligible = true;
        // Check creditworthyness of applicant
        if(!this.bank.HasSufficientSavings(customer, amount))
            eligible = false;

        else if(!this.loan.HasNoBadLoans(customer))
            eligible = false;

        else if(!this.credit.HasGoodCredit(customer))
            eligible = false;

        return eligible;
    }
}