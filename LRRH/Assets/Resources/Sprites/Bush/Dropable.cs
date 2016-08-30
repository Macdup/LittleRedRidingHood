using UnityEngine;
using System.Collections;

public class Dropable : MonoBehaviour {
    
    // public member
	public bool IsMultiDrop = false;
	private CoinManager m_CoinManager;

	// Use this for initialization
	void Start () {
		m_CoinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void drop()
    {
		var coin = m_CoinManager.getUsableCoin();
		coin.pop (transform.position);

		// J'ai cassé le multidrop pour l'instant. A réparer.
		/*if (IsMultiDrop) {
			int nbCoin = Random.Range (50, 100);
			for (int i = 0; i < nbCoin; ++i) {
				GameObject coinInstance2 = (GameObject)Instantiate(CoinPrefab);
				coinInstance2.transform.position = this.transform.position;
			}
		}*/
    }
}
