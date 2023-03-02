using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

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

    [Header("Block Bars")]
    public GameObject bottomBar = null;
    public GameObject sideBar = null;

    [Header("Board Config")]
    public GameObject blockSlot = null;
    public BlockData defaultData = null;
    public int rows = 5;
    public int cols = 5;
    public float spacing = 1f;
    float tileSize = 1f;

    float centerX = 0f;
    float centerY = 0f;

    public static BlockData draggingData = null;

    public int sourceIndex = -1;
    public int targetIndex = -1;

    [Header("Slots")]
    public List<BoardSlot> slots = new List<BoardSlot>();

    private void Start()
    {
        GetCenterPoint();
        GenerateGrid();
        ResetSwap();
    }

    private void ResetSwap()
    {
        sourceIndex = -1;
        targetIndex = -1;
    }

    private void GetCenterPoint()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2f; //10
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width; //17.(7)8

        float bottomBarHeight = 0;
        float sideBarWidth = 0;

        if (bottomBar != null)
        {
            RectTransform rectTransform = bottomBar.GetComponent<RectTransform>();
            bottomBarHeight = (rectTransform.sizeDelta.y) / Screen.height * worldScreenHeight;
        }

        if (sideBar != null)
        {
            RectTransform rectTransform = sideBar.GetComponent<RectTransform>();
            sideBarWidth = (rectTransform.sizeDelta.x) / Screen.width * worldScreenWidth;
        }

        float top = worldScreenHeight / 2;
        float bottom = worldScreenHeight / 2 * -1 + bottomBarHeight;
        float left = worldScreenWidth / 2 * -1;
        float right = worldScreenWidth / 2 - sideBarWidth;

        centerX = (left + right) / 2;
        centerY = (top + bottom) / 2;

        transform.position = new Vector3(centerX, centerY, 0);
    }

    private void GenerateGrid()
    {
        float offsetX = (float)(cols - 1) / 2 * (tileSize + spacing);
        float offsetY = (float)(rows - 1) / 2 * (tileSize + spacing);

        int index = 0;

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                GameObject slot = Instantiate(blockSlot, transform);

                float posX = j * -1 * (tileSize + spacing) + offsetX + transform.position.x;
                float posY = i * (tileSize + spacing) - offsetY + transform.position.y;

                slot.transform.position = new Vector3(posX, posY, 0);
                slot.GetComponent<BoardSlot>().SetData(defaultData);
                slot.GetComponent<BoardSlot>().Toggle(true);
                slot.GetComponent<BoardSlot>().SetIndex(index);
                index++;
                slots.Add(slot.GetComponent<BoardSlot>());
            }
        }
    }

    private void RefreshBoard()
    {
        
    }

    public void StartDragging(int source)
    {
        GameManager.isDragging = true;
        sourceIndex = source;
        draggingData = slots[sourceIndex].blockData;
    }

    public void SetTarget(int target)
    {
        targetIndex = target;
    }  
    
    public bool Swap()
    {
        GameManager.isDragging = false;
        if (sourceIndex != -1 && targetIndex != -1 && (sourceIndex != targetIndex))
        {
            BlockData temp = slots[sourceIndex].blockData;
            slots[sourceIndex].SetData(slots[targetIndex].blockData);
            slots[targetIndex].SetData(temp);
            //slots[targetIndex].SetBlur(false);
            targetIndex = -1;
            sourceIndex = -1;
            return true;
        }
        return false;
    }

    public bool Swap(BlockData data)
    {
        GameManager.isDragging = false;

        if (targetIndex == -1)
        {
            return false;
        }

        if (slots[targetIndex].blockData != null)
        {
            BlockBarItemList.Instance.Add(slots[targetIndex].blockData);
        }

        slots[targetIndex].SetData(data);
        //slots[targetIndex].SetBlur(false);
        targetIndex = -1;
        return true;
    }
}
