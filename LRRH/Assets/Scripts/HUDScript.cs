using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public RectTransform LifeBar;
    public RectTransform StaminaBar;
    public RectTransform ManaBar;
	public Text ScoreText;
	public Image JetpackGague;

	private int m_LifeBarSize;
    private int m_StaminaBarSize;
    private int m_ManaBarSize;
	private int m_Score;


	void Start () {
		m_LifeBarSize = (int)LifeBar.sizeDelta.x;
        m_StaminaBarSize = (int)StaminaBar.sizeDelta.x;
        m_ManaBarSize = (int)ManaBar.sizeDelta.x;
		m_Score = 0;
	}

	public void OnEnable () {
		Events.instance.AddListener<PlayerHit>(onPlayerHit);
		Events.instance.AddListener<PlayerLoot>(onPlayerLoot);
        Events.instance.AddListener<PlayerDefend>(onPlayerDefend);
        Events.instance.AddListener<PlayerSpell>(onPlayerSpell);
		Events.instance.AddListener<PlayerJetpackValueChanged>(onPlayerJetpackValueChanged);
	}
	void OnDestroy() {
		Events.instance.RemoveListener<PlayerHit> (onPlayerHit);
		Events.instance.RemoveListener<PlayerLoot>(onPlayerLoot);
        Events.instance.RemoveListener<PlayerDefend>(onPlayerDefend);
        Events.instance.RemoveListener<PlayerSpell>(onPlayerSpell);
		Events.instance.RemoveListener<PlayerJetpackValueChanged>(onPlayerJetpackValueChanged);
	}



	void Update () {
	}


	void onPlayerHit (PlayerHit e) {
		Vector2 actualSize = LifeBar.sizeDelta;
		actualSize.x = e.Health * m_LifeBarSize / 100;
		LifeBar.sizeDelta = actualSize;
	}

	void onPlayerLoot (PlayerLoot e) {
		++m_Score;
		ScoreText.text = m_Score.ToString();
	}

    void onPlayerDefend(PlayerDefend e)
    {
        Vector2 actualSize = StaminaBar.sizeDelta;
        actualSize.x = e.Stamina * m_StaminaBarSize / 100;
        StaminaBar.sizeDelta = actualSize;
    }
    void onPlayerSpell(PlayerSpell e)
    {
        Vector2 actualSize = ManaBar.sizeDelta;
        actualSize.x = e.Mana * m_ManaBarSize / 100;
        ManaBar.sizeDelta = actualSize;
    }

	void onPlayerJetpackValueChanged(PlayerJetpackValueChanged e)
	{
		JetpackGague.fillAmount = (e.JetpackDuration - e.JetpackValue) / e.JetpackDuration;
	}

}
