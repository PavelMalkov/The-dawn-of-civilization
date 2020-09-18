using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(BildAll))]
public class EditorBildAll : Editor
{
    private BildAll MyBild;

    public void OnEnable()
    {
        MyBild = (BildAll)target;
    }

    public override void OnInspectorGUI()
    {
        MyBild.CoefTimeOut = EditorGUILayout.FloatField("Коэфициент (во сколько раз мы уменьшаем прибыль при офлайне игрока)", MyBild.CoefTimeOut);
        //MyBild.Exempl = (GameObject)EditorGUILayout.ObjectField("Префаб в который добавляем данные:", MyBild.Exempl, typeof(GameObject), false);
        if (MyBild.ManyBuildingLocal.Count > 0)
        {

            foreach (Building item in MyBild.ManyBuildingLocal)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    BildAll.ManyBuilding.Remove(item);
                    break;
                }
                EditorGUILayout.EndHorizontal();


                /*
                // обычные данные
                public int Id; // номер здания
                public string homeName; //название здания
                public float CostBay; // Стоимость покупки
                public Sprite bildimage; // его изображение
                public float coefficientUp; // коэффициент повышения уровня
                public float coefficientMoney; // коэффициент повышения уровня
                */

                item.Id = EditorGUILayout.IntField("Какой ID здания", item.Id);
                item.homeName = EditorGUILayout.TextField("Название здание:", item.homeName);
                item.CostBay = EditorGUILayout.FloatField("Стоимость покупки:", item.CostBay);
                item.bildimage = (Sprite)EditorGUILayout.ObjectField("Изобравжение иконки дома:", item.bildimage, typeof(Sprite), false);
                item.coefficientUp = EditorGUILayout.FloatField("Коэффициент повышение уровня здания:", item.coefficientUp);
                item.coefficientMoney = EditorGUILayout.FloatField("Коэффициент повышение денег за уровень здания:", item.coefficientMoney);

                /*
                // сохраняемые данные
                public bool FactBay; // факт покупки
                public float money; // сколько денег здание приносит

                public int countUp; // какой уровень
                public float CostUp; // стоимость повышения уровня

                public float time; // сколько необходимо времени
                public float timelocal; // сколько времени прошло
                */
                
                item.FactBay = EditorGUILayout.Toggle("Факт покупки здания:", item.FactBay);
                item.countUp = EditorGUILayout.IntField("Какой уровень здания",item.countUp);

                item.CostUp = EditorGUILayout.FloatField("Стоимость повышение уровня", item.CostUp);
                item.Money = EditorGUILayout.FloatField("Сколько денег приносит за промежуток времени:", item.Money);

                item.CostUpLevel = item.CostUp * (float)Math.Pow(item.coefficientUp, item.countUp);
                item.MoneyLevel = item.Money * (float)Math.Pow(item.coefficientMoney, item.countUp);

                EditorGUILayout.LabelField("Стоимость повышение уровня = " + item.CostUpLevel);
                EditorGUILayout.LabelField("Количество получаемых денег = " + item.MoneyLevel);

                item.time = EditorGUILayout.FloatField("Сколько времени тратится:", item.time);
                item.timelocal = EditorGUILayout.FloatField("Сколько времени прошло:", item.timelocal);

                // Сам блок на панели
                //item.block = (GameObject)EditorGUILayout.ObjectField("Блок в магазине:", item.block, typeof(GameObject), false);

                EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("Нет элементов в списке");
        if (GUILayout.Button("Добавить описание", GUILayout.Height(30))) MyBild.ManyBuildingLocal.Add(new Building());
        //if (GUI.changed) SetObjectDirty(MyBild.gameObject);
    }

    /*public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }*/
}