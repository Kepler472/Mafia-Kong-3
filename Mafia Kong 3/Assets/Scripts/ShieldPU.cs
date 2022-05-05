using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPU : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        // Debug.Log("Shield collision");
        if(other.CompareTag("Player")){
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        PowerUpState powerUps = player.GetComponent<PowerUpState>();
        powerUps.Shield = true;
        Destroy(gameObject);
    }
}
