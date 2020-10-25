using UnityEngine;

public class Resolution : MonoBehaviour
{

    [HideInInspector]
    public float X, Y;
    void Awake()
    {
        X = Screen.width;
        Y = Screen.height;
        print(X + " " + Y);
        Data.X = X;
        Data.Y = Y;
        Debug.Log(Application.persistentDataPath);
    }
}

// Этот класс статический может хранить в себе значения он используется еще для конвертации чисел и их формата вывода
public class Data
{
    private static string[] teams = { "","K", "M", "B", "A", "Ax", "Ac" };

    public static float CountMoney = 0;
    public static int BildCount = 0; // количество зданий
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
        string str1 = (buf.ToString("####0.##") + Data.teams[count]);
        return str1; // значение teams
    }
}




