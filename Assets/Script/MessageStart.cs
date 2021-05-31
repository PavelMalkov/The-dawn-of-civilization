using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;

public class MessageStart : MonoBehaviour
{
    [SerializeField]
    GameObject Bg;

    [SerializeField]
    GameObject PanelAnim;

    [SerializeField]
    Button OK;

    [SerializeField]
    Button Ads;

    [SerializeField]
    Button Bay;

    [SerializeField]
    TextMeshProUGUI CountGold;
    [SerializeField]
    TextMeshProUGUI CountSince;
    [SerializeField]
    TextMeshProUGUI CountMaterial;
    [SerializeField]
    TextMeshProUGUI CountFood;

    float MoneySec, ScienceSec, MaterialSec, FoodSec;
    float Money, Science, Material, Food;
    float timeSecond, timeMin; // минуты это + часы + дни + года

    private void Start()
    {
        Preferense.Play = false;
        if (Bg != null) Bg.SetActive(true);
        if (PanelAnim != null) PanelAnim.SetActive(true);
        //Time.timeScale = 0f;
        
        TimeSpan tm;

        if (PlayerPrefs.GetString("LastSession") == "") CloseFirst();
        else if (PlayerPrefs.HasKey("LastSession") && DateTime.UtcNow > DateTime.Parse(PlayerPrefs.GetString("LastSession")))
        {
            tm = DateTime.UtcNow - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            timeMin = tm.Minutes + tm.Hours * 60 + tm.Days * 60 * 24;
            timeSecond = tm.Seconds;
            MoneySec = MoneyPerSecond(0);
            ScienceSec = MoneyPerSecond(1);
            MaterialSec = MoneyPerSecond(2);
            FoodSec = MoneyPerSecond(3);

            Money = (MoneySec * timeSecond + MoneySec * timeMin * 60) / Preferense.Div;
            Science = (ScienceSec * timeSecond + ScienceSec * timeMin * 60) / Preferense.Div;
            Material = (MaterialSec * timeSecond + MaterialSec * timeMin * 60) / Preferense.Div;
            Food = (FoodSec * timeSecond + FoodSec * timeMin * 60) / Preferense.Div;

            print("Зарабатываем " + MoneySec + " в секунду монет");
            print("Зарабатываем " + ScienceSec + " в секунду монет");
            print("Зарабатываем " + MaterialSec + " в секунду монет");
            print("Зарабатываем " + FoodSec + " в секунду монет");

            if (Money == 0 && Science == 0 && Material == 0 && Food == 0) Close();

            CountGold.text = TextControl.ConvertTxt(Money);
            CountSince.text = TextControl.ConvertTxt(Science);
            CountMaterial.text = TextControl.ConvertTxt(Material);
            CountFood.text = TextControl.ConvertTxt(Food);
        }
        else Close();
    }

    public void CloseFirst()
    {
        Preferense.Play = true;
        if (Bg != null) Bg.SetActive(false);
        if (PanelAnim != null) PanelAnim.SetActive(false);
        this.gameObject.SetActive(false);

        // старт игры 
    }

    // закрытие панели
    public void Close()
    {        
        Currency.Gold += Money;
        Currency.Science += Science;

        Preferense.Play = true;
        if (Bg != null) Bg.SetActive(false);
        if (PanelAnim != null) PanelAnim.SetActive(false);
        this.gameObject.SetActive(false);
    }

    //Сколько мы денег зарабатываем
    private float MoneyPerSecond(int Num)
    {
        float m1 = 0;
        foreach (Bild item in SaveControl.Bilds)
        {
            if (item.FactBay && item.NumCurrent == Num)
               m1 += item.Money / item.time;
        }
        return m1;
    }
}
