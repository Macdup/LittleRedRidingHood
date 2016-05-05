using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;


[ExecuteInEditMode]
public class PolygonColliderGenerator : MonoBehaviour {


	GameObject Level;
	public TextAsset textXML;
	public bool	Execute = false;
	public float Multiplier = 100;

	// Use this for initialization
	void Start () {
		Level = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Execute) {
			SetCollider (Level);
			Execute = false;
		}
	}

	void SetCollider(GameObject iLevel)
	{
		//Clear
		PolygonCollider2D[] poly = iLevel.GetComponents<PolygonCollider2D>();
		foreach(PolygonCollider2D p in poly)
			DestroyImmediate(p);

		PolygonCollider2D collider = iLevel.AddComponent<PolygonCollider2D> ();

		string spriteName = iLevel.GetComponent<SpriteRenderer> ().sprite.name;

		//Load Xml
		if(textXML != null)
		{
			XmlDocument xmlDoc = new XmlDocument(); 
			xmlDoc.LoadXml(textXML.text); 

			//Start parsing
			XmlNodeList pathList = xmlDoc.GetElementsByTagName("PathItem");

			for(int i=0 ; i<pathList.Count ; ++i)
			{
				XmlNode pathItem = pathList[i];
				Vector2[] points = new Vector2[pathItem.ChildNodes.Count];
				for(int j=0 ; j<pathItem.ChildNodes.Count ; ++j)
				{
					XmlNode pathPoint = pathItem.ChildNodes[j];
					Vector2 point;
					point.x = float.Parse (pathPoint.Attributes ["x"].Value) * Multiplier;
					point.y = float.Parse (pathPoint.Attributes ["y"].Value) * Multiplier;
					points [j] = point;
				}
				collider.SetPath (i, points);
			}
		}
	}
}
	


