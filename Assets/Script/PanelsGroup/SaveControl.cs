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

    public List<Bild> BildLocal = new List<Bild>(); // это список наших домов которые добавляются из unity
    public List<Boost> BoostLocal = new List<Boost>(); // это список наших ускорений которые добавляются из unity
    public List<Resheach> ReseachLocal = new List<Resheach>(); // это список наших исследований которые добавляются из unity

    public static List<Bild> Bilds = new List<Bild>(); // это список наших домов (для связи с другими скриптами)
    public static List<Boost> Boosts = new List<Boost>(); // это список наших ускорений (для связи с другими скриптами)
    public static List<Resheach> Reseachs = new List<Resheach>(); // это список наших исследований (для связи с другими скриптами)

    public bool ResetSave; //флаг для пересохранения

    bool OpenGame = true;

    string filePath = ""; // путь
    string dataAsJson; // хранящиеся данные

    Save loadedData; // обращение к ним

    private bool load = false;


    static SaveControl()
    {
        Bilds = new List<Bild>();
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
        Preferense.Div = CoefTimeOut;
        if (!load) 
        {
            if (ResetSave) SetStart();
            else LoadGameData();
            load = true;
        }
        SaveGameData();

    }

    private void OnApplicationQuit() // сохраняет но только когда выключено 
    {
        SaveGameData();
    }

    /*private void OnApplicationPause(bool pause) // сохраняет но только когда выключено // 1
    {   
        if (!load)
        {
            if (ResetSave) SetStart();
            else LoadGameData();
            load = true;
        } 
        SaveGameData();
    }*/

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

    //получение здания
    public static Bild GetBildSetting(int ID) 
    {
        foreach (var item in SaveControl.Bilds)
        {
            if (item.Id == ID) return item;
        }
        return null;
    }

    //получение ускорения
    public static Boost GetBoostSetting(int IDboost)
    { 
        foreach (var item in SaveControl.Boosts)
        {
            if (item.IdBoost == IDboost) return item;
        }
        return null;
    }

    //получение исследования
    public static Resheach GetReseachSetting(int IDreseach)
    {
        foreach (var item in SaveControl.Reseachs)
        {
            if (item.IdReseach == IDreseach) return item;
        }
        return null;
    }


    // Установка факта возможности покупки ускорения
    public static void SetBoostSetting(int IDboost, bool set) 
    {
        foreach (var item in SaveControl.Boosts)
        {
            if (item.IdBoost == IDboost) item.CanYouBay = set;
        }
    }

    // Установка факта возможности покупки здания
    public static void SetBildSetting(int IDbild, bool set)
    {
        foreach (var item in SaveControl.Bilds)
        {
            if (item.Id == IDbild) item.YouCanBay = set;
        }
    }

    private void SetStart() // Добавление начальных параметров игры 
    {
        foreach (Bild item in BildLocal)
        {
            //print("есть " + item.Id);
            SaveControl.Bilds.Add(item); // важно чтобы это было впервый раз
        }
        foreach (Boost item in BoostLocal) // добавление данных об ускорении
        {
            //print("есть " + item.Id);
            SaveControl.Boosts.Add(item);
        }
        foreach (Resheach item in ReseachLocal) // добавление данных об ускорении
        {
            //print("есть " + item.Id);
            SaveControl.Reseachs.Add(item);
        }
        PlayerPrefs.SetString("LastSession",null); // сбрасываем значение последнего запуска
    }

    private void SetStartRestart() // сброс прогресса
    {
        int i = 0;
        foreach (Bild item in BildLocal)
        {
            print("есть " + item.Id);
            SaveControl.Bilds[i] = item;
            i++;
        }
        i = 0;
        foreach (Boost item in BoostLocal)
        {
            print("есть " + item.Id);
            SaveControl.Boosts[i] = item;
            i++;
        }
    }

    // тестовые Загрузка
    private void LoadGameData()
    {
        filePath = Application.persistentDataPath + "/data.json";


        if (!(File.Exists(filePath))) using (FileStream fstream = new FileStream(filePath, FileMode.Create)) { }

        if (File.ReadAllText(filePath) != "{}" && File.ReadAllText(filePath) != "")
        {
            // Весь текст файла
            dataAsJson = File.ReadAllText(filePath);
            // Сохранение текста в класс
            loadedData = JsonUtility.FromJson<Save>(dataAsJson);

            // использование статического объекта для хранения полученных данных
            SaveControl.Bilds = loadedData.bilds;
            SaveControl.Boosts = loadedData.boosts;
            SaveControl.Reseachs = loadedData.resheaches;

            // загрузка всех счетчиков валют
            Currency.Gold = loadedData.GoldSave;
            Currency.Science = loadedData.ScienceSave;

            // подумай насчет вынести это куда-то
            int i = 0;
            foreach (var item in SaveControl.Bilds)
            {
                if (item.FactBay) Currency.BildCount++;
                item.bildimage = BildLocal[i].bildimage;
                i++;
            }
            //
            i = 0;
            foreach (var item in SaveControl.Boosts)
            {
                item.BoostImage = BoostLocal[i].BoostImage;
                i++;
            }
        }
        else
        {
            if (File.ReadAllText(filePath) == "") File.WriteAllText(filePath, "{}");
            Debug.Log("Сброс сохранений");
            SetStart();
        }
        if (SaveControl.Bilds.Count == 0)
        {
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

        loadedData.bilds = SaveControl.Bilds;
        loadedData.boosts = SaveControl.Boosts;
        loadedData.resheaches = SaveControl.Reseachs;
        // Сохранение всех счетчиков валют
        loadedData.GoldSave = Currency.Gold;
        loadedData.ScienceSave = Currency.Science;

        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(loadedData));
        }
        else
        {
            Debug.LogError("Can not save game data!");
        }
    }
}

