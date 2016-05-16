using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public RectTransform LifeBar;
	public Text ScoreText;

	private int m_LifeBarSize;
	private int m_Score;


	void Start () {
		m_LifeBarSize = (int)LifeBar.sizeDelta.x;
		m_Score = 0;
	}

	public void OnEnable () {
		Events.instance.AddListener<PlayerHit>(onPlayerHit);
		Events.instance.AddListener<PlayerLoot>(onPlayerLoot);
	}
	void OnDestroy() {
		Events.instance.RemoveListener<PlayerHit> (onPlayerHit);
		Events.instance.RemoveListener<PlayerLoot>(onPlayerLoot);
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
}
