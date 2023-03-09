using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCodeBlock : MonoBehaviour
{
    public List<PlaceHolder> holder;
    public List<CodingBlock> list;
    public static RunCodeBlock runCodeBlock;
    // Start is called before the first frame update
    void Start()
    {
        runCodeBlock = this;
        for(int i =0; i<holder.Count;i++)
        {
            holder[i].isActive = true;
        }    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Active());
        }
    }

    public void StartButton()
    {
        for (int i = 0; i < holder.Count; i++)
        {
            list.Add(holder[i].GetChild());
            list[i].Retract();
        }
        StartCoroutine(Active());
    }

    IEnumerator Active()
    {
        Bunny.bunny.aniBunny.SetBool("walk", true);
        
        for (int i =0; i<list.Count;i++)
        {
            list[i].Active();
            list.Remove(list[i]);
            i--;
            if (!Bunny.bunny.CheckStep())
            {
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
