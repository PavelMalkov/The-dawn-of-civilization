using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeControl : MonoBehaviour
{
    #region Singleton
    public static TimeControl Instance { get; private set; }
    private void InitSingleton()
    {
        Instance = this;
    }
    #endregion
    private void Awake()
    {
        InitSingleton();
        SetLastSession();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) PlayerPrefs.SetString("LastSession", DateTime.UtcNow.ToString());
    }

    private void OnApplicationQuit() // сохраняет но только когда выключено 
    {
        PlayerPrefs.SetString("LastSession", DateTime.UtcNow.ToString());
    }

    private void SetLastSession()
    {
        /*TimeSpan ts;
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
        }*/
        //PlayerPrefs.SetString("LastSession", DateTime.UtcNow.ToString());
    }
}
