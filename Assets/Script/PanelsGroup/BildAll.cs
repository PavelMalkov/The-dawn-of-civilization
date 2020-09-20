﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.IO;
using System.Text;

public class BildAll : MonoBehaviour
{
    public float CoefTimeOut;
    public List<Building> ManyBuildingLocal; // это список наших домов
    public static List<Building> ManyBuilding; // это список наших домов (для связи с другими скриптами)

    public bool ResetSave;

    string filePath = ""; // путь
    string dataAsJson; // хранящиеся данные
    BildSave loadedData; // обращение к ним

    static BildAll()
    {
        ManyBuilding = new List<Building>();
    }

    private void Awake()
    {
        // проверка времени по интернету очень медленная так что надо что то делать
        //var dateTime = CheckGlobalTime();
        //Debug.Log("Global UTC time: " + dateTime);
        //делаем проверку совподают ли локальное и интернет время Пока не делаю незнаю как пользователю объеснить

        print(float.MaxValue);
        print(Data.ConvertTxt(0));
        LoadGameData();
    }

    DateTime CheckGlobalTime()
    {
        var www = new WWW("https://google.com");
        while (!www.isDone && www.error == null) Thread.Sleep(2);

        var str = www.responseHeaders["Date"];
        DateTime dateTime;

        if (!DateTime.TryParse(str, out dateTime))
            return DateTime.MinValue;

        return dateTime.ToUniversalTime();
    }

    public static Building GetBildSetting(int ID)
    {
        return BildAll.ManyBuilding[ID];
    }

    private void SetStart() // Добавление начальных параметров игры 
    {
        int i = 0;
        foreach (Building item in ManyBuildingLocal)
        {
            print("есть " + item.Id);
            BildAll.ManyBuilding.Add(item); // важно чтобы это было впервый раз
            i++;
        }
        Data.CountMoney = 0;
    }

    private void SetStartRestart() // сброс прогресса
    {
        int i = 0;
        foreach (Building item in ManyBuildingLocal)
        {
            print("есть " + item.Id);
            BildAll.ManyBuilding[i] = item;
            i++;
        }
        Data.CountMoney = 0;
    }

    /*private void OnApplicationPause(bool pause)
    {
        print("Пауза приложения");
        if (pause) SaveGameData();
        else LoadGameData();
    }*/

    private void OnApplicationQuit() // сохраняет но только когда выключено
    {
        SaveGameData();
        print("Выход из приложения");
    }

    //Сколько мы денег зарабатываем
    private float MoneyPerSecond()
    {
        float m1 = 0;
        foreach (Building item in BildAll.ManyBuilding)
        {
            if (item.FactBay) m1 += item.Money / item.time;
        }
        return m1;
    }



    // тестовые Загрузка
    private void LoadGameData()
    {
        #if UNITY_EDITOR
        filePath = Application.dataPath + "/StreamingAssets/data.json";
#elif UNITY_ANDROID
        filePath = "jar:file://" + Application.dataPath + "!/assets/data.json";
#endif
        //filePath = "jar:file://" + Application.dataPath + "!/assets/data.json";

        if (!(File.Exists(filePath))) using (FileStream fstream = new FileStream(filePath, FileMode.Create)) { }

        if  (File.ReadAllText(filePath) != "{}" && File.ReadAllText(filePath) != "")
        {
            dataAsJson = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<BildSave>(dataAsJson);

            print("Проверочное");

            float timeSecond, timeMin, moneySecond; // минуты это + часы + дни + года

            BildAll.ManyBuilding = loadedData.ManyBuildingLocal; 
            Data.CountMoney = loadedData.CountMoney;

            TimeSpan tm;
            if (loadedData.DateLast != null)
            {
                tm = DateTime.Now - DateTime.Parse(loadedData.DateLast);
                timeMin = tm.Minutes + tm.Hours * 60 + tm.Days * 60 * 24;
                timeSecond = tm.Seconds;
                moneySecond = MoneyPerSecond();
                print("Зарабатываем " + moneySecond + " в секунду");
                print("Заработали во время отсутствия: " + (moneySecond * timeSecond + moneySecond * timeMin * 60) / CoefTimeOut);
            }
        }
        else
        {
            if (File.ReadAllText(filePath) == "") File.WriteAllText(filePath, "{}");
            //Debug.LogError("Can not load game data!");
            Debug.Log("Сброс сохранений");
            SetStart();
        }
    }

    // тестовые Сохранение
    private void SaveGameData()
    {
    #if UNITY_EDITOR
        filePath = Application.dataPath + "/StreamingAssets/data.json";
#elif UNITY_ANDROID
        filePath = "jar:file://" + Application.dataPath + "!/assets/data.json";
#endif

        print("Сохранение");
        // сохранение прогресса

        dataAsJson = File.ReadAllText(filePath);
        loadedData = JsonUtility.FromJson<BildSave>(dataAsJson);

        loadedData.ManyBuildingLocal = BildAll.ManyBuilding;
        loadedData.CountMoney = Data.CountMoney;
        loadedData.DateLast = DateTime.Now.ToString();

        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(loadedData));
        }
        else
        {
            Debug.LogError("Can not save game data!");
        }
    }
    // добавить удаление прогресса
    // НЕ УДАЛЯЙ
    /*
    for (int i = 0; i < loadListData.Count; i++)
    {
        if (loadListData[i].name == "ken")
        {
            loadListData.Remove(loadListData[i]);
        }
    }   
    */
}

[Serializable]
    public class BildSave
    {
        public float CountMoney;
        //public float MoneyPerSecond; // Это сколько денег в секунду мы зарабатываем (скорость при выключенной игре сделаем в 50 раз медленнее)
        public string DateLast;
        public List<Building> ManyBuildingLocal; // это список наших домов
        public BildSave() { CountMoney = 0; }
    }



/*
Сохраняемые данные:
FactBay факт покупки
countUp уровень
CostUp стоимость повышения
money количество денез за время
time это общее время время
timelocal это сколько времени прошло
*/

[Serializable]
public class Building
{
    // обычные данные
    public int Id; // номер здания
    public string homeName; //название здания
    public float CostBay; // Стоимость покупки
    public Sprite bildimage; // его изображение
    public float coefficientUp; // коэффициент повышения уровня
    public float coefficientMoney; // коэффициент повышения уровня

    // сохраняемые данные
    public bool FactBay; // факт покупки

    public int countUp; // какой уровень

    public float CostUpLevel;
    public float MoneyLevel;

    public float CostUp; // стоимость повышения уровня 
    public float Money; // сколько денег здание приносит

    public float time; // сколько необходимо времени
    public float timelocal; // сколько времени прошло


    // Надо нормально сделать get set

    public void getbildList(bool _FactBay, int _countUp, float _CostUp, float _money, float _time, float _timelocal, string _homeName, float _CostBay, Sprite _bildimage,
        float _coefficientUp, float _coefficientMoney)
    { _FactBay = FactBay; _countUp = countUp; _CostUp = CostUp; _money = Money; _time = time; _timelocal = timelocal; _homeName = homeName; _CostBay = CostBay; _bildimage = bildimage;
        _coefficientUp = coefficientUp; _coefficientMoney = coefficientMoney;
    }

    public void getbildList(int _Id, bool _FactBay, int _countUp, float _CostUp, float _money, float _time, float _timelocal, string _homeName, float _CostBay, Sprite _bildimage,
    float _coefficientUp, float _coefficientMoney)
    {
        _Id = Id;
        _FactBay = FactBay; _countUp = countUp; _CostUp = CostUp; _money = Money; _time = time; _timelocal = timelocal; _homeName = homeName; _CostBay = CostBay;
        _bildimage = bildimage; _coefficientUp = coefficientUp; _coefficientMoney = coefficientMoney;
    }

    public void setbildList(Building bildList)
    {
        bildList.Id = Id;
        bildList.FactBay = FactBay; bildList.countUp = countUp; bildList.CostUp = CostUp; bildList.Money = Money; bildList.time = time; bildList.timelocal = timelocal;
        bildList.homeName = homeName; bildList.CostBay = CostBay; bildList.bildimage = bildimage; bildList.coefficientUp = coefficientUp; bildList.coefficientMoney = coefficientMoney;
    }
}