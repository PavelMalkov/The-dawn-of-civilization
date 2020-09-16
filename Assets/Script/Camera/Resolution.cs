using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Resolution : MonoBehaviour
{
    [HideInInspector]
    public int X, Y;
    void Awake()
    {     
        X = Screen.width;
        Y = Screen.height;
        print(X + " " + Y);
        Data.X = X;
        Data.Y = Y;
    }
}
