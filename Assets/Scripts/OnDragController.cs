using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnDragController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 defaultPos;
    private Vector3 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = gameObject.transform.position;

        gameObject.GetComponent<Image>().raycastTarget = false;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint
            (new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y,
            Camera.main.WorldToScreenPoint(gameObject.transform.position).z);

        Vector3 currentPos = Camera.main.ScreenToWorldPoint(screenPoint) + offset;

        gameObject.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = defaultPos;
        gameObject.GetComponent<Image>().raycastTarget = true;
    }
}