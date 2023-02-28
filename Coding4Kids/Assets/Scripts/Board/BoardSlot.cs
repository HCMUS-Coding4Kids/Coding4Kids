using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    bool isActive = false;
    public BlockData blockData = null;
    public GameObject active;
    public GameObject image;

    public int index;
    float currentTime;
    bool isDragging = false;

    GameObject temp = null;

    private void Start()
    {
        Toggle(false);
    }

    private void Update()
    {
        if (!isDragging)
        {
            UpdateBlock();
        }
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void OnMouseEnter()
    {
        if(BoardManager.isDragging)
        {
            BoardManager.Instance.SetTarget(index);
        }    
    }

    public void OnMouseExit()
    {
        if (BoardManager.isDragging)
        {
            BoardManager.Instance.SetTarget(-1);
        }
    }

    public void OnMouseDown()
    {
        Toggle(false);
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
            if(!BoardManager.Instance.Swap())
            {
                Toggle(true);
            }
            Destroy(temp);
        }
    }

    public void OnMouseDrag()
    {
        float interval = Time.time - currentTime;

        if(!isDragging && interval > 0.1f && isActive)
        {
            temp = new GameObject();
            GameObject tempActive = Instantiate(active, temp.transform);
            GameObject tempImage = Instantiate(image, temp.transform);
            tempActive.GetComponent<SpriteRenderer>().sortingOrder += 10;
            tempImage.GetComponent<SpriteRenderer>().sortingOrder += 10;
            Toggle(false);
            isDragging = true;
            BoardManager.Instance.StartDragging(index);
        }
        if (temp != null)
        {
            temp.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
        }
    }


    public void Toggle(bool value)
    {
        isActive = value;
        active.SetActive(value);
        image.SetActive(value);
    }

    public void UpdateBlock()
    {
        if (blockData != null)
        {
            Toggle(true);
            image.GetComponent<SpriteRenderer>().sprite = blockData.thumbnail;
        }
        else
        {
            Toggle(false);
        }
    }

    public void SetData(BlockData data)
    {
        blockData = data;
        UpdateBlock();
    }    
}
