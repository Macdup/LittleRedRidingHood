using UnityEngine;
using System.Collections;


public class Coin : MonoBehaviour {

    //private member
    private Rigidbody2D Rigidbody2D;
	private CoinFeedbackManager m_CoinFeedbackManager;

    void Start()
    {
		m_CoinFeedbackManager = GameObject.Find("CoinFeedbackManager").GetComponent<CoinFeedbackManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void isTook(){
		var coinEffect = m_CoinFeedbackManager.getUsableCoinFeedback();
		coinEffect.pop (transform.position,1);
		gameObject.SetActive (false);
	}

	public void pop(Vector3 position){
		transform.position = position;
		gameObject.SetActive (true);
		Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
		Rigidbody2D.AddForce(new Vector2(Random.Range(-150,150),Random.Range(50,350)),ForceMode2D.Impulse);
	}


}
