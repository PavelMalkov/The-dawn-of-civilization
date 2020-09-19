using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Resolution : MonoBehaviour
{
    //Размеры поля телефона (X - ширина, Y - высота)
    [HideInInspector]
    public float X, Y;
    void Awake()
    {     
        X = Screen.width; // Находим размеры ширины
        Y = Screen.height; // Находим размеры высоты
        print(X + " " + Y);
        Data.X = X;
        Data.Y = Y;
        Debug.Log(Application.persistentDataPath);
    }
}

// Этот класс статический может хранить в себе значения
public static class Data
{
    public static float CountMoney = 0;
    public static float BildCount = 0; // количество зданий
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




