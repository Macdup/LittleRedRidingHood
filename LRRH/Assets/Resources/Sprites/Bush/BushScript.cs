using UnityEngine;
using System.Collections;
public enum BushState { stat, explode};
public class BushScript : MonoBehaviour {

    // public member

    [HideInInspector]
    public BushState BushState;
    [HideInInspector]
    public BushAnim BushAnim;

    // protected member
    //protected bool m_Dead = false;

    // private member
    private BoxCollider2D m_BoxCollider2D;
    private Dropable m_Dropable;

    // variable
    //private float _deathElapse = 0.0f;

	// Use this for initialization
	void Start () {
        BushState = BushState.stat;
        BushAnim = this.GetComponentInChildren<BushAnim>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        m_Dropable = GetComponent<Dropable>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void hit() {
        m_BoxCollider2D.enabled = false;
        BushAnim.explode();
        m_Dropable.drop();
    }


}
