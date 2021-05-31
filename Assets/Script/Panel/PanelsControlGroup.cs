using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsControlGroup : MonoBehaviour
{
    public List<PositionPanel> PanelsPosition; // это список наших Панелей
    public List<PanelCreate> PanelsCreate; // это список наших Панелей

    // открытие окна по ID
    public void OpenControlGroup(int Id)
    {
        if (Preferense.Play)
        {
            int i = 0;
            foreach (PositionPanel item in PanelsPosition)
            {
                if (i != Id) item.ClouseOpen();
                else
                {
                    item.ChancheStateOpen();
                }
                i++;
            }

            i = 0;
            foreach (PanelCreate item in PanelsCreate)
            {
                if (i == Id) item.ChancheActive();
                i++;
            }
        }
    }

    public void CloseAllControlGroup(int Id)
    {
        foreach (PositionPanel item in PanelsPosition)
        {
            item.ClouseOpen();
        }
    }
}
