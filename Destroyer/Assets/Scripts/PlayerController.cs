using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : myCharacterController
{
    [SerializeField] private ScreenTouch input;
    [SerializeField] private ShootControl shootController;
    public Transform tipofGun;
    Rigidbody rb;
    private Transform Idle;
   


    private void Start()
    {
        input = ScreenTouch.Instance;
        LevelManager.Instance.playerStart = transform.position;
        CameraFollow.Instance.target = transform;
        LevelManager.Instance.player = transform;
        Ren = GetComponentInChildren<Renderer>();
        isDoubleBullet = false;
        BulletDelay = 0.27f;
        rb =GetComponent<Rigidbody>();
    
    }
    private void FixedUpdate()
    {
       
        var direction = new Vector3(-input.Direction.x, 0, -input.Direction.y);
        Move(direction);
        if (enemies.Count == 0 && direction != Vector3.zero)
        {
            GetComponentInChildren<Animator>().Play("playerIdle"); // Koþarken karakter koþuyor hissiyatý vermesi için animasyon oynatýlýr
            Look(direction);    
        }
        else
        {
            GetComponentInChildren<Animator>().Play("playerRun"); // Karakterin duraðan halindeki hareketi
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        
        
    }
    private void Update()
    {
        if (enemies.Count > 0)
        {
            var enemy = enemies.Peek(); //lookAt() method but more smooth.
            if (enemy.gameObject.activeInHierarchy)
            {
                var directionE = enemy.transform.position - transform.position;
                directionE.y = 0;
                var rotation = Quaternion.LookRotation(directionE);
                transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rotation, 30 * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision) // düþman karaktere deðmesi halinde ölür.
    {
        if (collision.gameObject.GetComponent<EnemyBase>() != null)
        {
            if(OneAnimation)
                LevelManager.Instance.Dead();



        }

    }
    
    private void OnTriggerStay(Collider enemy) // düþmanlar alandaykende vurur
    {

        if (enemy.GetComponent<EnemyBase>() != null)
        {
            if (!enemies.Contains(enemy.transform) && enemy.gameObject.activeSelf) enemies.Enqueue(enemy.transform);
            AutoShoot();
        }

    }
    private bool isShooting;
    private void AutoShoot() // otomatik olarak kuyruktaki düþmanlara kurþun ateþler.
    {
        IEnumerator Do()
        {
            while (enemies.Count > 0 && isAlive)
            {


                var enemy = enemies.Dequeue();
                if (enemy.gameObject.activeSelf)
                {
                    var direction = enemy.transform.position - transform.position;
                    direction.y = 0;
                    direction = direction.normalized;
                    if (isDoubleBullet)
                    {
                        var GunPos = tipofGun.position;
                        GunPos.x +=0.1f;
                        shootController.Shoot(direction, GunPos);
                        GunPos.x -= 0.2f;
                        shootController.Shoot(direction, GunPos);
                        GunPos.x += 0.1f;
                    }
                    else
                    {
                        shootController.Shoot(direction, tipofGun.position);

                    }
                    yield return new WaitForSeconds(BulletDelay);
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
