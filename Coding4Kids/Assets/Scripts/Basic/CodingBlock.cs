using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CodingBlock: MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public Image ima;
    public bool isFinish = false;
    [HideInInspector] public int index;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parent;
    RectTransform rect;
    public virtual void Active()
    {
        
    }
    public virtual void Retract()
    {

    }
    private void Start()
    {
        parent = transform.parent;
        index = transform.parent.GetSiblingIndex() - 1;
    }

    public CodingBlock[] GetChild()
    {
        CodingBlock[] temp=new CodingBlock[transform.childCount];
        for(int i =0; i<transform.childCount;i++)
        {
            temp[i] = transform.GetChild(i).gameObject.GetComponent<CodingBlock>();
        }
        return temp;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = parent;
        transform.SetParent(transform.parent.root);
        ima.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var cam = Camera.main;
        Vector3 point = new Vector3();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.

        point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        transform.position = point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.SetSiblingIndex(index);
        ima.raycastTarget = true;
    }
}
