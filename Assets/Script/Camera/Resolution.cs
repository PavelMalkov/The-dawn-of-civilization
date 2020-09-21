using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.IO;


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

<<<<<<< HEAD
// Этот класс статический может хранить в себе значения он используется еще для конвертации чисел и их формата вывода
public class Data
=======
// Этот класс статический может хранить в себе значения
public static class Data
>>>>>>> parent of 59a305c... Изменения ценовой политики
{
    private static string[] teams = { "","K", "M", "B", "A", "Ax", "Ac" };

    public static float CountMoney = 0;
    public static float BildCount = 0; // количество зданий
    public static float X, Y;

    public static string ConvertTxt(float x)
    {

        int count = 0;
        float buf = x;
        while (buf > 1000)
        {
            count++;
            buf /= 1000;
        }
<<<<<<< HEAD
        string str1 = (buf.ToString("####0.##") + Data.teams[count]);
        return str1; // значение teams
    }
=======
        if (x >= 1000)
        {
            x /= 1000;
            str2 = "B";
        }
        str1 = x.ToString("####0.##") + str2;
        return str1;
    } 
>>>>>>> parent of 59a305c... Изменения ценовой политики
}




