using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

//[CustomEditor(typeof(Bild))]
public class EditorBild : Editor
{
    private Bild MyBild;

    public void OnEnable()
    {
        MyBild = (Bild)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical("Box");
        MyBild.Name = (Text)EditorGUILayout.ObjectField("Текстовое поле для названия здания:", MyBild.Name, typeof(Text),false);
        MyBild.Timer = (Text)EditorGUILayout.ObjectField("Текстовое поля для времени:", MyBild.Timer, typeof(Text),false);
        MyBild.BildImage = (Image)EditorGUILayout.ObjectField("Изобравжение иконки дома:", MyBild.BildImage, typeof(Image),false);
        MyBild.BildMain = (Image)EditorGUILayout.ObjectField("Дом на игровом поле:", MyBild.BildMain, typeof(Image),false);
        MyBild.Progress = (Scrollbar)EditorGUILayout.ObjectField("Полоса прогреса", MyBild.Progress, typeof(Scrollbar),false);
        MyBild.Bay = (Button)EditorGUILayout.ObjectField("Кнопка покупки здания:", MyBild.Bay, typeof(Button),false);
        MyBild.LevelUp = (Button)EditorGUILayout.ObjectField("Кнопка повышения прогресса", MyBild.LevelUp, typeof(Button),false);

        MyBild.money = EditorGUILayout.FloatField("Сколько денег приносит за промежуток времени:", MyBild.money);
        MyBild.time = EditorGUILayout.FloatField("Сколько времени тратится:", MyBild.time);

        EditorGUILayout.EndVertical();

        if (GUI.changed) SetObjectDirty(MyBild.gameObject);
    }

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}

//BildAll
/*


*/
