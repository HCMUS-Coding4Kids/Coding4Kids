using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    bool isActive = false;
    public BlockData blockData;
    public GameObject active;
    public GameObject image;
    private void Start()
    {
        Toggle(true);
    }

    public void OnMouseDown()
    {
        if(isActive)
        {
            Toggle(false);
            BlockBarItemList.Instance.Add(blockData);
        }
    }

    public void Toggle(bool value)
    {
        isActive = value;
        if(isActive)
        {
            active.SetActive(true);
            image.SetActive(true);
        }
        else
        {
            active.SetActive(false);
            image.SetActive(false);
        }
    }

    public void UpdateBlock()
    {
        image.GetComponent<SpriteRenderer>().sprite = blockData.thumbnail;
    }

    public void SetData(BlockData data)
    {
        blockData = data;
        UpdateBlock();
    }    
}
