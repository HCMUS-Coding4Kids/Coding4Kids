using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public bool isDragging;
    public BlockData draggingData;

    public enum Source
    {
        None, Block, BlockBar
    }

    public enum DragInto
    {
        None, Block, Slot, Function, LoopCount
    }
    public Board targetBoard;
    public Source source;
    public DragInto dragInto;

    public int sourceIndex;
    public int targetIndex;
    GameObject draggedObject = null;

    private void Start()
    {
        targetBoard = GameManager.Instance.targetBoard;
    }

    public void HandleDrop()
    {
        Destroy(draggedObject);
        isDragging = false;
        if (source == Source.BlockBar)
        {
            if (dragInto == DragInto.Function || dragInto == DragInto.Slot)
            {
                SideBarManager.Instance.Add(draggingData);
            }
            if (dragInto == DragInto.Block)
            {
                targetBoard.SetData(targetIndex, draggingData);
            }
        } 
        else if (source == Source.Block)
        {
            targetBoard.Swap(sourceIndex, targetIndex);
        }
        draggingData = null;
    }

    public void StartDragging(GameObject gameObject, BlockData draggingData, Source source)
    {
        isDragging = true;
        draggedObject = Instantiate(gameObject, gameObject.transform.parent.transform.parent.transform.parent);
        draggedObject.transform.AddComponent<CanvasRenderer>();
        this.source = source;
        Canvas canvas = draggedObject.transform.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100;
        this.draggingData = draggingData;
    }

    public void StartDragging(GameObject gameObject, int sourceIndex, BlockData draggingData, Source source)
    {
        isDragging = true;
        draggedObject = Instantiate(gameObject);
        this.source = source;
        this.sourceIndex = sourceIndex;
        this.draggingData = draggingData;
    }

    public void OnDrag()
    {
        if (draggedObject != null)
        {
            draggedObject.SetActive(true);
            if (source == Source.BlockBar)
            {
                draggedObject.transform.position = Input.mousePosition;
            } 
            else
            {
                draggedObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
            }
        }
    }
}
