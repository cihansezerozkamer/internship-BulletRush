using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour,IAmDamagable
{
    public NavMeshAgent agent;
    public int health;
    public EnemyType type;
    

    public bool TakeDamage(int damage)
    {
        health -= damage;
        return health <= 0;
    }
   
    

}
