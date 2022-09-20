using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemyController : EnemyBase
{   
  
    private void Start()
    {
        transform.localScale = type.enemyScale;
        health = type.health;
        agent = GetComponent<NavMeshAgent>();

    }
    void Update()
    {
        ChasePlayer();
        if (health <= 0)
        {
            LevelManager.Instance.EnemyDied();
            health = type.health;
            gameObject.SetActive(false);
        }
    }
    private void ChasePlayer() // Oyuncuyu takip eder.
    {
        if (PlayerController.Instance != null)
        agent.SetDestination(PlayerController.Instance.transform.position);
    }
}

