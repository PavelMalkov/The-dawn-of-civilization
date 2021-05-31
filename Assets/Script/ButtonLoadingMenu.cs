using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadingMenu : MonoBehaviour
{
    [SerializeField]
    GameObject Circl;
    public void Press()
    {
        Circl.SetActive(true);
    }
}
