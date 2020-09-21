using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveLoad : MonoBehaviour
{
    public static BildData data = new BildData();
    public BildData dataLocal;

    // The path local to the persistent data path we will use to save in binary
    //static private string localPath = "/save.poc";
    static private string localPath = "/save.txt";
    static private string localPathStart = "/data.txt";

    /*static SaveLoad()
    {
        data = new BildData();
        //{ "Id":0,"homeName":"Дом","CostBay":10.0,"bildimage":{ "instanceID":10784},"coefficientUp":1.100000023841858,"coefficientMoney":1.2000000476837159,"FactBay":true,"countUp":0,"CostUpLevel":10.0,"MoneyLevel":5.0,"CostUp":10.0,"Money":5.0,"time":10.0,"timelocal":7.0},
    }*/

    //public SaveLoad()
    //{
    //Bild house = new Bild(0, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f);
    //data.CoefTimeOut = 4;
    /*dataLocal.ManyBuildingLocal.Add(house);
    dataLocal.ManyBuildingLocal.Add(new Bild(1, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f));
    dataLocal.ManyBuildingLocal.Add(new Bild(2, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f));
    dataLocal.ManyBuildingLocal.Add(new Bild(3, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f));
    dataLocal.ManyBuildingLocal.Add(new Bild(4, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f));
    dataLocal.ManyBuildingLocal.Add(new Bild(5, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f));
    dataLocal.ManyBuildingLocal.Add(new Bild(6, false, 0, 20, 5, 10, 0, "Дом", 10, null, 1.2f, 1.1f));*/
    //}

    /*public static void ResaveBinary()
    {
        if (File.Exists(Application.persistentDataPath + localPath))
        {
            BildData serData = new BildData();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + localPathStart, FileMode.Open);
            serData = (BildData)formatter.Deserialize(file);
            file.Close();
            SaveLoad.data = serData;
        }
    }

    public static void SaveBinary()
    {
        BildData serData = new BildData();
        serData = SaveLoad.data;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + localPath);
        formatter.Serialize(file, serData);
        file.Close();
    }

    public static void LoadBinary()
    {
        if (File.Exists(Application.persistentDataPath + localPath))
        {
            BildData serData = new BildData();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + localPath, FileMode.Open);
            serData = (BildData)formatter.Deserialize(file);
            file.Close();
            SaveLoad.data = serData;
        }
    }*/

    public static void SaveData()
    {
#if UNITY_EDITOR
        filePath = Application.dataPath + "/StreamingAssets/data.json";
#elif UNITY_ANDROID
        filePath = "jar:file://" + Application.dataPath + "!/assets/data.json";
#endif
        //filePath = "jar:file://" + Application.dataPath + "!/assets/data.json";

        if (!(File.Exists(filePath))) using (FileStream fstream = new FileStream(filePath, FileMode.Create)) { }

        if (File.ReadAllText(filePath) != "{}" && File.ReadAllText(filePath) != "")
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

    public static BildData LoadData()
    {
        string path = Application.persistentDataPath + "/data.txt";
        FileStream stream = new FileStream(path, FileMode.Open);
        if (File.Exists(path) && stream.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            BildData data = formatter.Deserialize(stream) as BildData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file was not found in " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            //BildData data = new BildData();
            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }
    }

    public static Bild GetBildSetting(int ID)
    {
        return SaveLoad.data.ManyBuildingLocal[ID];
    }
}
