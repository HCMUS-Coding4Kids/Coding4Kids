using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForBlock : CodingBlock
{
    public int turn;

    private void Start()
    {
        //Active();
    }
    public override void Active()
    {
        CodingBlock[] child = GetChild();
        StartCoroutine(Go(child));
    }
    IEnumerator Go(CodingBlock[] child)
    {
        for(int j = 0; j < turn; j++)
        {
            child[0].Active();
            for (int i = 1; i < child.Length; i++)
            {
                if (!child[i - 1].isFinish)
                    i = i - 1;
                else
                {
                    yield return new WaitForSeconds(0.5f);
                    child[i].Active();
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        isFinish = true;
    }
}
