namespace Bank_System;

class Program
{
    static void Main(string[] args)
    {
        User.counter = 1;
         Console.WriteLine("Testing the Bank System");

            // Create a User
            User user1 = new User("Alice","alice@gmail.com");
            User user2 = new User("Bob","bob@gmail.com");

            // Create a SavingAccount for Alice
            SavingAccount savingAccount = SavingAccount.CreateSaving(user1,5);
            savingAccount.Deposit(5000);
            Console.WriteLine("\nSaving Account Details After Deposit");
            savingAccount.GetAccountDetails();

            // Add interest to the Saving Account
            savingAccount.AddInterest();
            Console.WriteLine("\nSaving Account Details After Adding Interest");
            savingAccount.GetAccountDetails();

            // Withdraw money from the Saving Account
            savingAccount.Withdraw(1000);
            Console.WriteLine("\nSaving Account Details After Withdrawal");
            savingAccount.GetAccountDetails();

            // Create a CheckingAccount for Bob
            CheckingAccount checkingAccount = CheckingAccount.CreateChecking(user2,3000);
            Console.WriteLine("\nChecking Account Created ");
            checkingAccount.Deposit(2000);
            checkingAccount.GetAccountDetails();

            // Withdraw within overdraft limit
            checkingAccount.Withdraw(2500);
            Console.WriteLine("\nChecking Account Details After Withdrawal (Overdraft) ");
            checkingAccount.GetAccountDetails();

            // Withdraw exceeding overdraft limit
            checkingAccount.Withdraw(1000);
            Console.WriteLine("\nChecking Account Details After Exceeding Overdraft ");
            checkingAccount.GetAccountDetails();

            // Display all user actions
            Console.WriteLine("\nUser Actions for Alice ");
            foreach (var action in user1.Actions)
            {
                Console.WriteLine($"{action.type} of {action.value:C} on {action.time}");
            }

            Console.WriteLine("\n User Actions for Bob ");
            foreach (var action in user2.Actions)
            {
                Console.WriteLine($"{action.type} of {action.value:C} on {action.time}");
            }
    }
}