
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomMovement : MonoBehaviour
{
    [Header("Wandering Settings")]
    public float wanderRadius = 10f;

    [Header("Speed Settings")]
    public Vector2 speedRange = new Vector2(1f, 20f);

    [Tooltip("If set to >= 0, this speed overrides the random speed.")]
    public float customSpeed = -1f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Use customSpeed if set, else randomize
        if (customSpeed >= 0f)
            agent.speed = customSpeed;
        else
            agent.speed = Random.Range(speedRange.x, speedRange.y);

        agent.acceleration = agent.speed * 2f;
        agent.angularSpeed = 360f;
        agent.stoppingDistance = 0.1f;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} not on NavMesh!");
        }

        SetNewDestination();
    }

    void Update()
    {
        if (!agent.isOnNavMesh || agent.pathPending || !agent.enabled)
            return;

        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
        }

        if (animator != null)
        {
            bool isMoving = agent.velocity.sqrMagnitude > 0.01f;
            animator.SetBool("IsWalking", isMoving);
            animator.speed = isMoving ? Mathf.Clamp(agent.velocity.magnitude / agent.speed, 0.5f, 1.5f) : 1f;
        }
    }

    void SetNewDestination()
    {
        Vector3 newPos = GetRandomPointOnNavMesh(transform.position, wanderRadius);
        agent.SetDestination(newPos);
    }

    Vector3 GetRandomPointOnNavMesh(Vector3 origin, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPos = origin + Random.insideUnitSphere * radius;
            randomPos.y = origin.y;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return origin;
    }
}
