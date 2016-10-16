using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitPointEffect : MonoBehaviour {

	private Text Text;

	// Use this for initialization
	void Awake () {
		Text= this.GetComponentInChildren<Text>();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void deactivate() {
		gameObject.SetActive (false);
	}

	public void pop(Vector3 position, float damageValue) {
		gameObject.SetActive (true);
		Text.text = damageValue.ToString ();
		transform.position = position;
	}
}
