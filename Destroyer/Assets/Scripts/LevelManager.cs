using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private ObjectPool EnemyPool;
    [SerializeField] public LevelType[] Levels;
    [SerializeField] private LevelType EndlessMode;
    public int levelType;
    public LevelType Level;
    public Vector3 playerStart;

    private int PoolCounter;
    private int ActiveEnemyCounter;

    public TextMeshProUGUI EnemyCounter_Text;
    public TextMeshProUGUI KilledEnemyCounter_Text;
    public TextMeshProUGUI HighScore_Text;


    public GameObject GFX;
    public GameObject restart;
    public GameObject winText;
    public GameObject nextLevel;

    public Transform player;
    
    private Vector3 MinB;
    private Vector3 MaxB;
    private Vector3 MinS;
    private Vector3 MaxS;
    private Vector3 _randomPosition;

    private float _xAxis;
    private float _zAxis;
    public float KnockBackForce = 10;


    public int TotalEnemy=0;
    public int TotalKilled=0;
    public bool isShielded;
    public float PowerUPSeconds= 6.0f;
    private void Start()
    {
        playerStart = new Vector3(0, 1, 0);
        SetBigRanges();
        SetSimpleRanges();
        HighScore_Text.text = "Highest Kill Score\n" + PlayerPrefs.GetInt("BestScore");
    }
    public void LevelModeStart()
    {
        player.transform.position = playerStart;
        levelType = 0;
        Level = Levels[levelType];
        TotalEnemy = Level.TotalEnemy;
        EnemyCounter_Text.text = TotalEnemy.ToString();
    }
    public void EndlessModeStart()
    {
        player.transform.position = playerStart;
        Level = EndlessMode;
        Level.EndlessStart();
        TotalEnemy = Level.TotalEnemy;
        EnemyCounter_Text.text = TotalEnemy.ToString();

    }
    public void gonextLevel() // yeni levela geçiþ için gerekli butonu çaðýrýr.
    {
     
        levelType += 1;
        Level = Levels[levelType];
        TotalEnemy = Level.TotalEnemy;
        EnemyCounter_Text.text = TotalEnemy.ToString();
        player.transform.position = playerStart;
        UnityEvents.Instance.isStart = true;
        UnityEvents.Instance.onRestart.Invoke();

    }
    public void goEndless()
    {
        
        player.transform.position = playerStart;
        TotalEnemy = Level.TotalEnemy;
        EnemyCounter_Text.text = TotalEnemy.ToString();
        RandomSpawn(Level);
    }
    private IEnumerator WaitForAnimation()
    {
        PlayerController.Instance.OneAnimation = false;
        PlayerController.Instance.isAlive = false;
        PlayerController.Instance.GetComponentInChildren<Animation>().Play("Dead");
        yield return new WaitForSeconds(1);
       
        if (!isShielded)
        {
            if (UnityEvents.Instance.isLevel)
            {
                restart.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                HighScore_Text.text = "Highest Kill Score\n" + PlayerPrefs.GetInt("BestScore");
                Time.timeScale = 0;
                Restart();
            }

        }


    }
    public void Restart() // oyuncu ölmesi halinde varolan leveli tekrar baþlatýr;
    {
        PlayerController.Instance.OneAnimation = true;
        PlayerController.Instance.isAlive = true;
        PlayerController.Instance.Ren.gameObject.transform.localScale = Vector3.one;

        if (UnityEvents.Instance.isLevel)
        {
            EnemyPool.DeactivePool();
            EnemyCounter_Text.text = Level.TotalEnemy.ToString();
            TotalEnemy = Level.TotalEnemy;
            player.transform.position = playerStart;
            PlayerController.Instance.enemies.Clear();
            UnityEvents.Instance.onRestart.Invoke();
            UnityEvents.Instance.isStart = true;
            Time.timeScale = 1.0f;
        }
        else
        {
            EnemyPool.DeactivePool();
            EnemyCounter_Text.text = Level.TotalEnemy.ToString();
            TotalEnemy = Level.TotalEnemy;
            player.transform.position = playerStart;
            if(TotalKilled > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", TotalKilled);
            }
            TotalKilled = 0;
            UnityEvents.Instance.onDead.Invoke();
            PlayerController.Instance.enemies.Clear();
            KilledEnemyCounter_Text.text = "0";
            Time.timeScale = 1.0f;

        }
        
    }
    public void Dead() //restart butonu aktive olur.
    {
        PlayerController.Instance.isAlive = false;
        StartCoroutine(WaitForAnimation());
        

    }
    private void SetBigRanges()
    {
        MinB = new Vector3(-48.25f, 0.8f, 22); //Random value.
        MaxB = new Vector3(18.75f, 0.8f, 49.25f); //Another ramdon value, just for the example.
    }
    private void SetSimpleRanges()
    {
        MaxS = new Vector3(48.5f, 0.5f, -5); //Random value.
        MinS = new Vector3(22, 0.5f, -48.5f); //Another ramdon value, just for the example.
    }

    public void RandomSpawn(LevelType level)
    {
        while(EnemyPool.GetPoolSize(0)>=PoolCounter && level.BigEnemy_Number > ActiveEnemyCounter) {
            PoolCounter++;
            ActiveEnemyCounter++;
            _xAxis = Random.Range(MinB.x, MaxB.x);
            _zAxis = Random.Range(MinB.z, MaxB.z);
            NavMeshHit hit;
            NavMesh.SamplePosition(new Vector3(_xAxis, 0.75f, _zAxis), out hit,5,NavMesh.AllAreas);
            Vector3 sampledSpawnPosition = hit.position;
            _randomPosition = sampledSpawnPosition;
            var Enemy = EnemyPool.GetPoolObject(0);
            Enemy.SetActive(false);
            Enemy.transform.position = _randomPosition;
            Enemy.SetActive(true);
           }
        PoolCounter = 0;
        ActiveEnemyCounter = 0;
        while(EnemyPool.GetPoolSize(1)>=PoolCounter && level.SimpleEnemy_Number > ActiveEnemyCounter) {
            PoolCounter++;
            ActiveEnemyCounter++;
            _xAxis = Random.Range(MinS.x, MaxS.x);
            _zAxis = Random.Range(MinS.z, MaxS.z);
            _randomPosition = new Vector3(_xAxis, 0.75f, _zAxis);
            var Enemy = EnemyPool.GetPoolObject(1);
            Enemy.SetActive(false);
            Enemy.transform.position = _randomPosition;
            Enemy.SetActive(true);
        }
        PoolCounter = 0;
        ActiveEnemyCounter = 0;
        while (EnemyPool.GetPoolSize(4) >= PoolCounter && level.GunnerEnemy_Number > ActiveEnemyCounter)
        {
            PoolCounter++;
            ActiveEnemyCounter++;
            _xAxis = Random.Range(MinS.x, MaxS.x);
            _zAxis = Random.Range(MinS.z, MaxS.z);
            _randomPosition = new Vector3(_xAxis, 0.75f, _zAxis);
            var Enemy = EnemyPool.GetPoolObject(4);
            Enemy.SetActive(false);
            Enemy.transform.position = _randomPosition;
            Enemy.SetActive(true);
        }
        PoolCounter = 0;
        ActiveEnemyCounter = 0;

    }
    public void EnemyDied()
    {
        TotalEnemy -= 1;
        EnemyCounter_Text.text = TotalEnemy.ToString();
        TotalKilled++;
        KilledEnemyCounter_Text.text = TotalKilled.ToString();
    }


    public void ResetLevelData(){
        EnemyPool.DeactivePool();
        TotalKilled = 0;
        KilledEnemyCounter_Text.text = "0";
        Time.timeScale = 1.0f;
    }

}


