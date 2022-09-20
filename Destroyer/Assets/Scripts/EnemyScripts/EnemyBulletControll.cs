using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControll : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 movement;
    private GameObject bullet;
    public void Fire(Vector3 direction, GameObject bullet) // kur�unun d��mana gitmesi i�in o y�n� ve h�z�n� movement de�i�kenine depolar
    {
        movement = direction * speed;
        if (bullet != null)
            this.bullet = bullet;
    }
    private void FixedUpdate() //kur�un o y�nde zamana ba�l� olarak hareket eder
    {
        if (bullet != null)
            bullet.transform.position += movement * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            LevelManager.Instance.Dead();
            gameObject.SetActive(false);
        }
    }

}


