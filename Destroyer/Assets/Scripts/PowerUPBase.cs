using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUPBase : MonoBehaviour
{
    public PowerUpType power;




  
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            if (power.Shield_.Equals(true))
            {
                PlayerController.Instance.ShieldPower();
                Debug.Log("Shielded");
            }
            if (power.Double_Bullet.Equals(true))
            {

                Debug.Log("DoubleBullet");
                PlayerController.Instance.Double_Bullet();
            }
            if (power.Attack_Speed.Equals(true))
            {
                Debug.Log("AttackSpeed");
                PlayerController.Instance.Attack_Speed();
            }
            gameObject.SetActive(false);
        }
        
        
    }

  


}
