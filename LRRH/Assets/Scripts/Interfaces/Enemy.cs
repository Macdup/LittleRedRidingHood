using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Enemy : MonoBehaviour
	{
		// public member
		public float Life = 100.0f;
		public float DamagePerHit = 10.0f;

		// protected member
		protected bool 		m_Dead = false;
		protected Animator 	m_Animator;

		// private member
		private SpriteRenderer 	m_SpriteRenderer;
        private Dropable        m_Dropable;

		// variable


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
		}

		virtual public void Update() {
			if (m_Dead) {
				if (m_Animator) {
					m_Animator.SetBool ("dead", true);
					AnimatorStateInfo asi = m_Animator.GetCurrentAnimatorStateInfo (0);
					if (!m_Animator.IsInTransition (0) && asi.IsName ("die_exit")) {
						DestroyImmediate (this.gameObject);
					}
				}
			}
		}

		virtual public void Hit(float iDamageValue) {
			Life -= iDamageValue;
			if(Life <= 0)
				Death();
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

	}
}

