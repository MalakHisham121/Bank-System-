namespace Bank_System;

static class Admin 
{
     public static String AdminName;
     public static List<Account> Accounts = new List<Account>();

    public static void ViewAllAccounts()
    {
        foreach (var account in Accounts)
        {
            Console.WriteLine("Account Code is : "+ account.GetAccountNumber());
            Console.WriteLine("Account owner is : "+ account.GetAccountOwner());
            Console.WriteLine("Account Balance is : "+ account.GetBalance());
        }
        
    }

    public static void CloseAccount(ref Account  account)
    {
        
        if (account == null)
        {
            Console.WriteLine("The account reference is null.");
            return;
        }

        if (account.GetBalance() > 0)
        {
            Console.WriteLine($"Closing account with remaining balance of {account.GetBalance():C}. Please withdraw the balance first.");
            return;
        }

        Accounts.Remove(account);
        Console.WriteLine($"Account {account.GetBalance()} belonging to {account.GetAccountOwner().UserName} has been closed.");

        
        account = null;
    }
}