using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    Animator animator;
    bool isMainCamera=true;

    void Start()
    {
        animator =  GetComponent<Animator>();
    }
    void Update()
    {
    }
    public void CameraChange()
    {
        Debug.Log("AAA");
        if (isMainCamera)
        {
            animator.Play("YanKamera");
            isMainCamera = false;
        }
        else
        {
            animator.Play("AnaKamera");
            isMainCamera = true;
        }
    }
}
