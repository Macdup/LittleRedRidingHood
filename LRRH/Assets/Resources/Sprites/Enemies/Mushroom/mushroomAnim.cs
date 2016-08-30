using UnityEngine;
using System.Collections;

public class mushroomAnim : MonoBehaviour {

    private Mushroom mushroom;

	// Use this for initialization
	void Start () {
		mushroom = GetComponentInParent<Mushroom>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startShot() {
		mushroom.shot();
    }
}
