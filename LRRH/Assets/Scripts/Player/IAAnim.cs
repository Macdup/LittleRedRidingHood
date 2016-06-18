using UnityEngine;
using System.Collections;

public class IAAnim : MonoBehaviour
{
    public bool counterPossibility = false;

    private IATest iaScript;
    private Spell spellScript; 

	// Use this for initialization
	void Start () {
        iaScript = GetComponentInParent<IATest>();
        //spellScript = playerScript.CurrentSpell;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void destroy(){
		GameObject.Destroy(transform.parent.gameObject);
	}

    public void idleEnd() {
        iaScript.SetIdle(false);
    }

    public void SetComboPossibility() {
        iaScript.SetComboPossibility();
    }

    public void ResetComboValidated() {
        iaScript.ResetComboValidated();
    }

    public void ComboCheck()
    {
        iaScript.ComboCheck();
    }

    public void ResetBeingHit() {
        iaScript.ResetBeingHit();
    }

    public void ResetUsingMagick()
    {
        iaScript.UsingMagic = false;
    }
    public void SpellFX()
    {
        spellScript.SpellFX();
    }

    public void setCounterPossibilityOn() {
        counterPossibility = true;
    }
    public void setCounterPossibilityOff()
    {
        counterPossibility = false;
    }


}
