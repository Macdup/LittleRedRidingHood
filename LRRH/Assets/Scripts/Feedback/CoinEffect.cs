using UnityEngine;
using System.Collections;

public class CoinEffect : MonoBehaviour {

	private MeshRenderer MeshRenderer;
	private TextMesh TextMesh;

	// Use this for initialization
	void Awake () {
		MeshRenderer = this.GetComponentInChildren<MeshRenderer>();
		TextMesh= this.GetComponentInChildren<TextMesh>();
	}

	void Start () {
		MeshRenderer.sortingLayerName = "Default";
		MeshRenderer.sortingOrder = 1;
	}

	// Update is called once per frame
	void Update () {

	}

	void deactivate() {
		gameObject.SetActive (false);
	}

	public void pop(Vector3 position, float textValue) {
		transform.position = position;
		gameObject.SetActive (true);
		TextMesh.text = textValue.ToString ();
	}
}
