using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnityEvents : MonoSingleton<UnityEvents>
{
    [Serializable] public class OnRestart : UnityEvent { }
    [Serializable] public class OnNextLevel : UnityEvent { }
    
    [Serializable] public class OnWin : UnityEvent { }
    [Serializable] public class OnTouchFirst : UnityEvent { }
    [Serializable] public class OnDeadEndless : UnityEvent { }

    public OnWin onWin;
    public OnNextLevel onNextLevel;
    public OnTouchFirst onTouchFirst;
    public OnRestart onRestart;
    public OnDeadEndless onDead;

    public bool isStart=true;
    public bool isLevel = true;
    
    private void LateUpdate()
    {
        if (isLevel)
        {
            if(LevelManager.Instance.TotalEnemy == 0)
            {
                if(LevelManager.Instance.levelType + 1 < LevelManager.Instance.Levels.Length)
                {
                    onNextLevel.Invoke();
                }
                else
                {
                    onWin.Invoke();
                }
            }
            if (ScreenTouch.Instance.pivotPOP.enabled.Equals(true) && isStart)
            {
                isStart = false;
                onTouchFirst.Invoke();
                LevelManager.Instance.RandomSpawn(LevelManager.Instance.Level);
            }
        }
        else
        {
            if (ScreenTouch.Instance.pivotPOP.enabled.Equals(true) && isStart)
            {
                isStart = false;
                onTouchFirst.Invoke();
                LevelManager.Instance.EndlessModeStart();
                LevelManager.Instance.RandomSpawn(LevelManager.Instance.Level);
            }
            if (LevelManager.Instance.TotalEnemy == 0 && LevelManager.Instance.Level != null)
            {
                onTouchFirst.Invoke();
                LevelManager.Instance.Level.IncreaseEnemy();
                LevelManager.Instance.goEndless();  
            }
        }
    }
    public void MakeLevelMode()
    {
        isLevel = true;
        isStart = true;
        LevelManager.Instance.LevelModeStart();

    }
    public void MakeEndlessMode()
    {
        isLevel = false;
        isStart = true;
        LevelManager.Instance.EndlessModeStart();
    }
}
