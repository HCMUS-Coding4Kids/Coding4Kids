using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FunctionSlotManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static FunctionSlotManager Instance { get; private set; }

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
        SideBarManager.Instance.SetEnterFunction(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         SideBarManager.Instance.SetEnterFunction(false);
    }
}
