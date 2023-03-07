using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : CodingBlock
{
    public Bunny square;
    public int dir;
    //private void Start()
    //{
    //    Active();
    //}
    public override void Active()
    {
        transform.parent = transform.root;
        transform.SetSiblingIndex(1);
        if (dir == 1)
        {
            square.transform.parent = square.list[square.parentId+1].transform;
            square.parentId += 1;
        }
        if(dir==-1)
        {
            square.transform.parent = square.list[square.parentId - 1].transform;
            square.parentId -= 1;
        }
        if (dir == 2)
        {
            square.transform.parent = square.list[square.parentId -5].transform;
            square.parentId -=5;
        }
        if (dir == -2)
        {
            square.transform.parent = square.list[square.parentId + 5].transform;
            square.parentId += 5;
        }
        if (dir == 3)
        {
            square.transform.parent = square.list[square.parentId + 1].transform;
            square.parentId += 1;
            square.transform.parent = square.list[square.parentId - 5].transform;
            square.parentId -= 5;
        }
        if (dir == -3)
        {
            square.transform.parent = square.list[square.parentId + 1].transform;
            square.parentId += 1;
            square.transform.parent = square.list[square.parentId + 5].transform;
            square.parentId += 5;
        }
        if (dir == 4)
        {
            square.transform.parent = square.list[square.parentId - 1].transform;
            square.parentId -= 1;
            square.transform.parent = square.list[square.parentId - 5].transform;
            square.parentId -= 5;
        }
        if (dir == -4)
        {
            square.transform.parent = square.list[square.parentId - 1].transform;
            square.parentId -= 1;
            square.transform.parent = square.list[square.parentId + 5].transform;
            square.parentId += 5;
        }
        if (dir == 5)
        {
            square.transform.parent = square.list[square.parentId - 5].transform;
            square.parentId -= 5;
            square.transform.parent = square.list[square.parentId + 1].transform;
            
            square.parentId += 1;
        }
        if (dir == -5)
        {
            square.transform.parent = square.list[square.parentId - 5].transform;
            square.parentId -= 5;
            square.transform.parent = square.list[square.parentId - 1].transform;
            
            square.parentId -= 1;
        }
        if (dir == 6)
        {
            square.transform.parent = square.list[square.parentId + 5].transform;
            square.parentId += 5;
            square.transform.parent = square.list[square.parentId + 1].transform;

            square.parentId += 1;
        }
        if (dir == -6)
        {
            square.transform.parent = square.list[square.parentId + 5].transform;
            square.parentId += 5;
            square.transform.parent = square.list[square.parentId - 1].transform;

            square.parentId -= 1;
        }
        isFinish = true;
        
    }
}

