using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace bankproject
{
    public class PeriodicTransfer
    {
        #region Variables
        [XmlIgnore]
        public Account SourceAccount { get; set; } // account from
        [XmlIgnore]
        public Account DestinationAccount { get; set; } // account to
        public decimal Amount { get; set; } // amount of money to transfer
        public DateTime NextTransfer { get; set; } // date of next transfer 
        public int Interval { get; set; } // how often this transfer must be made (Days)
        #endregion

        public PeriodicTransfer() { } // Non-parametric contructor

        public PeriodicTransfer(Account sourceAccount, Account destinationAccount, decimal amount, int interval)
        {
            SourceAccount = sourceAccount;
            DestinationAccount = destinationAccount;    
            if(amount <= 0 )
            {
                throw new WrongAmountException("Invalid Amount!");
            }
            Amount = amount;
            Interval = interval;
            NextTransfer = DateTime.Now.AddDays(interval);
        }

        public void ExecuteTransfer()
        {
            if (DateTime.Now >= NextTransfer)
            {
                SourceAccount.Transfer(DestinationAccount, Amount);
                NextTransfer = DateTime.Now.AddDays(Interval);
            }

        }

        public override string ToString()
        {
            return $"Number: {DestinationAccount.AccountNumber}, Amount: {Amount:C}, Next Transfer: {NextTransfer:dd-MM-yyyy}";
        }
    }
}
