using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DraggableBoard : Board
{
    public static new DraggableBoard Instance { get; private set; }

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

    public static BlockData draggingData = null;
    public int sourceIndex = -1;
    public int targetIndex = -1;

    private void Start()
    {
        ResetSwap();
    }

    private void ResetSwap()
    {
        sourceIndex = -1;
        targetIndex = -1;
    }
 
    private void RefreshBoard()
    {

    }

    public void StartDragging(int source)
    {
        GameManager.isDragging = true;
        sourceIndex = source;
        draggingData = slots[sourceIndex].blockData;
    }

    public void SetTarget(int target)
    {
        targetIndex = target;
    }

    public bool Swap()
    {
        GameManager.isDragging = false;
        if (sourceIndex != -1 && targetIndex != -1 && (sourceIndex != targetIndex))
        {
            BlockData temp = slots[sourceIndex].blockData;
            slots[sourceIndex].SetData(slots[targetIndex].blockData);
            slots[targetIndex].SetData(temp);
            //slots[targetIndex].SetBlur(false);
            targetIndex = -1;
            sourceIndex = -1;
            return true;
        }
        return false;
    }

    public bool Swap(BlockData data)
    {
        GameManager.isDragging = false;

        if (targetIndex == -1)
        {
            return false;
        }

        if (slots[targetIndex].blockData != null)
        {
            BlockBarItemList.Instance.Add(slots[targetIndex].blockData);
        }

        slots[targetIndex].SetData(data);
        //slots[targetIndex].SetBlur(false);
        targetIndex = -1;
        return true;
    }
}
