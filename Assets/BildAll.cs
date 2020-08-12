using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class BildAll : MonoBehaviour
{
    public GameObject Exempl; // это префаб куда мы будем записывать наши значения
    public List<Building> ManyBuilding; // это список наших домов

    private void Start()
    {
        foreach (Building item in ManyBuilding)
        {
            print("есть");
            Instantiate(Exempl);
        }
    }

    // проверка нажатой кнопки
    /*public void OnClick(Building ManyBuilding)
    {
        ManyBuilding.ClickBay(ManyBuilding.name.text.ToString());
    }*/
}
[System.Serializable]
public class Building
{
    public Text name;
    public Text timer;
    public Sprite bildimage;
    public Image bild;
    public Scrollbar progress;
    public Button bay;
    public Button levelup;

    public float money;
    public float time;
    public float cost;
    public string homeName;

    private bool factbay = false;

    public Building() {}

    // добавить защиту от повторного нажатия
    public void ClickBay(string str)
    {
        switch (str)
        {
            case "Bay":
                {
                    // здесь добавить появление дома на игровом поле
                    this.factbay = true;
                    bay.gameObject.SetActive(false);
                    levelup.gameObject.SetActive(true);
                    timer.gameObject.SetActive(true);
                    progress.gameObject.SetActive(true);
                    //необходимо запускать карутину из интерфейса
                    //StartCoroutine(Time());
                    break;
                }

            default:
                break;
        }
    }

    IEnumerator Time()
    {
        float timelocal = time;
        int sec, min, hour;
        int secAll, minAll, hourAll;
        //hour = (int)TimeSpan.FromSeconds(time).TotalHours; // 0,0166666666666667

        while (true)
        {
            // установить таймер
            minAll = (int)TimeSpan.FromSeconds(timelocal).TotalMinutes; // 1
            secAll = (int)TimeSpan.FromSeconds(timelocal - (minAll * 60)).TotalSeconds; // 60

            min = (int)TimeSpan.FromSeconds(timelocal).TotalMinutes; // 1
            sec = (int)TimeSpan.FromSeconds(timelocal - (min * 60)).TotalSeconds; // 60
            timelocal -= 1;

            string res = String.Format("{0:d2}:{1:d2}/({2:d2}:{3:d2})", min, sec, minAll, secAll);

            timer.text = res;

            // установить прогресс
            progress.size = (time - timelocal - 1) / time; // тут всегда 0
            yield return new WaitForSeconds(1);
            if (timelocal == -1)
            {
                timelocal = time;
            }; // сделать сложение money с монетами;
        }
    }
}
