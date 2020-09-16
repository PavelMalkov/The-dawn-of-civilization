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
    public string ReseachName;
    public string ReseachAbout;
    public Sprite ReseachImage;
    public float CostBay;

    private int countUp = 0;


    private bool FactBay = false;


    void Start()
    {
        FactBay = false; // нужно будет в дальнейшем сохранять
        if (FactBay == false)
        {
            RectTransform a = this.GetComponent<RectTransform>();
            float SizeX = Data.X * 0.9f;
            float SizeY = SizeX / 5.7857f;
            a.sizeDelta = new Vector2(SizeX, SizeY); // задаем размер наших блоков зданий

            Name.text = ReseachName;
            About.text = ReseachAbout;
            BildImage.sprite = ReseachImage;

            txtCost.text = "Стоимость покупки " + Data.ConvertTxt(CostBay) + "$";
            //txtCost.gameObject.SetActive(false);
        }
    }

    // добавить защиту от повторного нажатия +
    public void ClickBay()
    {
        if (Data.count >= CostBay)
        {
            Data.count -= CostBay;
            FactBay = true;
            Bay.gameObject.SetActive(false);
        }
        // можно добавить что денег не достаточно
    }
}
