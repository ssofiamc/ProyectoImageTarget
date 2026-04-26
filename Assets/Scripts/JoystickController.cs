using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [System.Serializable]
    public class JoystickEvent : UnityEvent<Vector2> {} //Se transforma de movimiento de 2 direcciones a un ovimiento 3d

    public RectTransform joystickContainer;
    public RectTransform joystickHandle;

    public float joystickRange = 75f;
    public JoystickEvent onJoystickMove;

    private Vector2 inputDirection = Vector2.zero;
    private bool isDragging = false;

    void Start()
    {
        if (joystickHandle != null)
            joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickContainer, eventData.position, eventData.pressEventCamera, out Vector2 localPoint
        );

        Vector2 clamped = Vector2.ClampMagnitude(localPoint, joystickRange);
        inputDirection = clamped / joystickRange;
        joystickHandle.anchoredPosition = clamped;

        onJoystickMove?.Invoke(inputDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        onJoystickMove?.Invoke(Vector2.zero);
        isDragging = false;
    }

#if UNITY_EDITOR //Cuando se prueba en Unity
    void Update()
    {
        if (!Application.isEditor || isDragging) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(joystickContainer, mousePos))
            {
                isDragging = true;
                PointerEventData data = new PointerEventData(EventSystem.current) { position = mousePos };
                OnPointerDown(data);
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            PointerEventData data = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            OnDrag(data);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            PointerEventData data = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            OnPointerUp(data);
            isDragging = false;
        }
    }
#endif
}
