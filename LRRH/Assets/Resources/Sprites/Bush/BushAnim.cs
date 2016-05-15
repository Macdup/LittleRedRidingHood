using UnityEngine;
using System.Collections;

public class BushAnim : MonoBehaviour {

    // public member
    // protected member
    // private member
    private Animator m_anim;
    // variable
    private int _explodeHash = Animator.StringToHash("explode");
   


	// Use this for initialization
	void Start () {
        m_anim = this.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void explode() {
        m_anim.SetTrigger(_explodeHash);
    }

    void destroy()
    {
        GameObject.Destroy(transform.parent.gameObject);
    }
}
