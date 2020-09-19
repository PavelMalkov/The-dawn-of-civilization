using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Research : MonoBehaviour
{
    // Это объекты которые изменяются и привязываются в самом префабе поэтому они скрыты
    [HideInInspector]
    public Image BildImage;
    [HideInInspector]
    public Text Name;
    [HideInInspector]
    public Text About;
    [HideInInspector]
    public Text txtCost;
    [HideInInspector]
    public Button Bay;

    // вот эти данные должны храниться
    public int Id;
    public string ReseachName;
    public string ReseachAbout;
    public Sprite ReseachImage;
    public float CostBay;

    private bool FactBay = false;


    void Start()
    {
        FactBay = false; // нужно будет в дальнейшем сохранять

        RectTransform a = this.GetComponent<RectTransform>();
        float SizeX = Data.X * 0.9f;
        float SizeY = SizeX / 5.7857f;
        a.sizeDelta = new Vector2(SizeX, SizeY); // задаем размер наших блоков зданий

        Name.text = ReseachName;
        About.text = ReseachAbout;
        BildImage.sprite = ReseachImage;

        if (FactBay == false)
        {
            txtCost.text = "Стоимость покупки ускорения " + Data.ConvertTxt(CostBay) + "$";
            //txtCost.gameObject.SetActive(false);
        }
        else
        {
            txtCost.text = "Ускорение приобретено$";
        }
    }

    // добавить защиту от повторного нажатия +
    public void ClickBay()
    {
        if (Data.CountMoney >= CostBay)
        {
            Data.CountMoney -= CostBay;
            FactBay = true;
            txtCost.text = "Ускорение приобретено";

            //само ускорение
            BildAll.ManyBuilding[Id].time /= 2;
            BildAll.ManyBuilding[Id].timelocal = (float)Math.Round(BildAll.ManyBuilding[Id].timelocal / 2);
        }
        // можно добавить что денег не достаточно
    }
}
