using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProtocolCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private Camera _mainCam;

    private bool isDrag = false;
    private Vector3 _offset;
    private Rect _screenRect;
    private float _moveSpeedLimit = 50f;
    private GraphicRaycaster _canvasRaycaster;
    public Canvas _canvas;
    private Image _imageCompo;

    private void Awake()
    {
        _canvasRaycaster = _canvas.GetComponent<GraphicRaycaster>();
        _imageCompo = GetComponent<Image>();

        CalculateScreenRect();
    }
    private void Update()
    {
        //Debug.Log(isDrag);
        DragFollow();
        ClampPosition();
    }
    private void CalculateScreenRect()
    {
        Vector3 camPosition = _mainCam.transform.position;
        Vector2 topLeft = _mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 20));
        Vector2 bottomRight = _mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 20));

        Vector2 size = bottomRight - topLeft;
        _screenRect = new Rect(topLeft, size);
    }
    private void ClampPosition()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, _screenRect.xMin, _screenRect.xMax);
        position.y = Mathf.Clamp(position.y, _screenRect.yMax, _screenRect.yMin);
        transform.position = position;
    }
    private void DragFollow()
    {
        if (!isDrag) return;
        Vector3 targetPos = _mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        Debug.Log(targetPos);
        //Vector2 delta = (targetPos - (Vector2)transform.position);

        //Vector2 velocity = delta.normalized * Mathf.Min(_moveSpeedLimit, delta.magnitude / Time.deltaTime);

        transform.position = targetPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);

        _offset = mousePosition - (Vector2)transform.position;
        isDrag = true;
        _canvasRaycaster.enabled = false;
        _imageCompo.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //드래그중
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        _canvasRaycaster.enabled = true;
        _imageCompo.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnPointerUp");
    }
}
