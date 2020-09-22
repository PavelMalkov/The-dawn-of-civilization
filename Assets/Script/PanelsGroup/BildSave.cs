using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BildSave
{
    public float CountMoney;
    //public float MoneyPerSecond; // Это сколько денег в секунду мы зарабатываем (скорость при выключенной игре сделаем в 50 раз медленнее)
    public string DateLast;
    public List<Bild> ManyBuildingLocal; // это список наших домов
    public BildSave() { CountMoney = 0; }
}