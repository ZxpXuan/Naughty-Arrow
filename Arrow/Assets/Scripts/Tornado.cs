using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField]
    float torque;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var rg = collider.GetComponent<Rigidbody2D>();
        if (!rg) return;

        rg.velocity = Quaternion.Euler(0, 0, torque) * rg.velocity;
    }
}
