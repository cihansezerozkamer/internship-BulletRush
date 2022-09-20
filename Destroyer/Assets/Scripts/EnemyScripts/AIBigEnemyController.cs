using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AIBigEnemyController : EnemyBase
{
    public float radius;
  
    private void Start()
    {
        transform.localScale = type.enemyScale;
        health = type.health;
        agent = GetComponent<NavMeshAgent>();


    }
    void Update()
    {
        if (PlayerController.Instance != null)
        {
            ChasePlayer();
            if (health <= 0)
            {
                LevelManager.Instance.EnemyDied();
                health = type.health;
                gameObject.SetActive(false);

            }
        }
        
    }
    private void ChasePlayer() // Arkadan dolaþmak için önce sað veya soluna sonra belli bir mesafeden sonra direk oyuncuya saldýrýr.
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > radius && transform.position.x - PlayerController.Instance.transform.position.x > 0)
        {
            agent.SetDestination(PlayerController.Instance.transform.position - PlayerController.Instance.transform.right*2);
        }
        else if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > radius && transform.position.x - PlayerController.Instance.transform.position.x <= 0)
        {
            agent.SetDestination(PlayerController.Instance.transform.position + PlayerController.Instance.transform.right * 2);
        }
        else
        {
            agent.SetDestination(PlayerController.Instance.transform.position);
        }
    }
}
    
