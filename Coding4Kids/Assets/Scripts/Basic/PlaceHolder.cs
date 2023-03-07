using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceHolder : MonoBehaviour, IDropHandler
{
    public bool isActive=true;
    public void OnDrop(PointerEventData eventData)
    {
        if (isActive)
        {
            GameObject dropped = eventData.pointerDrag;
            CodingBlock block = dropped.GetComponent<CodingBlock>();
            if (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                CodingBlock childBlock = child.GetComponent<CodingBlock>();
                childBlock.parentAfterDrag = block.parentAfterDrag;
                childBlock.transform.SetParent(block.parentAfterDrag);
                child.SetSiblingIndex(childBlock.index);
            }
            
            
            block.parentAfterDrag = transform;
            StartCoroutine(wait());
        }
    }
    public void Active()
    {
        CodingBlock child = transform.GetChild(0).GetComponent<CodingBlock>();
        child.Active();
    }
    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
    }

}
