using UnityEngine;
using System.Collections;

public class mushroomAnim : MonoBehaviour {

    private mushroomScript mushroomScript;

	// Use this for initialization
	void Start () {
        mushroomScript = GetComponentInParent<mushroomScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startShot() {
        mushroomScript.shot();
    }
}
