using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    private Player playerScript;
    private Spell spellScript;
    public Vector3 PlayerPosition;

	// Use this for initialization
	void Start () {
        playerScript = GetComponentInParent<Player>();
        PlayerPosition = this.GetComponentInParent<Transform>().position;
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
        //playerScript.ResetBeingHit();
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
        playerScript.Dash(playerScript.AttackLongDashSpeed);
    }

    public void Dash()
    {
        playerScript.Dash(playerScript.DashAttackSpeed);
    }

    public void CounterBackDash()
    {
        playerScript.Dash(-playerScript.CounterAttackBackDashSpeed);
    }

    public void CounterForwardDash()
    {
        playerScript.Dash(playerScript.CounterAttackForwardDashSpeed);
    }

    public void PlaySoundAttack1()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.AudioClipAttack1);
    }
    public void PlaySoundAttack2()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.AudioClipAttack2);
    }
    public void PlaySoundAttack3()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.AudioClipAttack3);
    }
    public void PlaySoundJump1()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.AudioClipJump1);
    }
    public void PlaySoundJump2()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.AudioClipJump2);
    }
}
