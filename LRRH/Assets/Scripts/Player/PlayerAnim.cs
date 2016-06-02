using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    private Player playerScript; 

	// Use this for initialization
	void Start () {
        playerScript = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void destroy(){
		GameObject.Destroy(transform.parent.gameObject);
	}

    public void idleEnd() {
        playerScript.SetIdle(false);
    }

    public void SetComboPossibility() {
        playerScript.SetComboPossibility();
    }

    public void ResetComboValidated() {
        playerScript.ResetComboValidated();
    }

    public void ComboCheck()
    {
        playerScript.ComboCheck();
    }

    public void ResetBeingHit() {
        playerScript.ResetBeingHit();
    }

    public void ResetUsingMagick()
    {
        playerScript.UsingMagic = false;
    }
}
