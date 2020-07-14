using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountMoney : MonoBehaviour
{
    public Text MyText;
    public int count = 0;

    private void Start()
    {
        // Востановить данные сколько получаем монет
        StartCoroutine(SumEn());
    }

    public void OnMainBildClick()
    {
        Data.count++;
        MyText.text = Data.count.ToString();
    }

    IEnumerator SumEn()
    {
        while (true)
        {
            MyText.text = Data.count.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}
