using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  {

	public enum ButtonState {Down, Up, None};
	
	public ButtonState CurrentState;

	// Use this for initialization
	void Start () {
		CurrentState = ButtonState.None;
	}

	public void OnPointerDown (PointerEventData eventData) {
		CurrentState = ButtonState.Down;
        this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	public void OnPointerUp (PointerEventData eventData) {
		CurrentState = ButtonState.Up;
		this.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
