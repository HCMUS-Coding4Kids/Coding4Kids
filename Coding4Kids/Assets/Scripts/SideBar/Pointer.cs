using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public int index;
    public Board board;

    private void Start()
    {
        index = 0;
    }

    private void Update()
    {
        if (index < 0 || index > board.slots.Count - 1)
        {
            index = 0;
        }
        transform.position = board.slots[index].transform.position;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public int GetIndex()
    {
        return index;
    }
}
