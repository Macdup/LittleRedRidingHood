using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public ButtonScript BSReset;  

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Backspace) || BSReset.CurrentState == ButtonScript.ButtonState.Down) {  
			SceneManager.LoadScene (0);  
		} 
	}

}
