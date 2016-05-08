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
		protected bool m_Dead = false;

		// private member
		private float 			m_DeathDuration = 1.0f;
		private SpriteRenderer 	m_SpriteRenderer;
        private Dropable        m_Dropable;

		// variable
		private float _deathElapse = 0.0f;


		virtual public void Start() {
			m_SpriteRenderer = this.GetComponent<SpriteRenderer> ();
			if (m_SpriteRenderer == null) {
				m_SpriteRenderer = this.GetComponentInChildren<SpriteRenderer> ();
			}
            m_Dropable = GetComponent<Dropable>();
		}

		virtual public void Update() {
			if (m_Dead) {
				if (_deathElapse <= m_DeathDuration) {
					_deathElapse += Time.deltaTime;
					Color c = m_SpriteRenderer.color;
					c.a = 1.0f - (_deathElapse / m_DeathDuration);
					m_SpriteRenderer.color = c;
				} else {
					DestroyImmediate (this);
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

