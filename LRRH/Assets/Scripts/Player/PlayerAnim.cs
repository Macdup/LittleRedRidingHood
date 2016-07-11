using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    private Player playerScript;
    private Spell spellScript; 

	// Use this for initialization
	void Start () {
        playerScript = GetComponentInParent<Player>();
        //spellScript = playerScript.CurrentSpell;
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
    public void SpellFX()
    {
        spellScript.SpellFX();
    }
    public void FireStick()
    {
        playerScript.FireStick();
    }

    public void DashForward()
    {
        playerScript.DashForward();
    }
}
