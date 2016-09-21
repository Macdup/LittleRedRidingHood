using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public float BombTimer;

    private Animator m_Animator;
    private int _tickingHash = Animator.StringToHash("ticking");
    private bool _isTicking = false;
	private ExplosionFeedbackManager m_ExplosionFeedbackManager;

    // Use this for initialization
    void Start () {
        m_Animator = this.GetComponent<Animator>();
		m_ExplosionFeedbackManager = GameObject.Find("ExplosionFeedbackManager").GetComponent<ExplosionFeedbackManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_isTicking == true)
            return;

        BombTimer -= 1 * Time.deltaTime;
        
        if (BombTimer < 0) {
            _isTicking = true;
            m_Animator.SetTrigger(_tickingHash);
        }
	}

    public void Disable() {
		ExplosionEffect explosion = m_ExplosionFeedbackManager.getUsableExplosionEffect ();
		explosion.pop (transform.position);
        transform.gameObject.SetActive(false);
    }

	public void pop(Vector3 position){
		transform.position = position;
		gameObject.SetActive (true);
	}
}
