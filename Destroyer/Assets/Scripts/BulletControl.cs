using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 movement;
    private GameObject bullet;
    private Vector3 KnockBackDirection;
    private ParticleSystem CollisionEffect_;
    private MeshRenderer meshRenderer_;

    void Start()
    {
        CollisionEffect_ = GetComponent<ParticleSystem>();
        meshRenderer_ = GetComponent<MeshRenderer>();
    }
    public void Fire(Vector3 direction,GameObject bullet) // kurşunun düşmana gitmesi için o yönü ve hızını movement değişkenine depolar
    {
        if(meshRenderer_ != null)meshRenderer_.enabled = true;
        movement = direction * speed;
        if (bullet != null)
        this.bullet = bullet;
        KnockBackDirection = direction;
    }
    private void FixedUpdate() //kurşun o yönde zamana bağlı olarak hareket eder
    {
        if (bullet != null)
        bullet.transform.position += movement * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyBase>() != null )
        {
            EnemyBase enemybase = (EnemyBase) other.GetComponent<EnemyBase>();

            if (other.gameObject.activeSelf)
            {
                StartCoroutine(DoEffect(enemybase));
                KnockBack(other.GetComponent<Rigidbody>());
                
            }
           

          
        }

        IEnumerator DoEffect(EnemyBase enemybase)
        {
            
            CollisionEffect_.Play();
            movement = Vector3.zero;
            enemybase.TakeDamage(100);
            if (meshRenderer_ != null) meshRenderer_.enabled = false;
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
    private void KnockBack(Rigidbody EnemyRB)
    {
        EnemyRB.velocity = KnockBackDirection * LevelManager.Instance.KnockBackForce;
    }
    
}
