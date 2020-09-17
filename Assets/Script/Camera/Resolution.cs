using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Resolution : MonoBehaviour
{
    private Save sv = new Save();

    [HideInInspector]
    public float X, Y;
    void Awake()
    {
        X = Screen.width;
        Y = Screen.height;
        print(X + " " + Y);
        Data.X = X;
        Data.Y = Y;
        if (PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            Data.count = sv.score;
        }
        else
        {
            print("Awake 1 раз в сохранении");
        }
    }

    private void OnApplicationPause() // работает при запуске
    {
        sv.score = Data.count;
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }

    private void OnApplicationQuit() // сохраняет но только когда выключено
    {
        sv.score = Data.count;
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
}

[Serializable]
public class Save
{
    public float score;

    //Здесь необходимо перести все данные которые мы хотим сохранить
}

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
