using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableBlock : BoardBlock
{
    [Header("Graphic Elements")]
    public GameObject active;
    public GameObject image;
    public GameObject blur;

    //BlockData originalData = null;
    bool isBlurred = true;

    float currentTime;
    bool isDragging = false;

    GameObject temp = null;

    private void Update()
    {
        if (!isDragging)
        {
            UpdateBlock();
        }
    }

    public void OnMouseEnter()
    {
        DragManager.Instance.dragInto = DragManager.DragInto.Block;
        DragManager.Instance.targetIndex = index;
    }

    public void OnMouseExit()
    {
        DragManager.Instance.dragInto = DragManager.DragInto.None;
        DragManager.Instance.targetIndex = -1;
    }

    public void SetBlur(bool value)
    {
        blur.SetActive(value);
        isBlurred = value;
    }

    public void OnMouseDown()
    {
        currentTime = Time.time;
    }

    public void OnMouseUp()
    {
        float interval = Time.time - currentTime;

        if(interval < 0.1f)
        {
            BlockBarItemList.Instance.Add(blockData);
            SetData(null);
            return;
        } 
        else if (isDragging)
        {
            isDragging = false;
            SetBlur(false);
            DragManager.Instance.HandleDrop();
        }
    }

    public void OnMouseDrag()
    {
        float interval = Time.time - currentTime;

        if(!isDragging && interval > 0.1f && blockData != null)
        {
            SetBlur(true);
            isDragging = true;
            temp = new GameObject();
            GameObject tempActive = Instantiate(active, temp.transform);
            GameObject tempImage = Instantiate(image, temp.transform);
            tempActive.GetComponent<SpriteRenderer>().sortingOrder += 10;
            tempImage.GetComponent<SpriteRenderer>().sortingOrder += 10;
            DragManager.Instance.StartDragging(temp, index, blockData, DragManager.Source.Block);
            DragManager.Instance.OnDrag();
            Destroy(temp);
        }
        else
        {
            DragManager.Instance.OnDrag();
        }
    }

    public override void UpdateBlock()
    {
        if (blockData != null)
        {
            SetBlur(false);
            image.GetComponent<SpriteRenderer>().sprite = blockData.thumbnail;
        }
        else
        {
            SetBlur(true);
            image.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    public override void SetData(BlockData data)
    {
        blockData = data;
    }    
}
