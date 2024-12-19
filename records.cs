using System.Runtime.InteropServices.JavaScript;

namespace Bank_System;


public struct records
{
    public String type { get; }
    public decimal value { get; }
    public DateTime time { get; }

    public records(String type,decimal v, DateTime time)
    {
        this.type = type;
        this.value = v;
        this.time = time;
    }

}