
using Break.Weapons;
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
        if (index >= 0 && index < array.Length)
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

    public static void Direct(this Weapon weapon, Transform target)
    {
        var targetDirection = (target.position - weapon.transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(targetDirection, Vectors.Up);

        weapon.transform.rotation = Quaternion.Lerp(weapon.transform.rotation, targetRotation, weapon.RotationSpeed);
    }

    public static void Direct(this Weapon weapon, Vector3 point)
    {
        var targetDirection = (point - weapon.transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(targetDirection, Vectors.Up);

        weapon.transform.rotation = Quaternion.Lerp(weapon.transform.rotation, targetRotation, weapon.RotationSpeed);
    }
    /// <summary>
    /// Возвращает процент числа от max. min = 0
    /// </summary>
    /// <param name="num"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float InPercent(this float num, float max)
    {
        return num / max;
    }
    /// <summary>
    /// Возвращает процент от числа в интервале от min до max
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float InPercent(this float num, float min, float max)
    {
        var clampNum = Mathf.Clamp(num, min, max); 
        var onePercent = (max - min) / 100;
        var intervalMagnitude = clampNum - min;

        return (intervalMagnitude / onePercent) / 100;
    }
}

