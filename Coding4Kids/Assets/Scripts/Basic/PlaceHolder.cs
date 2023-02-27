using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceHolder : MonoBehaviour, IDropHandler
{
    public bool isActive;
    public void OnDrop(PointerEventData eventData)
    {
        if (isActive)
        {
            if (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                CodingBlock childBlock = child.GetComponent<CodingBlock>();
                childBlock.parentAfterDrag = childBlock.GetComponent<CodingBlock>().parent;
                child.SetSiblingIndex(childBlock.index - 1);
            }
            GameObject dropped = eventData.pointerDrag;
            CodingBlock block = dropped.GetComponent<CodingBlock>();
            block.parentAfterDrag = transform;
        }
    }

}
