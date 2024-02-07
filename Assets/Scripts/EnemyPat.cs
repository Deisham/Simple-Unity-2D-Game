using UnityEngine;

public enum EnemyState
{
    Patrol,
    Attack,
    Chase
}

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public int damage = 20;
    public float attackRange = 1f;
    public LayerMask playerLayer;
    public float attackRate = 1f;
    public float attackDelay = 0.6f; // Задержка перед атакой
    public Animator animator;

    private int waypointIndex = 0;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isPlayerInRange = false;
    private Vector2 lastPosition;
    private EnemyState currentState = EnemyState.Patrol;
    private bool isAttacking = false; // Флаг для отслеживания состояния атаки

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        isPlayerInRange = PlayerInRange();

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
        }

        UpdateAnimation();
        FlipTowardsWaypoint();
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[waypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }

        // Если игрок в зоне видимости, переключаемся в состояние преследования
        if (isPlayerInRange)
        {
            currentState = EnemyState.Chase;
        }
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackRate && GetComponent<Enemy>().IsAlive())
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
            if (playerCollider != null)
            {
                Vector3 toPlayer = (playerCollider.transform.position - transform.position).normalized;
                bool facingPlayer = FacingPlayer(toPlayer);
                if (facingPlayer && !isAttacking)
                {
                    isAttacking = true;
                    lastAttackTime = Time.time;
                    animator.SetTrigger("IsAttacking");
                    Invoke("DealDamage", attackDelay); // Вызываем метод DealDamage() с задержкой
                }
            }
        }

        // Если игрок вышел из зоны видимости, переключаемся обратно в режим патрулирования
        if (!isPlayerInRange)
        {
            currentState = EnemyState.Patrol;
        }
    }

    private void DealDamage()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (playerCollider != null)
        {
            HealthSystem playerHealth = playerCollider.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        isAttacking = false; // Сбрасываем флаг атаки
    }

    private void ChasePlayer()
    {
        // Преследуем игрока
        if (waypoints.Length > 0)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }

        // Если игрок находится в зоне атаки, переключаемся в режим атаки
        if (isPlayerInRange)
        {
            currentState = EnemyState.Attack;
        }
    }

    private void UpdateAnimation()
    {
        if (waypointIndex >= 0 && waypoints.Length > 0 && Vector2.Distance(transform.position, waypoints[waypointIndex].position) > 0.1f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void FlipTowardsWaypoint()
    {
        if ((Vector2)transform.position != lastPosition)
        {
            if (transform.position.x > lastPosition.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x < lastPosition.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            lastPosition = transform.position;
        }
    }

    private bool FacingPlayer(Vector3 toPlayer)
    {
        return (transform.localScale.x > 0f && toPlayer.x > 0f) || (transform.localScale.x < 0f && toPlayer.x < 0f);
    }

    private bool PlayerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange, playerLayer) != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 gizmoPosition = transform.position + new Vector3(1f, 1, 0);
        Gizmos.DrawWireSphere(gizmoPosition, attackRange);
    }
}
