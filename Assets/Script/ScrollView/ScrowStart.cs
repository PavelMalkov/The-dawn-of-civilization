using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrowStart : MonoBehaviour
{
    public Scrollbar scrollbar;
    private bool close = true;
    // Start is called before the first frame update
    public void VALUE()
    {
        if (close) { scrollbar.value = 1; close = false; } // надо посоветоваться нужно ли всегда с начала включать
        else close = true;
    }
}
