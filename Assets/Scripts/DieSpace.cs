using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpace : MonoBehaviour
{
    public GameObject respawn;
    public HealthSystem healthSystem;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Применить урон к здоровью персонажа
            healthSystem.TakeDamage(20);

            // Респавн персонажа
            RespawnPlayer(other.transform);
        }
    }

    void RespawnPlayer(Transform playerTransform)
    {
        playerTransform.position = respawn.transform.position;
    }
}
