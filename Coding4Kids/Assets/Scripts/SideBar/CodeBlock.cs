using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlock : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerExitHandler
{
    public enum Parent
    {
        None, Function
    }
    public BlockData blockData;
    public Parent parent = Parent.None;

    [Header("UI Elements")]
    public Image image;
    public Image highlight;
    public Image background;

    float currentTime = 0f;
    Vector3 originalPosition;
    Canvas canvas = null;

    private void Update()
    {
        if(blockData != null)
        {
            image.sprite = blockData.thumbnail;
        }
        else
        {
            image.sprite = null;
        }
    }

    private void Start()
    {
        ToggleHighlight(false);
    }
    
    public void ToggleHighlight(bool value)
    {
        highlight.gameObject.SetActive(value);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        SideBarManager.targetIndex = transform.GetSiblingIndex();
        ToggleHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SideBarManager.targetIndex = -1;
        ToggleHighlight(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float interval = Time.time - currentTime;
        if (interval < 0.1f)
        {
            BlockBarItemList.Instance.Add(blockData);
            Destroy(gameObject);
        }
        else
        {
            transform.SetSiblingIndex(SideBarManager.targetIndex);
            SideBarManager.targetIndex = -1;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        float posY = Input.mousePosition.y;
        float topY = 0f;
        float bottomY = 0f;
        if (parent == Parent.Function)
        {
            topY = FunctionSlotManager.Instance.topY.position.y;
            bottomY = FunctionSlotManager.Instance.bottomY.position.y;
        } 
        else
        {
            topY = SlotManager.Instance.topY.position.y;
            bottomY = SlotManager.Instance.bottomY.position.y;
        }
        if(posY > topY)
        {
            posY = topY;
            SideBarManager.targetIndex = 0;
        } else if (posY < bottomY)
        {
            posY = bottomY;
            SideBarManager.targetIndex = transform.parent.childCount - 1;
        }
        transform.position = new Vector3(transform.position.x, posY, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SideBarManager.isSwapping = true;
        SideBarManager.targetIndex = -1;
        originalPosition = transform.position;
        canvas = gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SideBarManager.isSwapping = false;
        transform.position = originalPosition;
        Destroy(GetComponent<Canvas>());
        canvas = null;
    }
}
