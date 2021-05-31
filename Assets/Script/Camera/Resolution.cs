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
        Currency.X = X;
        Currency.Y = Y;
        Debug.Log(Application.persistentDataPath);
    }
}


