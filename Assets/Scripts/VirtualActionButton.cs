using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class VirtualActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Evento al presionar")]
    public UnityEvent onButtonDown;

    [Header("Evento al soltar")]
    public UnityEvent onButtonUp;

    private bool isPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        onButtonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        onButtonUp?.Invoke();
    }

    public bool IsPressed()
    {
        return isPressed;
    }
}
