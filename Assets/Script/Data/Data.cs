using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static float count = 0;
    public static float X, Y;

    public static string ConvertTxt(float x)
    {
        string str1 = ""; // это строка чисел
        string str2 = ""; // это строка с буквой

        if (x >= 1000)
        {
            x /= 1000;
            str2 = "К";
        }
        if (x >= 1000)
        {
            x /= 1000;
            str2 = "M";
        }
        if (x >= 1000)
        {
            x /= 1000;
            str2 = "B";
        }
        str1 = x.ToString("####0.##") + str2;
        return str1;
    }
}
