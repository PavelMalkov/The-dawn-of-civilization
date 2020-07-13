using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Resolution : MonoBehaviour
{
    public int X, Y;
    void Start()
    {     
       X = Screen.width;
       Y = Screen.height;
       print(X + " " + Y);
    }
}
