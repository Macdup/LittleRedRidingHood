using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{

	public enum ButtonState {Down, Up, None};

	
	public ButtonState CurrentState;
    public bool HandleEnterExit = true;

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

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!HandleEnterExit) return;
        if (CurrentState == ButtonState.Down)
        {
            CurrentState = ButtonState.Up;
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!HandleEnterExit) return;
        CurrentState = ButtonState.Down;
        this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

        // Update is called once per frame
        void Update () {
	}
}
