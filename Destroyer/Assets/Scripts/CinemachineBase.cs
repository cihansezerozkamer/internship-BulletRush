using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineBase : MonoBehaviour
{
    public CinemachineVirtualCamera[]  cinemachineVirtuals;
    void Start()
    {
        foreach (var cam in cinemachineVirtuals)
        {
            cam.GetComponent<CinemachineVirtualCamera>();
        }
    }
    private void Update()
    {
        
        foreach (var cam in cinemachineVirtuals)
        {  
                if (PlayerController.Instance != null)
                {
                    cam.Follow = PlayerController.Instance.transform;
                    cam.LookAt = PlayerController.Instance.transform;
                } 
        }
    }


}
