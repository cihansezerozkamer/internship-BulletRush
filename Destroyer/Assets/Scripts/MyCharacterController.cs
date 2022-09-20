using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCharacterController : MonoSingleton<myCharacterController>
{
    //[SerializeField] private ScreenTouch input;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform LeftSide;
    [SerializeField] private Transform RightSide;
    public Queue<Transform> enemies = new Queue<Transform>();
    public bool isDoubleBullet;
    public float BulletDelay;
    public GameObject MaterialObject;
    public Renderer Ren;
    public Material Shield;
    public Material DoubleBullet;
    public Material AttackSpeed;
    public Material PlayerMaterial;
    public bool OneAnimation = true;
    public bool isAlive= true;






    protected void Move(Vector3 direction) // hareket için inputtan gelen directionu baz alır
    {

        playerRigidbody.velocity = direction * moveSpeed * Time.deltaTime;

    }
    protected void Look(Vector3 direction) // gittiği yöne pürüssüz bir şekilde bakmasını sağlar
    {
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }
    public Transform LeftTrack()
    {
        return LeftSide;
    }
    public Transform RightTrack()
    {
        return RightSide;
    }
    public void ShieldPower()
    {
        StartCoroutine(Do());
        IEnumerator Do()
        {
            Ren.material = Shield;
            LevelManager.Instance.isShielded = true;
            yield return new WaitForSeconds(LevelManager.Instance.PowerUPSeconds);
            LevelManager.Instance.isShielded = false;
            Ren.material = PlayerMaterial;
        }
        


    }

    public void Double_Bullet()
    {
        StartCoroutine(Do());
        IEnumerator Do()
        {
            Ren.material = DoubleBullet;
            isDoubleBullet = true;
            yield return new WaitForSeconds(LevelManager.Instance.PowerUPSeconds);
            isDoubleBullet = false;
            Ren.material = PlayerMaterial;
        }

    }

    public void Attack_Speed()
    {
        StartCoroutine(Do());
        IEnumerator Do()
        {
            Ren.material = AttackSpeed;
            BulletDelay = 0.15f;
            yield return new WaitForSeconds(LevelManager.Instance.PowerUPSeconds);
            BulletDelay = 0.27f;
            Ren.material = PlayerMaterial;
        }

    }
}
