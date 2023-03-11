using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBlock : MonoBehaviour
{
    public int index;

    protected bool isActive = false;
    public BlockData blockData;

    private void Start()
    {
        Toggle(false);
    }

    public virtual void Toggle(bool value)
    {
        isActive = value;
    }

    public virtual void UpdateBlock()
    {

    }
    public virtual void SetData(BlockData data)
    {
        blockData = data;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
}
