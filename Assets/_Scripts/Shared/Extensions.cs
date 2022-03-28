
using System;

public static class Extensions
{
    public static TimeSpan InSec(this float num)
    {
        return TimeSpan.FromSeconds(num);
    }
}

