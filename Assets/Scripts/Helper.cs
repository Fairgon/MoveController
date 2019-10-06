using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Helper
{
    // Изменяет значение с постоянной скоростью.
    public static float FloatLerp(ref float from, float to, float speed)
    {
        if (to == 0 && from <= 0)
            from = to;
        else if (from > to)
            from -= speed;
        else if (from < to)
            from += speed;

        float imprecision = 0.05f;

        if (Mathf.Abs(from - to) < imprecision)
            from = to;
        
        return from;
    }
    
    /// <summary>
    /// Парсит строку в целочисленный масив.
    /// </summary>
    /// <param name="value">Строка которую нужно парсить.</param>
    /// <param name="ch">Разделяющий символ.</param>
    /// <returns>int[]</returns>
    public static int[] ParseToIntArray(string value, char ch)
    {
        string[] str = value.Split(new char[] { ch });

        int[] array = new int[str.Length];

        for (int i = 0; i < str.Length; ++i)
        {
            array[i] = int.Parse(str[i]);
        }

        return array;
    }

    /// <summary>
    /// Парсит строку в масив с плавабщей точкой.
    /// </summary>
    /// <param name="value">Строка которую нужно парсить.</param>
    /// <param name="ch">Разделяющий символ.</param>
    /// <returns>int[]</returns>
    public static float[] ParseToFloatArray(string value, char ch)
    {
        string[] str = value.Split(new char[] { ch });

        float[] array = new float[str.Length];

        for (int i = 0; i < str.Length; ++i)
        {
            array[i] = float.Parse(str[i]);
        }

        return array;
    }

    /// <summary>
    /// Парсит строку в масив.
    /// </summary>
    /// <param name="value">Исходная строка.</param>
    /// <param name="ch">Разделяющий символ.</param>
    /// <returns>string[]</returns>
    public static string[] ParseToStringArray(string value, char ch)
    {
        return value.Split(new char[] { ch });
    }

    public static Vector3 CustomNormalize(Vector3 v)
    {
        double m = System.Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        if (m > 9.99999974737875E-06)
        {
            float fm = (float)m;
            v.x /= fm;
            v.y /= fm;
            v.z /= fm;
            return v;
        }
        else return Vector3.zero;
    }
    
    public static float Distance(Vector3 a, Vector3 b)
    {
        a = new Vector3(a.x, 0, a.z);
        b = new Vector3(b.x, 0, b.z);

        return Vector3.Distance(a, b);
    }

    /// <summary>
    /// Возвращает угол в градусах, позволяет получать отрицательные углы.
    /// </summary>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public static Vector3 EulerAngle(Quaternion quaternion)
    {
        float x = quaternion.eulerAngles.x;
        float y = quaternion.eulerAngles.y;
        float z = quaternion.eulerAngles.z;

        if (x > 180) x -= 360;
        if (y > 180) y -= 360;
        if (z > 180) z -= 360;

        return new Vector3(x, y, z);
    }
}

public class Range<T> where T : IComparable
{
    public T Minimum { get; set; }
    public T Maximum { get; set; }

    public bool Contains(T value)
    {
        bool isContained = Minimum.CompareTo(value) <= 0
         & Maximum.CompareTo(value) >= 0;
        return isContained;
    }

    public Range(T singleValue)
    {
        Minimum = singleValue;
        Maximum = singleValue;
    }

    public Range(T min, T max)
    {
        Minimum = min;
        Maximum = max;
    }
}
