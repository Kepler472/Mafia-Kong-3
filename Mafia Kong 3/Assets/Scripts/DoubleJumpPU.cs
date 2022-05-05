using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPU : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        // Debug.Log("Double Jump collision");
        if(other.CompareTag("Player")){
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        PowerUpState powerUps = player.GetComponent<PowerUpState>();
        powerUps.DoubleJumps = 3;
        Destroy(gameObject);
    }
}
