using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SaveControl))]
public class EditorBildAll : Editor
{
    //Массив строк, которые хотим видеть в выпадающем списке
    public string[] options = new string[] { "OpenBild", "Up", "OpenOfAbilities" };
    public int index = 0;

    private SaveControl MyBild;

    public void OnEnable()
    {
        MyBild = (SaveControl)target;
    }

    public override void OnInspectorGUI()
    {

        MyBild.CoefTimeOut = EditorGUILayout.FloatField("Коэфициент (во сколько раз мы уменьшаем прибыль при офлайне игрока)", MyBild.CoefTimeOut);
        MyBild.ResetSave = EditorGUILayout.Toggle("Перезагрузить данные:", MyBild.ResetSave);
        //MyBild.Exempl = (GameObject)EditorGUILayout.ObjectField("Префаб в который добавляем данные:", MyBild.Exempl, typeof(GameObject), false);
        if (MyBild.BildLocal.Count >= 0)
        {
            foreach (Bild item in MyBild.BildLocal)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    MyBild.BildLocal.Remove(item);
                    break;
                }
                EditorGUILayout.EndHorizontal();
                item.Open = EditorGUILayout.Toggle("Свернуть окно ", item.Open);
                if (!item.Open)
                {
                    item.Id = EditorGUILayout.IntField("Какой ID здания", item.Id);
                    item.IdBoost = EditorGUILayout.IntField("Какой IdBoost ускорения", item.IdBoost);
                    item.homeName = EditorGUILayout.TextField("Название здание:", item.homeName);
                    item.CostBay = EditorGUILayout.FloatField("Стоимость покупки:", item.CostBay);
                    item.bildimage = (Sprite)EditorGUILayout.ObjectField("Изобравжение иконки дома:", item.bildimage, typeof(Sprite), false);
                    item.coefficientUp = EditorGUILayout.FloatField("Коэффициент повышение уровня здания:", item.coefficientUp);
                    item.coefficientMoney = EditorGUILayout.FloatField("Коэффициент повышение денег за уровень здания:", item.coefficientMoney);

                    item.FactBay = EditorGUILayout.Toggle("Факт покупки здания:", item.FactBay);
                    item.YouCanBay = EditorGUILayout.Toggle("Возможность покупки:", item.YouCanBay);
                    item.countUp = EditorGUILayout.IntField("Какой уровень здания", item.countUp);

                    item.CostUp = EditorGUILayout.FloatField("Стоимость повышение уровня", item.CostUp);
                    item.Money = EditorGUILayout.FloatField("Сколько денег приносит за промежуток времени:", item.Money);
                    EditorGUILayout.LabelField("Номер валюты (0 - золото, 1 - наука)");
                    item.NumCurrent = EditorGUILayout.IntField(item.NumCurrent);

                    item.CostUpLevel = item.CostUp * (float)Math.Pow(item.coefficientUp, item.countUp);
                    item.MoneyLevel = item.Money * (float)Math.Pow(item.coefficientMoney, item.countUp);

                    EditorGUILayout.LabelField("Стоимость повышение уровня = " + item.CostUpLevel);
                    EditorGUILayout.LabelField("Количество получаемых денег = " + item.MoneyLevel);

                    item.time = EditorGUILayout.FloatField("Сколько времени тратится:", item.time);
                    item.timelocal = EditorGUILayout.FloatField("Сколько времени прошло:", item.timelocal);
                }
                EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("Нет элементов в списке");
        if (GUILayout.Button("Добавить описание зданий", GUILayout.Height(30)))
        {
            MyBild.BildLocal.Add(new Bild());
        }

        if (MyBild.BoostLocal.Count >= 0)
        {
            foreach (Boost item in MyBild.BoostLocal)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    MyBild.BoostLocal.Remove(item);
                    break;
                }
                EditorGUILayout.EndHorizontal();

                item.Open = EditorGUILayout.Toggle("Свернуть окно ", item.Open);
                if (!item.Open)
                {
                    item.Id = EditorGUILayout.IntField("Какой ID здания", item.Id);
                    item.IdBoost = EditorGUILayout.IntField("Какой ID ускорения", item.IdBoost);
                    item.NameBoost = EditorGUILayout.TextField("Название ускорения:", item.NameBoost);
                    item.AboutBoost = EditorGUILayout.TextField("Текст ускорения:", item.AboutBoost);
                    item.CanYouBay = EditorGUILayout.Toggle("Возможность приобритения ", item.CanYouBay);

                    item.BoostImage = (Sprite)EditorGUILayout.ObjectField("Изобравжение иконки ускорения:", item.BoostImage, typeof(Sprite), false);

                    EditorGUILayout.LabelField("Стоимость ускорений:");
                    item.CostBay = EditorGUILayout.FloatField("Стоимость повышения уровня", item.CostBay);

                    EditorGUILayout.LabelField("Бонус ускорения:");

                    item.CoefBoost = EditorGUILayout.FloatField("Бонус повышения уровня", item.CoefBoost);

                    EditorGUILayout.LabelField("Факт покупки ускорения:");
                    item.FactBayBonus = EditorGUILayout.Toggle("Факт повышения уровня", item.FactBayBonus);
                }
                EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("Нет элементов в списке");
        if (GUILayout.Button("Добавить описание ускорений", GUILayout.Height(30)))
        {
            MyBild.BoostLocal.Add(new Boost());
        }

        if (MyBild.ReseachLocal.Count >= 0)
        {
            if (MyBild.ReseachLocal.Count >= 0)
            {

                foreach (Resheach item in MyBild.ReseachLocal)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        MyBild.ReseachLocal.Remove(item);
                        break;
                    }
                    EditorGUILayout.EndHorizontal();

                    item.Open = EditorGUILayout.Toggle("Свернуть окно ", item.Open);
                    if (!item.Open)
                    {
                        item.Id = EditorGUILayout.IntField("Какой ID здания", item.Id);
                        item.IdReseach = EditorGUILayout.IntField("Какой ID исследования", item.IdReseach);
                        item.IdBoosts = EditorGUILayout.IntField("Какой ID ускорения", item.IdBoosts);
                        item.NameResheach = EditorGUILayout.TextField("Название исследования:", item.NameResheach);
                        //item.AboutResheach = EditorGUILayout.TextField("Текст ускорения:", item.AboutResheach);


                        index = EditorGUILayout.Popup(index, options);
                        item.State = index;

                        item.ResheachImage = (Sprite)EditorGUILayout.ObjectField("Изобравжение иконки ускорения:", item.ResheachImage, typeof(Sprite), false);

                        item.Open1 = (Sprite)EditorGUILayout.ObjectField("Изображение улучшаемого дома:", item.Open1, typeof(Sprite), false);
                        item.Open2 = (Sprite)EditorGUILayout.ObjectField("Изобравжение открывающихся возможностей 1:", item.Open2, typeof(Sprite), false);
                        item.Open3 = (Sprite)EditorGUILayout.ObjectField("Изобравжение открывающихся возможностей 2:", item.Open3, typeof(Sprite), false);

                        item.CostBay = EditorGUILayout.FloatField("Стоимость ускорения:", item.CostBay);

                        item.bonus = EditorGUILayout.FloatField("Начальный бонус ускорения", item.bonus);
                        item.bonusdiv = EditorGUILayout.FloatField("Шаг ускорения", item.bonusdiv);

                        item.time = EditorGUILayout.FloatField("Сколько времени тратится:", item.time);
                        item.timelocal = EditorGUILayout.FloatField("Сколько времени прошло:", item.timelocal);
                    }
                    EditorGUILayout.EndVertical();
                }
                if (GUI.changed) SetObjectDirty(MyBild.gameObject);
            }
        }
        else EditorGUILayout.LabelField("Нет элементов в списке");
        if (GUILayout.Button("Добавить описание исследований", GUILayout.Height(30)))
        {
            MyBild.ReseachLocal.Add(new Resheach());
        }
    }
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}