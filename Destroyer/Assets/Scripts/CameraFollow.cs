using UnityEngine;
public class CameraFollow : MonoSingleton<CameraFollow>
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    
    void FixedUpdate() //kamera Lerp methodunu kullanarak daha hafif bir hareket saðlar ve belli bir yüksekliðe sabitlenir, inspectordan girdi alabilir.
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }

}



