using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCodeBlock : MonoBehaviour
{
    public List<PlaceHolder> holder;
    // Start is called before the first frame update
    void Start()
    {
        foreach (PlaceHolder i in holder)
        {
            i.isActive = true;
        }    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Active());
        }
    }

    IEnumerator Active()
    {
        Bunny.bunny.aniBunny.SetBool("walk", true);
        for(int i=0; i<holder.Count;i++)
        {
            holder[i].Active();
            if (!Bunny.bunny.CheckStep())
            {
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
}
