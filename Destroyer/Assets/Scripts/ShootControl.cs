using UnityEngine;

public class ShootControl: MonoBehaviour
{
    [SerializeField] private BulletControl bulletcontroll;
    [SerializeField] private ObjectPool objectPool;
    public float Delay => delay;
    [SerializeField] private float delay;

    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }
    public void Shoot(Vector3 direction,Vector3 position) // kurşunu ateşleyeceği yön ve doğrultu.
    {
        var bullet = objectPool.GetPoolObject(2) ;
        bullet.transform.position = position;
        var toFire = bullet.GetComponent<BulletControl>();
        toFire.Fire(direction,bullet);
    }
    public void EnemyShoot(Vector3 direction, Vector3 position) // kurşunu ateşleyeceği yön ve doğrultu.
    {
        var bullet = objectPool.GetPoolObject(3);
        bullet.transform.position = position;
        var toFire = bullet.GetComponent<EnemyBulletControll>();
        toFire.Fire(direction, bullet);
    }
}
