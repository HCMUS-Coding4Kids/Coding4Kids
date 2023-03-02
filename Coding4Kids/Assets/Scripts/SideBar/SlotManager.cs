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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.isDragging)
        {
            SideBarManager.Instance.SetEnterSlot(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.isDragging)
        {
            SideBarManager.Instance.SetEnterSlot(false);
        }
    }
}
