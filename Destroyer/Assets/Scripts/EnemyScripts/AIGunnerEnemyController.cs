using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGunnerEnemyController : EnemyBase
{
    [SerializeField] private ShootControl shootController;
    public Transform tipofGun;
    public Queue<Transform> PlayerPositions = new Queue<Transform>();

  
    private void ChasePlayer() // Oyuncuyu takip eder.
    {
        if (PlayerController.Instance != null)
            agent.SetDestination(PlayerController.Instance.transform.position);
    }
    
    private void Start()
    {
        transform.localScale = type.enemyScale;
        health = type.health;
        agent= GetComponent<NavMeshAgent>();
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
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            if (!PlayerPositions.Contains(other.transform)) PlayerPositions.Enqueue(other.transform);
            transform.LookAt(other.transform.position);
            agent.isStopped = true;
            AutoShoot();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
       agent.isStopped=false;
    }

    private bool isShooting;
    private void AutoShoot()
    {

        IEnumerator Do()
        {
            while (PlayerPositions.Count > 0)
            {

                var player = PlayerPositions.Dequeue();
                if (player != null && player.gameObject.activeSelf)
                {
                    var direction = player.transform.position - transform.position;
                    direction.y = 0;
                    direction = direction.normalized;
                    shootController.EnemyShoot(direction, tipofGun.position);
                    yield return new WaitForSeconds(shootController.Delay);
                }
            }
            isShooting = false;
        }
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(Do());
        }
    }
}
            