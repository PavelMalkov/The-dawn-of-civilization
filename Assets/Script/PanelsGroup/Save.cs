using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public float GoldSave;
    public float ScienceSave;
    public List<Bild> bilds; // это список наших домов
    public List<Boost> boosts; // это ускорение
    public List<Resheach> resheaches; // это ускорение
    public Save() { GoldSave = 0; ScienceSave = 0;}
}