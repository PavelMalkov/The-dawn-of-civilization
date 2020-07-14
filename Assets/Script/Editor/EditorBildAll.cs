using System.Collections;
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
        MyBild.Exempl = (GameObject)EditorGUILayout.ObjectField("Префаб в который добавляем данные:", MyBild.Exempl, typeof(GameObject), false);
        if (MyBild.ManyBuilding.Count > 0)
        {
            foreach (Building item in MyBild.ManyBuilding)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    MyBild.ManyBuilding.Remove(item);
                    break;
                }
                EditorGUILayout.EndHorizontal();
                item.name = (Text)EditorGUILayout.ObjectField("Текстовое поле для названия здания:", item.name, typeof(Text), false);
                item.timer = (Text)EditorGUILayout.ObjectField("Текстовое поля для времени:", item.timer, typeof(Text), false);
                item.bildimage = (Image)EditorGUILayout.ObjectField("Изобравжение иконки дома:", item.bildimage, typeof(Image), false);
                item.bild = (Image)EditorGUILayout.ObjectField("Дом на игровом поле:", item.bild, typeof(Image), false);
                item.progress = (Scrollbar)EditorGUILayout.ObjectField("Полоса прогреса", item.name, typeof(Scrollbar), false);
                item.bay = (Button)EditorGUILayout.ObjectField("Кнопка покупки здания:", item.name, typeof(Button), false);
                item.levelup = (Button)EditorGUILayout.ObjectField("Кнопка повышения прогресса", item.name, typeof(Button), false);

                item.money = EditorGUILayout.FloatField("Сколько денег приносит за промежуток времени:", item.money);
                item.time = EditorGUILayout.FloatField("Сколько времени тратится:", item.time);

                EditorGUILayout.EndVertical();
            }
        }
        else EditorGUILayout.LabelField("Нет элементов в списке");
        if (GUILayout.Button("Добавить", GUILayout.Height(30))) MyBild.ManyBuilding.Add(new Building());
        if (GUI.changed) SetObjectDirty(MyBild.gameObject);
    }

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}