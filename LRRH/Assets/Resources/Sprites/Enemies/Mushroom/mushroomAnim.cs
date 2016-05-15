using UnityEngine;
using System.Collections;

public class mushroomAnim : MonoBehaviour {

    private MushroomScript mushroomScript;

	// Use this for initialization
	void Start () {
        mushroomScript = GetComponentInParent<MushroomScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startShot() {
        mushroomScript.shot();
    }
}
