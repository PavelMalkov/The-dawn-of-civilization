using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Resolution : MonoBehaviour
{
    [HideInInspector]
    public int X, Y;
    void Start()
    {     
       X = Screen.width;
       Y = Screen.height;
       print(X + " " + Y);
    }
}
