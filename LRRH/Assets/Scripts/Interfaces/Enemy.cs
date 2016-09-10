using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Enemy : MonoBehaviour
	{
		// public member
		public float Life = 100.0f;
		public float DamagePerHit = 10.0f;
		public float StopDurationAfterBeingHit = 0.2f;
        public float NoDamageDurationAfterBeingHit = 0.1f;
        public bool  IsBumpable = false;
		public bool  DoesBumpPlayer = false;
		public float BumpForce = 0.0f;
        public float staminaLossPerHit;

		// protected member
		protected bool 		m_Dead = false;
		public bool 		m_BeingHit = false;
        public bool         m_Stopped = false;
        protected Animator 	m_Animator;


		// private member
		private SpriteRenderer 	m_SpriteRenderer;
        private Dropable        m_Dropable;
        public Player          m_Player;
		private HitFeedbackManager m_HitFeedbackManager;

		// variable
        public bool _isInCounterTime = false;

		virtual public void Start() {
			m_SpriteRenderer = this.GetComponent<SpriteRenderer> ();
			if (m_SpriteRenderer == null) {
				m_SpriteRenderer = this.GetComponentInChildren<SpriteRenderer> ();
			}
            m_Dropable = GetComponent<Dropable>();

			m_Animator = GetComponentInChildren<Animator>();
			if (m_Animator == null) {
				m_Animator = this.GetComponentInChildren<Animator> ();
			}
            m_Player = GameObject.Find("Player").GetComponent<Player>();
			m_HitFeedbackManager = GameObject.Find("HitFeedbackManager").GetComponent<HitFeedbackManager>();
		}

		virtual public void Update() {
			if (m_Dead) {
				if (m_Animator) {
					m_Animator.SetBool ("dead", true);
				}
				Rigidbody2D rb = this.GetComponent<Rigidbody2D> ();
				if (rb != null)
					rb.velocity = Vector2.zero;
			}
		}

		virtual public void OnTriggerEnter2D(Collider2D other) {
			Player player = other.gameObject.GetComponent<Player> ();
			if (player != null) {
				player.Hit(DamagePerHit,staminaLossPerHit);

				//Bump player
				if (DoesBumpPlayer) {
					player.Bump(this.transform.position, BumpForce);
				}
			}
		}

		virtual public void Hit(float iDamageValue) {
			if (!m_BeingHit) {
				m_BeingHit = true;
                m_Stopped = true;
                

                if (m_Animator)
	            {
	                m_Animator.SetTrigger("hit");
					Vector3 feedbackPosition = transform.position;
					feedbackPosition.y += GetComponent<BoxCollider2D> ().size.y;
					HitPointEffect hit = m_HitFeedbackManager.getUsableHitPointEffect ();
					hit.pop(feedbackPosition, iDamageValue);
	            }
				Life -= iDamageValue;
				if (Life <= 0)
					Death ();
				else {
					Invoke ("ResetHitCoolDown", NoDamageDurationAfterBeingHit);
                    Invoke("ResetStoppedCoolDown", StopDurationAfterBeingHit);
                }
			}
		}

		virtual public void Bump(Vector3 iSourcePosition, float iBumpForce) {
			if (IsBumpable) {
				Rigidbody2D rb = this.GetComponent<Rigidbody2D> ();
				if (rb == null)
					return;
				
				Vector2 bumpDir = this.transform.position.x>iSourcePosition.x? new Vector2(iBumpForce,iBumpForce) : new Vector2(-iBumpForce,iBumpForce);
                //rb.velocity += bumpDir;
                rb.AddForce(bumpDir);
			}
		}

		virtual public void Death() {
			m_Dead = true;
			Collider2D[] colliders = this.GetComponents<Collider2D> ();
			foreach (Collider2D c in colliders) {
				c.enabled = false;
			}

			colliders = this.GetComponentsInChildren<Collider2D> ();
			foreach (Collider2D c in colliders) {
				c.enabled = false;
			}

            if(m_Dropable != null)
                m_Dropable.drop();
		}


		virtual public void ResetHitCoolDown() {
			m_BeingHit = false;	
		}

        virtual public void ResetStoppedCoolDown()
        {
            m_Stopped = false;
        }

    }
}

