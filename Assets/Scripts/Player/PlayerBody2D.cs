using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody2D : MonoBehaviour {
    PlayerControl2D playerScript;
    void Awake()
    {
        playerScript = GetComponentInParent<PlayerControl2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collider = other.gameObject;
        if (collider.CompareTag("Enemy"))
            playerScript.SetDamageFrom(collider);
    }
}
