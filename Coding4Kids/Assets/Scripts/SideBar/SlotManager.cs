using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static SlotManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public Transform topY = null;
    public Transform bottomY = null;
    public Transform middleY = null;

    private void Start()
    {
        if(SideBarManager.Instance.hasFunction)
        {
            bottomY = middleY;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragManager.Instance.dragInto = DragManager.DragInto.Slot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragManager.Instance.dragInto = DragManager.DragInto.None;
    }
}
