using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenTouch : MonoSingleton<ScreenTouch>, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] public Image pivotPOP;
    private Vector2 touchPosition;
    public Vector2 Direction { get; private set; } // readonly yapmak için.
   

    public void OnPointerDown(PointerEventData eventData) //Basılan noktanın bilgileri alınır
    {
        touchPosition = eventData.position;
        pivotPOP.enabled = true;
        pivotPOP.transform.position = touchPosition;
    }

    public void OnPointerUp(PointerEventData eventData) //Kaldırınca bilgiler sıfırlanır
    {
        Direction = Vector2.zero;
        pivotPOP.enabled = false;
    }
    public void OnDrag(PointerEventData eventData) //sürükleme halinde karaktere yön bilgisi gönderilir
    {
        var delta = eventData.position - touchPosition;
        Direction = delta.normalized;
    }
}