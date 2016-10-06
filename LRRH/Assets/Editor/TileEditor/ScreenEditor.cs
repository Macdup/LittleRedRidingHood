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
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				tile.position.y += 12;
			}
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
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				tile.position.y -= 12;
			}
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
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				tile.position.x += 17;
			}
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
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				tile.position.x -= 17;
			}
        }
    }

}