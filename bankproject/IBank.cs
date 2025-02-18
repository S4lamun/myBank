namespace bankproject;

internal interface IBank
{
    public void AddAccount(Account account);
    public void RemoveAccount(Account account);
    public string DisplayAllAccounts();
    public Account FindAccount(long accountNumber);
    public Account? FindAccount(string login, string password);
    public string DisplayAllEmployess();
    public void AddEmployee(BankEmployee employee);
    public void RemoveEmployee(BankEmployee employee);
    public BankEmployee? FindEmployee(string id, string password);
}