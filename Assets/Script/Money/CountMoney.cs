using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountMoney : MonoBehaviour
{
    public Text MyText;
    public int count = 0;
    //int CostOneClick;

    // Start is called before the first frame update
    void Start()
    {
        //CostOneClick = 1;
        //count = int.Parse(MyText.text);
    }

    // Update is called once per frame
    void Update()
    {
        /*count++;
        MyText.text = count.ToString();*/
    }

    public void OnMainBildClick()
    {
        count++;
        MyText.text = count.ToString();
    }
}
