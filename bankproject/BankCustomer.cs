namespace bankproject;

public class BankCustomer : Person // Class which represents owner of Account
{
    public BankCustomer() { }

    public BankCustomer(string name, string surname, string pesel, EnumSex sex) : base(name, surname, pesel, sex) { }

}