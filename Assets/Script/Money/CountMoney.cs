using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountMoney : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI ScienseText;
    private void Start()
    {
        // Старт обновления денег, для того чтобы обновлясь в 10 кардров в секунду
        StartCoroutine(SumEn());
    }

    IEnumerator SumEn()
    {
        while (true)
        {            
            GoldText.text = TextControl.ConvertTxt(Currency.Gold);
            ScienseText.text = TextControl.ConvertTxt(Currency.Science);
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
