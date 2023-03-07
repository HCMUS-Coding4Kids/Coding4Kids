using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public GameObject Board;
    public GameObject[] list;
    public int parentId;
    public Animator aniBunny;
    public static Bunny bunny;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
        bunny = this;
        aniBunny = GetComponent<Animator>();
    }
    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
        list = new GameObject[Board.transform.childCount];
        for (int i = 0; i < Board.transform.childCount; i++)
        {
            list[i] = Board.transform.GetChild(i).gameObject;
            if (list[i].transform == transform.parent) parentId = i;
        }
    }
    public bool CheckStep()
    {
        if(transform.parent.tag=="Finish")
        {
            aniBunny.SetBool("getCarrot", true);
            transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        return transform.parent.GetComponent<PlaceHolder>().isActive;
    }
}
