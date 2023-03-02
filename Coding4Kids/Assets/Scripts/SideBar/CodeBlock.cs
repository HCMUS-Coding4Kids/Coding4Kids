using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlock : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerExitHandler
{
    public BlockData blockData;

    [Header("UI Elements")]
    public Image image;
    public Image highlight;

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
        if(posY > SlotManager.Instance.topY.transform.position.y)
        {
            posY = SlotManager.Instance.topY.transform.position.y;
            SideBarManager.targetIndex = 0;
        } else if (posY < SlotManager.Instance.bottomY.transform.position.y)
        {
            posY = SlotManager.Instance.bottomY.transform.position.y;
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
