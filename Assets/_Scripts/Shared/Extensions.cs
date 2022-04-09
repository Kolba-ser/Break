
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

    public static bool InBounds<T>(this int index, T[] array)
    {
        if(index >= 0 && index < array.Length)
        {
            return true;
        }

        return false;
    }

    public static bool IsEmpty(this string text)
    {
        if (text.Length > 0)
            return false;

        return true;
    }

    public static bool AsBool(this int num)
    {
        float temp = num;
        num = (int)Mathf.Clamp01(temp);

        return Convert.ToBoolean(num);
    }
}

