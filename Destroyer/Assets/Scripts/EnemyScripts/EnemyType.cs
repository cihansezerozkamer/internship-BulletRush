using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Type",menuName = "Enemy Type") ]
public class EnemyType : ScriptableObject
{
    public int health = 100;
    public Vector3 enemyScale = Vector3.one;
}
