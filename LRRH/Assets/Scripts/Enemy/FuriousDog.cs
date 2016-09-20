﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

enum stateOfMind { normal,tired,angry };

public class FuriousDog : Enemy {

    // Member
    private BoxCollider2D m_BottomBox;

    // variable
    private float _energy = 5;
    private float _maxEnergy = 100;
    private bool _PlayerInSight;
    private stateOfMind _stateOfMind = stateOfMind.normal;
    private bool m_BottomTouched;
    private LayerMask _wallsMask;

    public override void Start()
    {
        base.Start();
        m_BottomBox = GetComponent<BoxCollider2D>();
        _wallsMask = LayerMask.GetMask("Walls");
    }

    // Use this for initialization
    public override void Update () {
        
        detectPlayer();

        if (_PlayerInSight == true && _energy > 0 && m_BottomTouched == true) {
            attack();
        }

    }

    void FixedUpdate()
    {
        // Evalute states
        m_BottomTouched = m_BottomBox.IsTouchingLayers(_wallsMask);
    }

    void detectPlayer()
    {
        Vector3 playerDir = m_Player.transform.position - transform.position;
        playerDir.y += 50;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, playerDir, 150);
        Debug.DrawRay(transform.position,playerDir.normalized * 150,Color.white);

        if (hits.Length > 0) {
            for (var i = 0; i < hits.Length; i++) {
                var player = hits[i].transform.GetComponent<Player>();
                if (player != null) {
                    _PlayerInSight = true;
                }
            }
        }

    }

    void attack() {
        Vector2 playerDir = playerDir = m_Player.transform.position - transform.position;
        playerDir.y += 50;
        m_RigidBody.AddForce(playerDir.normalized * 100, ForceMode2D.Impulse);
        _energy = _energy -  1/5f;
        if (_energy < 0)
            StartCoroutine(FillEnergy());
    }

    IEnumerator FillEnergy() {
        yield return new WaitForSeconds(2.0f);
        _energy = 5;
    }


  
}