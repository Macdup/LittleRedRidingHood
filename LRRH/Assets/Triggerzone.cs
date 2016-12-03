using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerzone : MonoBehaviour {

    public GameObject Listener;
    public GameObject Target;

    private TriggerListener m_tl;

    // Use this for initialization
    void Start () {
        m_tl = (TriggerListener)Listener.GetComponent<TriggerListener>();
        if (m_tl == null)
            Object.Destroy(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Target)
            m_tl.OnTriggerzoneEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == Target)
            m_tl.OnTriggerzoneExit2D(collision);
    }
}
