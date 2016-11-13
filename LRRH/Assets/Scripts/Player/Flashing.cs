using UnityEngine;
using System.Collections;

public class Flashing : MonoBehaviour {

	private float vel;
	private SpriteRenderer m_spriteRenderer;

	// Use this for initialization
	void Start () {
		m_spriteRenderer = this.GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float a = 0.5f + (Mathf.Sin (Time.time * 20)) * 0.5f ;
		Color col = m_spriteRenderer.color;
		col.a = a;
		m_spriteRenderer.color = col;
	}

	void OnDisable(){
		Color col = m_spriteRenderer.color;
		col.a = 1;
		m_spriteRenderer.color = col;
	}
}
