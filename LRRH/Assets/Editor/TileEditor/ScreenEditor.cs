using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(Screen))]
public class ScreenEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Move Zone(s) up"))
        {
            moveZonesUp();
        }
        if (GUILayout.Button("Move Zone(s) down"))
        {
            moveZonesDown();
        }
        if (GUILayout.Button("Move Zone(s) right"))
        {
            moveZonesRight();
        }
        if (GUILayout.Button("Move Zone(s) left"))
        {
            moveZonesLeft();
        }
        EditorGUILayout.EndVertical();
    }

    public void moveZonesUp()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++) {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.y += 330;
            selectionList[i].transform.position = newPos;
            var screen =  selectionList[i].GetComponent<Screen>();
            var newMinBound = screen.minBound;
            newMinBound.y += 11;
            screen.minBound = newMinBound;
            var newMaxBound = screen.maxBound;
            newMaxBound.y += 11;
            screen.maxBound = newMaxBound;
        }
    }

    public void moveZonesDown()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++)
        {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.y -= 330;
            selectionList[i].transform.position = newPos;
            var screen = selectionList[i].GetComponent<Screen>();
            var newMinBound = screen.minBound;
            newMinBound.y -= 11;
            screen.minBound = newMinBound;
            var newMaxBound = screen.maxBound;
            newMaxBound.y -= 11;
            screen.maxBound = newMaxBound;
        }
    }

    public void moveZonesRight()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++)
        {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.x += 540;
            selectionList[i].transform.position = newPos;
            var screen = selectionList[i].GetComponent<Screen>();
            var newMinBound = screen.minBound;
            newMinBound.x += 18;
            screen.minBound = newMinBound;
            var newMaxBound = screen.maxBound;
            newMaxBound.x += 18;
            screen.maxBound = newMaxBound;
        }
    }

    public void moveZonesLeft()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++)
        {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.x -= 540;
            selectionList[i].transform.position = newPos;
            var screen = selectionList[i].GetComponent<Screen>();
            var newMinBound = screen.minBound;
            newMinBound.x -= 18;
            screen.minBound = newMinBound;
            var newMaxBound = screen.maxBound;
            newMaxBound.x -= 18;
            screen.maxBound = newMaxBound;
        }
    }

}