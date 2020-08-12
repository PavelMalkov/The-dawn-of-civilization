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
                // надо изображение самого дома
                item.bildimage = (Sprite)EditorGUILayout.ObjectField("Изобравжение иконки дома:", item.bildimage, typeof(Sprite), false);
                // это положение самого дома на игровом поле
                item.bild = (Image)EditorGUILayout.ObjectField("Дом на игровом поле:", item.bild, typeof(Image), false);
                item.homeName = EditorGUILayout.TextField("Название здание:", item.homeName);
                item.money = EditorGUILayout.FloatField("Сколько денег приносит за промежуток времени:", item.money);
                item.time = EditorGUILayout.FloatField("Сколько времени тратится:", item.time);
                item.cost = EditorGUILayout.FloatField("Сколько стоит здание:", item.time);

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