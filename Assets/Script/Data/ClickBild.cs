using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBild : MonoBehaviour
{
    // Будем менять под разные валюты
    [SerializeField]
    float OneClick;

    [SerializeField]
    public int Tipe; // 0 - золото, 1 - наука

    [SerializeField]
    GameObject Mess;

    //Границы для расчетов крайних точек колайдера 
    Bounds boxBounds;
    //Координата вехней точки колайдера
    Vector2 top;

    float sum = 0;

    GameObject instance;

    private void Start()
    {
        boxBounds = GetComponentInParent<BoxCollider2D>().bounds;
        top = new Vector2(0, boxBounds.extents.y);
    }

    private void OnMouseDown()
    {                
        if (!instance)
        {
            GenerateMessage();
        } else if (instance.transform.localPosition.y != top.y) GenerateMessage();
        else
        {
            sum += OneClick;
            instance.GetComponent<MessageAnimation>().sum = sum;
            instance.GetComponent<MessageAnimation>().Click();
        }
    }

    private void GenerateMessage()
    {
        sum = OneClick; // сумма
        instance = GameObject.Instantiate(Mess) as GameObject;

        // обращаемя к скрипту
        MessageAnimation message = instance.GetComponent<MessageAnimation>();
        message.NumCurrency = Tipe; // номер иконки
        message.sum = OneClick; // текс
        message.tipe = Tipe;

        instance.GetComponent<Transform>().position = top; // устанавливаем позицию
        instance.transform.SetParent(gameObject.transform, false); // устанавлиеваем его дочерним
        instance.SetActive(true); // отрисовка объектов которые подходят
    }
}
