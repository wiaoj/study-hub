namespace DesignPatterns.Creational.Prototype;
public sealed class Account : ICloneable {
    protected String iban;
    protected Decimal balance;
    protected Decimal credit;
    protected Customer owner;

    protected Boolean defaultAccount;
    private Boolean openToWithdraw;
    private Boolean openToPayment;
    private Boolean opentToTransfer;

    //Problem
    /*
     Tek kurucu metod içerisinde gereksiz birden çok parametre bulunuyor
     Bunların gerekli gereksiz etrafta bulunması doğru değildir
     */
    public Account(String iban,
                   Decimal balance,
                   Decimal credit,
                   Boolean defaultAccount,
                   Customer owner,
                   Boolean openToWithdraw,
                   Boolean openToPayment,
                   Boolean opentToTransfer) {
        this.iban = iban;
        this.balance = balance;
        this.credit = credit;
        this.defaultAccount = defaultAccount;
        this.owner = owner;
        this.openToWithdraw = openToWithdraw;
        this.openToPayment = openToPayment;
        this.opentToTransfer = opentToTransfer;
    }

    public Object Clone() {
        Account account = null;
        try {
            return account;
        }
        catch(Exception) {

            throw;
        }
    }
}