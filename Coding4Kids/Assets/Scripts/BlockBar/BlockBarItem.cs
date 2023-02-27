using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockBarItem
{
    public BlockData blockData;
    public int count;

    public BlockBarItem(BlockData blockData, int count)
    {
        this.blockData = blockData;
        this.count = count;
    }

    public BlockBarItem()
    {
        this.blockData = null;
        this.count = 0;
    }
}
