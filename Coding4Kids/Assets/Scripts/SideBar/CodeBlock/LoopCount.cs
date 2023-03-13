using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoopCount : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        DragManager.Instance.dragInto = DragManager.DragInto.LoopCount;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragManager.Instance.dragInto = DragManager.DragInto.None;
    }
}
