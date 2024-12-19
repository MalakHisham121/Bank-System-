using System.Runtime.InteropServices.JavaScript;

namespace Bank_System;

public class User
{

    public int UserID { get; init; }
    public static int counter;
    public String UserName { get; set; }
    public String email{ get; set; }
    public List<records> Actions = new List<records>();
    public Account MyAccount;

    public User(String name,String email)
    {
        UserID = counter;
        counter++;
        UserName = name;
        this.email = email;
        
        
    }

    public static User CreateUser(int type)
    {
        
        Console.WriteLine("Please enter your Name");
        String name = Console.ReadLine();
        Console.WriteLine("Please enter your email");
        String mail = Console.ReadLine();
        
        
        return new User( name, mail);
    }
    
}