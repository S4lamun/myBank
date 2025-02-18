using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;


namespace bankproject;

public class BankEmployee : Person // Class which represents BankEmployee (one of the admin which can make changes in account)
{
    #region Variables
    string employeeID; // Employee's login  ID is FirstLetterOfName|FirstLetterOfSurrname/00/Counter
    string employeePassword; // Employee's password
    #endregion


    public BankEmployee() { } // Non-parametric contructor

    public BankEmployee(string name, string surname, string pesel, EnumSex sex, string employeePassword) : base(name, surname, pesel, sex)
    {
        EmployeePassword = employeePassword;
        EmployeeID = $"{char.ToUpper(name[0])}{char.ToUpper(surname[0])}/00/{Counter}";
        Counter++;
    } // Parametric Contructor

    public override string ToString()
    {

        return base.ToString() + $", Employee ID: {EmployeeID}";
    } // Displaying Employee data

    #region Static
    static int counter;
    static BankEmployee()
    {
        Counter = 1;
    }

    #endregion

    #region Properties
    public string EmployeeID { get => employeeID; set => employeeID = value; }
    public string EmployeePassword { get => employeePassword; set => employeePassword = value; }
    public static int Counter { get => counter; set => counter = value; }
    #endregion

}