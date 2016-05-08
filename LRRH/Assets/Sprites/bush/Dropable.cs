using UnityEngine;
using System.Collections;

public class Dropable : MonoBehaviour {
    
    // public member
    public GameObject CoinPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void drop()
    {
        GameObject coinInstance = (GameObject)Instantiate(CoinPrefab);
        coinInstance.transform.position = this.transform.position;
    }
}
