using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level Type", menuName = "Level Type")]

public class LevelType : ScriptableObject
{
    public int BigEnemy_Number;
    public int SimpleEnemy_Number;
    public int GunnerEnemy_Number;
    public int TotalEnemy;

    public void EndlessStart()
    {
        BigEnemy_Number = 2;
        SimpleEnemy_Number = 4;
        GunnerEnemy_Number = 2;
        TotalEnemyCount();
    }
    public void IncreaseEnemy()
    {
        BigEnemy_Number += 1;
        SimpleEnemy_Number += 2;
        GunnerEnemy_Number += 1;
        TotalEnemyCount();
        
    }
    private void TotalEnemyCount()
    {
        TotalEnemy = BigEnemy_Number + SimpleEnemy_Number + GunnerEnemy_Number;
    }


}
