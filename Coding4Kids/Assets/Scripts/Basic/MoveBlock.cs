using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : CodingBlock
{
    public Bunny square;
    public int dir;

    public void InitMoveBlock(Bunny bunny, int direc, Transform pa, int i)
    {
        square = bunny;
        dir = direc;
        transform.SetParent(pa);
        index = i;
    }

    //private void Start()
    //{
    //    Active();
    //}
    public override void Active()
    {
        
        if (dir == 1)
        {
            square.transform.SetParent(square.list[square.parentId+1].transform);
            square.parentId += 1;
        }
        if(dir==-1)
        {
            square.transform.SetParent(square.list[square.parentId - 1].transform);
            square.parentId -= 1;
        }
        if (dir == 2)
        {
            square.transform.SetParent(square.list[square.parentId -5].transform);
            square.parentId -=5;
        }
        if (dir == -2)
        {
            square.transform.SetParent(square.list[square.parentId + 5].transform);
            square.parentId += 5;
        }
        if (dir == 3)
        {
            square.transform.SetParent(square.list[square.parentId + 1].transform);
            square.parentId += 1;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, 2,transform.parent,index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == -3)
        {
            square.transform.SetParent(square.list[square.parentId + 1].transform);
            square.parentId += 1;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, -2, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == 4)
        {
            square.transform.SetParent(square.list[square.parentId - 1].transform);
            square.parentId -= 1;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, 2, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == -4)
        {
            square.transform.SetParent(square.list[square.parentId - 1].transform);
            square.parentId -= 1;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, -2, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == 5)
        {
            square.transform.SetParent(square.list[square.parentId - 5].transform);
            square.parentId -= 5;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, 1, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == -5)
        {
            square.transform.SetParent(square.list[square.parentId - 5].transform);
            square.parentId -= 5;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square,-1, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == 6)
        {
            square.transform.SetParent(square.list[square.parentId + 5].transform);
            square.parentId += 5;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, 1, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1, temp.GetComponent<MoveBlock>());
        }
        if (dir == -6)
        {
            square.transform.SetParent(square.list[square.parentId + 5].transform);
            square.parentId += 5;

            GameObject temp = new GameObject();
            temp.AddComponent<MoveBlock>().InitMoveBlock(square, -1, transform.parent, index);
            RunCodeBlock.runCodeBlock.list.Insert(1,temp.GetComponent<MoveBlock>());
        }
        isFinish = true;
        
    }
    public override void Retract()
    {
        transform.SetParent(transform.root);
        transform.SetSiblingIndex(1);
    }
}

