using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.IO;
using UnityEngine.UI;

public class SaveControl : MonoBehaviour
{
    public float CoefTimeOut;

    public List<Bild> ManyBuildingLocal = new List<Bild>(); // это список наших домов которые добавляются из unity

    public static List<Bild> ManyBuilding = new List<Bild>(); // это список наших домов (для связи с другими скриптами)
    public static List<Boost> boosts = new List<Boost>(); // это список наших домов (для связи с другими скриптами)

    public bool ResetSave; //флаг для пересохранения

    bool OpenGame = true;

    string filePath = ""; // путь
    string dataAsJson; // хранящиеся данные
    Save loadedData; // обращение к ним

    static SaveControl()
    {
        ManyBuilding = new List<Bild>();
    }

    private void Awake()
    {
        // проверка времени по интернету очень медленная так что надо что то делать
        //var dateTime = CheckGlobalTime();
        //Debug.Log("Global UTC time: " + dateTime);
        //делаем проверку совподают ли локальное и интернет время Пока не делаю незнаю как пользователю объеснить
        //The dawn of civilization 
        //LoadGameData();
        //print(Application.persistentDataPath);
        //OpenGame = true;
    }

    private void OnApplicationQuit() // сохраняет но только когда выключено
    {
        if (OpenGame)
        {
            LoadGameData();
            OpenGame = false;
        }
        else
        {
            SaveGameData();
            OpenGame = true;
        }
        print("Выход из приложения");
    }

    private void OnApplicationPause() // сохраняет но только когда выключено
    {
        if (OpenGame)
        {
            LoadGameData();
            if (ResetSave) SetStartRestart();
            OpenGame = false;
        }
        else
        {
            SaveGameData();
            OpenGame = true;
        }
        print("Выход из приложения");
    }

    private void Destroy() // сохраняет но только когда выключено
    {
        SaveGameData();
        print("Выход из приложения");
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


    //Сколько мы денег зарабатываем
    private float MoneyPerSecond()
    {
        float m1 = 0;
        foreach (Bild item in SaveControl.ManyBuilding)
        {
            if (item.FactBay) m1 += item.Money / item.time;
        }
        return m1;
    }

    //получение здания
    public static Bild GetBildSetting(int ID) { return SaveControl.ManyBuilding[ID]; }

    private void SetStart() // Добавление начальных параметров игры 
    {
        int i = 0;
        foreach (Bild item in ManyBuildingLocal)
        {
            print("есть " + item.Id);
            SaveControl.ManyBuilding.Add(item); // важно чтобы это было впервый раз
            i++;
        }
        Data.CountMoney = 0;
        Data.BildCount = 0;
    }

    private void SetStartRestart() // сброс прогресса
    {
        int i = 0;
        foreach (Bild item in ManyBuildingLocal)
        {
            print("есть " + item.Id);
            SaveControl.ManyBuilding[i] = item;
            i++;
        }
        Data.CountMoney = 0;
        Data.BildCount = 0;
    }

    // тестовые Загрузка
    private void LoadGameData()
    {
        filePath = Application.persistentDataPath + "/data.json";


        if (!(File.Exists(filePath))) using (FileStream fstream = new FileStream(filePath, FileMode.Create)) { }

        if (File.ReadAllText(filePath) != "{}" && File.ReadAllText(filePath) != "")
        {
            dataAsJson = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<Save>(dataAsJson);

            SaveControl.ManyBuilding = loadedData.ManyBuildingLocal;
            Data.CountMoney = loadedData.CountMoney;

            // подумай насчет вынести это куда-то
            foreach (var item in SaveControl.ManyBuilding)
            {
                if (item.FactBay) Data.BildCount++;
            }

            TimeSpan tm;
            float timeSecond, timeMin, moneySecond; // минуты это + часы + дни + года
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
        filePath = Application.persistentDataPath + "/data.json";

        print("Сохранение");
        // сохранение прогресса

        dataAsJson = File.ReadAllText(filePath); // чтение файла
        loadedData = JsonUtility.FromJson<Save>(dataAsJson); // загрузка параметров

        loadedData.ManyBuildingLocal = SaveControl.ManyBuilding;
        loadedData.boosts = SaveControl.boosts;
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

    /* НЕ УДАЛЯЙ
     * добавить удаление прогресса
    for (int i = 0; i < loadListData.Count; i++)
    {
        if (loadListData[i].name == "ken")
        {
            loadListData.Remove(loadListData[i]);
        }
    }   
    */
}

