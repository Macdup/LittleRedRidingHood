using UnityEngine;
using System.Collections;

public class PlantAnim : MonoBehaviour {

	private PlantScript PlantScript;

	// Use this for initialization
	void Start () {
		PlantScript = GetComponentInParent<PlantScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shot(){
		PlantScript.shot();
	}
}
