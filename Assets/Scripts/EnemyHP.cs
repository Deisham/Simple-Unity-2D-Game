using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
 
    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool IsAlive()
    {
    return currentHealth > 0;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage called. Current Health: " + currentHealth + ", Damage: " + damage);

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

void Die()
{
    Debug.Log("Enemy died");

    animator.SetBool("IsDead", true);

    // Отключаем компоненты, которые могут вызывать действия
    GetComponent<EnemyPatrol>().enabled = false; // Отключаем скрипт патрулирования
    GetComponent<Collider2D>().enabled = false; // Отключаем коллайдер

    // Добавим задержку перед удалением объекта
    StartCoroutine(DestroyAfterAnimation());
}

    IEnumerator DestroyAfterAnimation()
    {
        // Ждем, пока проиграет анимация смерти
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Удаляем объект из сцены
        Destroy(gameObject);
    }
}
