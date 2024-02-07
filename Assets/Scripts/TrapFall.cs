using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : MonoBehaviour
{
    Rigidbody2D rb;
    bool playerHit = false;

    public float fallSpeed = 5f; // Начальная скорость падения

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(0, -fallSpeed); // Установка начальной скорости падения
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerHit)
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }

            Debug.Log("-10");

            // Отключить коллайдер ловушки
            Collider2D trapCollider = GetComponent<Collider2D>();
            if (trapCollider != null)
            {
                trapCollider.enabled = false;
            }

            // Выключить ловушку через некоторое время
            StartCoroutine(DisableTrapAfterDelay(2f)); // Измените время, если необходимо
        }
    }

    IEnumerator DisableTrapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Если игрок вышел из зоны действия ловушки, ускорить её падение
            fallSpeed *= 1.5f; // Например, умножим скорость на 1.5
            StartCoroutine(DisableTrapAfterDelay(0.8f));
        }
    }
}
