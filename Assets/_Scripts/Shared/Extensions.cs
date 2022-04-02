
using System;
using UnityEngine;

public static class Extensions
{
    public static TimeSpan InSec(this float num)
    {
        return TimeSpan.FromSeconds(num);
    }

    public static float Lenght(this Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(vector.x), 2) 
                        + Mathf.Pow(Mathf.Abs(vector.y), 2)
                        + Mathf.Pow(Mathf.Abs(vector.z), 2));
    }
}

