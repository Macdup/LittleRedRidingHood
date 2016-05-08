using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public ButtonScript BSReset;  
	public float TimeScale = 1.0f;

	// Use this for initialization
	void Start () {
		// FOR DEBUG
		Time.timeScale = TimeScale;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Backspace) || BSReset.CurrentState == ButtonScript.ButtonState.Down) {  
			SceneManager.LoadScene (0);  
		} 
	}

}
