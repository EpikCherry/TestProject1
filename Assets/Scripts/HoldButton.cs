using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public UnityEvent onPointEnter;
    public UnityEvent onPointExit;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onPointEnter.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onPointExit.Invoke();
        }
    }
}
