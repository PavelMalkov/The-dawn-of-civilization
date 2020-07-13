using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    long CountMoney;
    string Period;
    
    void Start()
    {
        // даные которые сохраняются в игре
        // эти данные мы должны востанавливать при запуске игры
        CountMoney = 0;
        Period = "Племенное поселение";
    }

    
    void Clouse()
    {
        GameObject CountMoney = GameObject.Find("Bild");
        CountMoney Count = CountMoney.GetComponent<CountMoney>();
        print(Count.count);
    }
}
