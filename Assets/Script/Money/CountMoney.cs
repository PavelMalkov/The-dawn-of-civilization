using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountMoney : MonoBehaviour
{
    public Text MyText;
    public float count = 0;

    private void Start()
    {
        // Востановить данные сколько получаем монет
        count = Data.count;
        // Старт обновления денег
        StartCoroutine(SumEn());
    }

    IEnumerator SumEn()
    {
        while (true)
        {
            MyText.text = Data.ConvertTxt(Data.count);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
