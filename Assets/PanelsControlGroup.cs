using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsControlGroup : MonoBehaviour
{
    public List<PositionPanel> Panels; // это список наших Панелей

    public void OpenControlGroup(int Id)
    {
        int i = 0;
        foreach (PositionPanel item in Panels)
        {
            if (i != Id) item.ClouseOpen();
            else
            {
                item.ChancheStateOpen();
            }
            i++;
        }
    }
}
