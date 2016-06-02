using UnityEngine;
using System.Collections;

public class Heal : Spell
{
    // public member
    public float HealAmount = 100.0f;

    int _HealHash = Animator.StringToHash("Heal");

    public override void launchSort() {
        float manaTest =  Player.Mana - ManaCost;
        if (manaTest > 0) {
            Player.Mana = manaTest;
            Anim.SetTrigger(_HealHash);
            PlayerSpell MagicEvent = new PlayerSpell(manaTest);
            Events.instance.Raise(MagicEvent);
            Player.Health += HealAmount;
            if (Player.Health > Player.HealthMax) {
                Player.Health = Player.HealthMax;
            }
            PlayerHit PlayerHitEvent = new PlayerHit(Player.Health);
            Events.instance.Raise(PlayerHitEvent);
        }
    }

    public void resetMagic() {
        Player.UsingMagic = false;
    }

    public override void SpellFX()
    {
        Vector3 abovePlayer = Player.transform.position;
        abovePlayer.y += 50;
        GameObject instanceFX = (GameObject) Instantiate(Fx, transform.position, Quaternion.identity);
        GameObject instanceText = (GameObject)Instantiate(StatFeedback, abovePlayer, Quaternion.identity);
        instanceText.GetComponentInChildren<TextMesh>().color = Color.cyan;
        instanceText.GetComponentInChildren<TextMesh>().text = HealAmount.ToString();
    }
    

}
