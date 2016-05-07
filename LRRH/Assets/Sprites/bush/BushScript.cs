using UnityEngine;
using System.Collections;
public enum BushState { stat, explode};
public class BushScript : MonoBehaviour {

    // public member
    public GameObject CoinPrefab;

    [HideInInspector]
    public BushState BushState;
    [HideInInspector]
    public BushAnim BushAnim;

    // protected member
    //protected bool m_Dead = false;

    // private member
    //private float m_DeathDuration = 1.0f;
    //private SpriteRenderer m_SpriteRenderer;

    // variable
    //private float _deathElapse = 0.0f;

	// Use this for initialization
	void Start () {
        BushState = BushState.stat;
        BushAnim = this.GetComponentInChildren<BushAnim>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void drop(){
        GameObject coinInstance = (GameObject)Instantiate(CoinPrefab);
        coinInstance.transform.position = this.transform.position;
    }


}
