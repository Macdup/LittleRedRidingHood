using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    public float LightIntensityPlayerInZone = 0.3f;
    public float LightIntensityPlayerOutZone = 1.0f;
    public float TransitionDuration = 1.0f;

    private Light m_Light;
    private bool m_PlayerEntered;
    private bool m_IsPlayerInZone;
    private bool m_Animate = false;
    private float m_LightIntensity;
    private float m_TimeStartDuration;
    


    // Use this for initialization
    void Start () {
        m_Light = this.GetComponentInParent<Light>();

        m_IsPlayerInZone = GetComponent<Collider2D>().IsTouchingLayers(LayerMask.NameToLayer("Player"));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(m_Animate)
        {
	        if(m_PlayerEntered != m_IsPlayerInZone)
            {
                m_IsPlayerInZone = m_PlayerEntered;
                m_LightIntensity = m_Light.intensity;
                m_TimeStartDuration = Time.fixedTime;
            }

            if (m_PlayerEntered)
                m_Light.intensity = Mathf.Lerp(m_LightIntensity, LightIntensityPlayerInZone, (Time.fixedTime - m_TimeStartDuration) / TransitionDuration);
            else
                m_Light.intensity = Mathf.Lerp(m_LightIntensity, LightIntensityPlayerOutZone, (Time.fixedTime - m_TimeStartDuration) / TransitionDuration);

            if (Time.fixedTime - m_TimeStartDuration >= TransitionDuration)
                m_Animate = false;
        }
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            m_Animate = true;
            m_PlayerEntered = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            m_Animate = true;
            m_PlayerEntered = false;
        }
    }
}
