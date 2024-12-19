using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Bank_System;

abstract public class Account : IRecord
{
    protected string AccountNumber { get; set; }
    protected User AccountOwner { get; set; }
    protected decimal Balance { get; set; }
    private IRecord _recordImplementation;

    public string GetAccountNumber()
    {
        return AccountNumber ?? throw new InvalidOperationException("Account number is not set.");
    }

    public User GetAccountOwner()
    {
        return AccountOwner ?? throw new InvalidOperationException("Account owner is not set.");
    }

    public decimal GetBalance()
    {
        return Balance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.");
        Balance += amount;
        records r = new records("deposit", amount, DateTime.Now);
        record(this.AccountOwner, r);
    }

    public void GetAccountDetails()
    {
        if (string.IsNullOrEmpty(AccountNumber)) throw new InvalidOperationException("Account details are incomplete.");
        if (AccountOwner == null) throw new InvalidOperationException("Account owner details are missing.");
        Console.WriteLine("The Account Number is " + AccountNumber);
        Console.WriteLine("The Account Owner is " + AccountOwner.UserName);
        Console.WriteLine("The Balance is " + Balance);
    }

    public abstract decimal Withdraw(decimal amount);

    public void record(User user, records r)
    {
        if (user == null ) throw new ArgumentNullException("User or record cannot be null.");
        user.Actions.Add(r);
    }
}

public class SavingAccount : Account
{
    public decimal interestRate { get; private set; }
    private DateTime LastDate;

    SavingAccount(User user, decimal interest)
    {
        if (user == null) throw new ArgumentNullException("User cannot be null.");
        if (interest <= 0) throw new ArgumentException("Interest rate must be greater than zero.");
        AccountNumber = "s" + user.UserID.ToString();
        AccountOwner = user;
        interestRate = interest;
        LastDate = DateTime.Now;
        Balance = 0;
        Admin.Accounts.Add(this);
        user.MyAccount = this;
    }

    public static SavingAccount CreateSaving(User user, decimal interest)
    {
        if (user == null) throw new ArgumentNullException("User cannot be null.");
        if (interest <= 0) throw new ArgumentException("Interest rate must be positive.");
        return new SavingAccount(user, interest);
    }

    public void AddInterest()
    {
        if (DateTime.Now < LastDate) throw new InvalidOperationException("Invalid date for interest calculation.");
        int gain = (DateTime.Now.Year - LastDate.Year) * 12 + DateTime.Now.Month - LastDate.Month;
        if (gain > 0)
        {
            decimal interest = Balance * (interestRate / 100) * (gain / 12.0m);
            if (interest < 0) throw new InvalidOperationException("Calculated interest is invalid.");
            Balance += interest;
            records r = new records("interest", interest, DateTime.Now);
            record(this.AccountOwner, r);
            LastDate = DateTime.Now;
        }
    }

    public override decimal Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdraw amount must be greater than zero.");
        if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        records r = new records("withdraw", amount, DateTime.Now);
        record(this.AccountOwner, r);
        return Balance;
    }
}

public class CheckingAccount : Account
{
    private decimal OverDraft;

    CheckingAccount(User user, decimal overDraft)
    {
        if (user == null) throw new ArgumentNullException("User cannot be null.");
        if (overDraft < 0) throw new ArgumentException("Overdraft limit must be non-negative.");
        AccountNumber = "c" + user.UserID.ToString();
        AccountOwner = user;
        Balance = 0;
        OverDraft = overDraft;
        Admin.Accounts.Add(this);
        user.MyAccount = this;
    }

    public static CheckingAccount CreateChecking(User user, decimal overdraft)
    {
        if (user == null) throw new ArgumentNullException("User cannot be null.");
        if (overdraft < 0) throw new ArgumentException("Overdraft must be non-negative.");
        return new CheckingAccount(user, overdraft);
    }

    public override decimal Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdraw amount must be greater than zero.");
        if (Balance - amount < -OverDraft) throw new InvalidOperationException("Overdraft limit exceeded.");
        Balance -= amount;
        records r = new records("withdraw", amount, DateTime.Now);
        record(this.AccountOwner, r);
        return Balance;
    }
}

