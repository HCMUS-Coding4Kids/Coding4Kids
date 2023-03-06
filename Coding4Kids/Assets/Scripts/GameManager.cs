using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isDragging = false;
    public static bool isRunning = false;

    public Board targetBoard = null;
    public float timeBetweenExecution = 0.5f;
    public Pointer pointer;
    public List<BlockData> LoadFunctionSlot()
    {
        List<BlockData> blocks = new List<BlockData>();
        Transform functionSlot = SideBarManager.Instance.funcBlockHolder.gameObject.transform;
        for(int i = 0; i < functionSlot.childCount; i++)
        {
            blocks.Add(functionSlot.GetChild(i).GetComponent<CodeBlock>().blockData);
        }
        return blocks;
    }

    public List<BlockData> LoadSlot()
    {
        List<BlockData> functionBlocks = LoadFunctionSlot();
        List<BlockData> blocks = new List<BlockData>();
        Transform slot = SideBarManager.Instance.blockHolder.gameObject.transform;
        for (int i = 0; i < slot.childCount; i++)
        {
            BlockData block = slot.GetChild(i).GetComponent<CodeBlock>().blockData;
            if(block.type == BlockData.Type.Function)
            {
                blocks.AddRange(functionBlocks);
            }
            else
            {
                blocks.Add(block);
            }
        }
        return blocks;
    }

    IEnumerator RunBoard()
    {
        List<BlockData> blocks = LoadSlot();
        for(int i = 0; i < blocks.Count; i++)
        {
            HandleBlock(blocks[i]);
            yield return new WaitForSeconds(timeBetweenExecution);
        }
        Debug.Log("Finished");
    }

    private void HandleBlock(BlockData blockData)
    {
        int index = pointer.GetIndex();
        Debug.Log(blockData);
        if (blockData.type == BlockData.Type.Directional)
        {
            int temp;
            DirectionalBlockData direction = blockData as DirectionalBlockData;
            if (direction.direction == DirectionalBlockData.Direction.Up)
            {
                temp = index + targetBoard.rows;
                if (temp < targetBoard.slots.Count)
                {
                    pointer.SetIndex(temp);
                }
            }
            else if (direction.direction == DirectionalBlockData.Direction.Down)
            {
                temp = index - targetBoard.rows;
                if (temp >= 0)
                {
                    pointer.SetIndex(temp);
                }
            }
            else if (direction.direction == DirectionalBlockData.Direction.Left)
            {
                if ((index + 1) % targetBoard.cols != 0)
                {
                    temp = index + 1;
                    pointer.SetIndex(temp);
                }
            }
            else
            {
                if (index % targetBoard.cols != 0)
                {
                    temp = index - 1;
                    pointer.SetIndex(temp);
                }
            }
        }
        else
        {
            targetBoard.slots[index].SetData(blockData);
        }
    }

    public void StartExecution()
    {
        StartCoroutine(RunBoard());
    }

    public void ClearBoard()
    {
        ResetFunction();
        ResetBoard();
    }

    public void ResetBoard()
    {
        Transform slot = SideBarManager.Instance.blockHolder.gameObject.transform;
        for (int i = 0; i < slot.childCount; i++)
        {
            Transform block = slot.GetChild(i);
            BlockData blockData = block.GetComponent<CodeBlock>().blockData;
            BlockBarItemList.Instance.Add(blockData);
            Destroy(block.gameObject);
        }
    }

    public void ResetFunction()
    {
        Transform functionSlot = SideBarManager.Instance.funcBlockHolder.gameObject.transform;
        for(int i = 0; i < functionSlot.childCount; i++)
        {
            Transform block = functionSlot.GetChild(i);
            BlockData blockData = block.GetComponent<CodeBlock>().blockData;
            BlockBarItemList.Instance.Add(blockData);
            Destroy(block.gameObject);
        }
    }
}

