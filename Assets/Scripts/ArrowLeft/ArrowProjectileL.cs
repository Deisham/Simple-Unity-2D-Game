using UnityEngine;

public class ArrowProjectileLeft : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 5f;

    public void Launch(Vector2 dir)
    {
        direction = -dir.normalized;

        // Вызываем метод DestroyArrow через 2 секунды
        Invoke("DestroyArrow", 2f);
    }

    private void Update()
    {
        // Двигаем стрелу в заданном направлении
        transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка столкновения со спрайтом игрока
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }

            // Отменяем вызов DestroyArrow, чтобы избежать лишнего уничтожения
            CancelInvoke("DestroyArrow");

            Destroy(gameObject); // Уничтожаем стрелу после попадания
        }
    }

    // Метод для уничтожения стрелы
    private void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
