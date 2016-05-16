using UnityEngine;
using System.Collections;

public class Dropable : MonoBehaviour {
    
    // public member
    public GameObject CoinPrefab;
	public bool IsMultiDrop = false;

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

		if (IsMultiDrop) {
			int nbCoin = Random.Range (50, 100);
			for (int i = 0; i < nbCoin; ++i) {
				GameObject coinInstance2 = (GameObject)Instantiate(CoinPrefab);
				coinInstance2.transform.position = this.transform.position;
			}
		}
    }
}
