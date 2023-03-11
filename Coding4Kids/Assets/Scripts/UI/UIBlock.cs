using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBlock : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int index = -1;
    public BlockData blockData;

    [Header("UI Elements")]
    public Image image;
    public Image background;
    public GameObject available;
    public GameObject unavailable;
    public TextMeshProUGUI countText;

    bool isAvailable = false;

    //GameObject tempBlock = null;

    public void UpdateBlock(int count)
    {
        countText.text = count.ToString();
        if (count <= 0)
        {
            SetAvailable(false);
            isAvailable = false;
        }
        else
        {
            SetAvailable(true);
            isAvailable = true;
        }
    }

    public void Init(BlockData blockData, int index)
    {
        this.index = index;
        this.blockData = blockData;
        image.sprite = blockData.thumbnail;
        background.color = blockData.backgroundColor;
        if(blockData.type == BlockData.Type.Color)
        {
            ColorBlockData colorBlockData = (ColorBlockData)blockData;
            image.color = colorBlockData.colorPalete;
        }
    }

    private void SetAvailable(bool available)
    {
        unavailable.SetActive(!available);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isAvailable)
        {
            eventData.pointerDrag = null;
            return;
        }
        DragManager.Instance.StartDragging(available, blockData, DragManager.Source.BlockBar);
        DragManager.Instance.OnDrag();
        BlockBarItemList.Instance.Remove(blockData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.HandleDrop();
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragManager.Instance.OnDrag();
    }
}
