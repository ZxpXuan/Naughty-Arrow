using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : MonoBehaviour
{
    [SerializeField]
    public Transform Character;
    private Rigidbody2D rb;
    private Vector2 a;
    private Vector2 b;
    public float pw_x = 400f;

    void Start() {
        rb = Character.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        a = transform.position -rb.transform.position;
        b = a.normalized;
        rb.gameObject.GetComponent<Rigidbody2D>().AddForce(b * pw_x);
        
    }
}