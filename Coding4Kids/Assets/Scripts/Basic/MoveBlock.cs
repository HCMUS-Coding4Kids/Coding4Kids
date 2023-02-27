using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : CodingBlock
{
    public GameObject square;
    public int dir;
    //private void Start()
    //{
    //    Active();
    //}
    public override void Active()
    {

        if(dir==1)
            square.transform.position += Vector3.right;
        if(dir==-1)
            square.transform.position += Vector3.left;
        if (dir == 2)
            square.transform.position += Vector3.up;
        if (dir == -2)
            square.transform.position += Vector3.down;
        isFinish = true;
    }
}
