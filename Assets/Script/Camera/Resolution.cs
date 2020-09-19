using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Resolution : MonoBehaviour
{
    //Размеры поля телефона (X - ширина, Y - высота)
    [HideInInspector]
    public int X, Y;
    void Awake()
    {     
        X = Screen.width; // Находим размеры ширины
        Y = Screen.height; // Находим размеры высоты
        print(X + " " + Y);
        Data.X = X; // Записываем данные в статический класс который хранит основные данные
        Data.Y = Y;  
    }
}
