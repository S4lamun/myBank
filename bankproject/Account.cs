using Org.BouncyCastle.Bcpg;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ZstdSharp.Unsafe;

namespace bankproject;

public class Account : IComparable<Account>
{
    #region Variables

    private string password;
    private string login;
    public List<(DateTime, decimal, string)> transactions;
    
    #endregion


    public Account() { } // Nonparametric contructor


    public Account(BankCustomer owner, string password, decimal balance, string login)
    {
        this.Owner = owner;
        Password = password;
        Balance = balance;
        AccountNumber = BankAccountNumber;
        BankAccountNumber++;
        Login = login;
        transactions = new();
    } // Parametric contructor


    public List<string> DisplayTransactions()
    {
        List<string> transactionList = new();
        var sortedTransactions = transactions.OrderByDescending(t => t.Item1);
        foreach (var (date, amount, description) in sortedTransactions)
        {
            transactionList.Add($"{date:dd-MM-yyyy:HH:mm}, {amount:C}, {description}");
        }
        return transactionList;
    } // function to display all transactions in WPF


    public int CompareTo(Account? other)
    {
        if (other == null) return 1;
        return Balance.CompareTo(other.Balance);
    } // Comparing accounts on their balance


    public void Transfer(Account a1, decimal amount)
    {
        if (Balance < amount || amount <0) throw new WrongAmountException($"You don't have enough balance to transfer {amount:C}");

        a1.Balance += amount;
        Balance -= amount;
        transactions.Add((DateTime.Now, -amount, "Outgoing transfer"));
        a1.transactions.Add((DateTime.Now, amount, "Incoming transfer"));
    } // Making a transfer beetween accounts


    public void Deposit(decimal amount)
    {
        Balance += amount;
        transactions.Add((DateTime.Now, amount, "Deposit"));
    } // Making a deposit to account


    public void Withdraw(decimal amount)
    {
        if (Balance < amount || amount <0) throw new WrongAmountException($"You don't have enough balance to withdraw {amount:C}");
        Balance -= amount;
        transactions.Add((DateTime.Now, -amount, "Withdraw"));
    } // Making a withdraw from account 


    public override string ToString()
    {
        return $"{Owner}, Balance: {Balance:C}, Bank Account Number: {AccountNumber:D19}";
    } // Displaying Account data

    #region Static 
    private static long bankAccountNumber;
    static Account()
    {
        BankAccountNumber = 0000000000000000001;
    }
    #endregion


    #region Properties

    public string Password // password must have at least: 1 capital letter, 1 digit, 1 special character and must be at least 6 characters long    
    {
        get => password;
        init
        {
            if (!Regex.IsMatch(value, @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,}$"))
                throw new WrongPasswordException(
                    "Password must have at least 1 capital letter, 1 digit, and be at least 6 characters long (no special characters).");
            password = value;
        }
    }
    public long AccountNumber { get; init; } // AccountNumber can be only set in Classs
    public decimal Balance { get; set; }
    public BankCustomer Owner { get; set; }
    public static long BankAccountNumber { get => bankAccountNumber; set => bankAccountNumber = value; }
    public string Login { get => login; set => login = value; }

    #endregion
}