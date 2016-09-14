using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(Tile))]
public class TileEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Build Collider"))
        {
            createCollider();
        }
        if (GUILayout.Button("Select Zones"))
        {
            getZones();
        }
        EditorGUILayout.EndVertical();
    }

    public void createCollider()
    {

        // On récupère l'ensemble des colliders de la sélection courante
        var selectionList = Selection.transforms;
        List<Vector2> tileCollidersPointPositionList = new List<Vector2>();

        // On crée un objet dans la zone d'une des tiles de la sélection
        // Et on place le tile de la sélection sous l'objet créé
        var go = new GameObject();
        go.name = "HiddenWalls";
        go.transform.SetParent(selectionList[0].transform.parent);
        for (var i = 0; i < selectionList.Length; i++)
        {
            selectionList[i].transform.SetParent(go.transform);
        }

        //Pour chaque BoxCollider2D de la sélection courante, on récupère la position des coins
        for (var i = 0; i < selectionList.Length; i++)
        {
            var collider = selectionList[i].GetComponent<BoxCollider2D>();
            var center = collider.bounds.center;
            var size = collider.size;
            var point1 = new Vector2(center.x - size.x / 2, center.y - size.y / 2);
            var point2 = new Vector2(center.x + size.x / 2, center.y - size.y / 2);
            var point3 = new Vector2(center.x + size.x / 2, center.y + size.y / 2);
            var point4 = new Vector2(center.x - size.x / 2, center.y + size.y / 2);
            tileCollidersPointPositionList.Add(point1);
            tileCollidersPointPositionList.Add(point2);
            tileCollidersPointPositionList.Add(point3);
            tileCollidersPointPositionList.Add(point4);
            DestroyImmediate(collider);
            selectionList[i].gameObject.SetActive(false);
        }

        Vector2[] colliderPointPositionArray = tileCollidersPointPositionList.ToArray();

        //il faut maintenant enlever les points qui ne sont pas constitutifs des bordures du collider formé par l'assemble des tiles sélectionnées
        List<Vector2> tileColliderPointPositionList = new List<Vector2>();
        for (var i = 0; i < tileCollidersPointPositionList.Count; i++)
        {
            var positionCount = tileCollidersPointPositionList.FindAll(j => j == tileCollidersPointPositionList[i]);
            if (positionCount.Count == 1 || positionCount.Count == 3)
            {
                var isInLastList = tileColliderPointPositionList.FindAll(k => k == tileCollidersPointPositionList[i]);
                if (isInLastList.Count == 0)
                    tileColliderPointPositionList.Add(tileCollidersPointPositionList[i]);
            }
        }
        // Il faut maintenant ordonner les points suivant l'ordre logique de liaison
        // Prendre un point de départ.
        // On crée un 2DPolygonCollider à partie des points
        Vector2[] polycolPoints = tileColliderPointPositionList.ToArray();
        var polycol = go.AddComponent<PolygonCollider2D>();
        polycol.isTrigger = true;
        go.AddComponent<HiddenWalls>();
        polycol.points = polycolPoints;
    }

    public void getZones() {
        var selectionList = Selection.transforms;
        List<GameObject> ZoneList = new List<GameObject>();
        for (var i = 0; i < selectionList.Length; i++) {
            ZoneList.Add(selectionList[i].GetComponentInParent<Screen>().gameObject);
        }
        Selection.objects = ZoneList.ToArray();
    }

}