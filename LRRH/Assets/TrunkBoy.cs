using AssemblyCSharp;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrunkBoy : Enemy, TriggerListener
{
    // public
    public float MoveSpeed = 1.0f;

    bool m_Triggered = false;


    // Update is called once per frame
    public override void Update()
    {
        if(m_Triggered)
        {
            Vector3 moveToPlayer = m_Player.transform.position - transform.position;

            if (moveToPlayer.magnitude < 10) // close enough from target
                Attack();
            else
            {
                moveToPlayer.y = 0;
                moveToPlayer.Normalize();
                m_RigidBody.velocity = (moveToPlayer * MoveSpeed);
            }
        }
    }

    void Attack()
    {

    }


    public override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
    }


    void TriggerListener.OnTriggerzoneEnter2D(Collider2D collision)
    {
        m_Triggered = true;
    }

    void TriggerListener.OnTriggerzoneExit2D(Collider2D collision)
    {
        m_Triggered = false;
    }
}