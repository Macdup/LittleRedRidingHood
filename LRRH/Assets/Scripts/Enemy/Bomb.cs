using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public float BombTimer;

    private Animator m_Animator;
    private int _tickingHash = Animator.StringToHash("ticking");
    private bool _isTicking = false;

    // Use this for initialization
    void Start () {
        m_Animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_isTicking == true)
            return;

        BombTimer -= 1 * Time.deltaTime;
        
        if (BombTimer < 0) {
            _isTicking = true;
            Debug.Log(BombTimer);
            m_Animator.SetTrigger(_tickingHash);
        }
	}

    public void Disable() {
        transform.gameObject.SetActive(false);
    }
}
