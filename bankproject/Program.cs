using System.Runtime.Intrinsics.X86;
using System.Data;
using System.Data.SqlClient;
using Mysqlx.Crud;

namespace bankproject;

internal class Program
{
    private static void Main()
    {
        Bank.CreateBank();

    }
}