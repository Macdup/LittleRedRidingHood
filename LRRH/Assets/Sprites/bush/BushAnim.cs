using UnityEngine;
using System.Collections;

public class BushAnim : MonoBehaviour {

    // public member
    // protected member
    // private member
    private Animator _anim;
    private BushScript _BushScript;
    // variable
    private int _explodeHash = Animator.StringToHash("explode");
   


	// Use this for initialization
	void Start () {
        _anim = this.GetComponentInChildren<Animator>();
        _BushScript = this.GetComponentInParent<BushScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void explode() {
        _anim.SetTrigger(_explodeHash);
    }

    void destroy()
    {
        _BushScript.drop();
        GameObject.Destroy(transform.parent.gameObject);
    }
}
