using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

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

    [Header("Example Board")]
    public bool useExampleBoard = false;
    public GameObject exampleBoard;
    public float exampleScale = 0.5f;

    float centerX = 0f;
    float centerY = 0f;

    [Header("Slots")]
    public List<BoardBlock> slots = new List<BoardBlock>();
    public List<BoardBlock> exampleSlots = new List<BoardBlock>();

    private void Start()
    {
        GetCenterPoint();
        GenerateGrid();
    }

    protected void GetCenterPoint()
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

    protected void GenerateGrid()
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
                slot.GetComponent<BoardBlock>().SetData(defaultData);
                slot.GetComponent<BoardBlock>().Toggle(true);
                slot.GetComponent<BoardBlock>().SetIndex(index);
                index++;
                slots.Add(slot.GetComponent<BoardBlock>());
            }
        }
    }

    protected void GenerateExampleBoard()
    {
        if(!useExampleBoard)
        {
            return;
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i)!= exampleBoard.transform)
            {
                GameObject temp = Instantiate(transform.GetChild(i).gameObject, exampleBoard.transform);
                BoardBlock tempBlock = temp.GetComponent<BoardBlock>();
                if (tempBlock != null)
                {
                    exampleSlots.Add(tempBlock);
                }
            }
        }

        exampleBoard.transform.localScale = Vector3.one * exampleScale;
        float boardWidth = cols + (cols - 1) * spacing;
        float spacingBetweenBoards = exampleScale * 4 * spacing;
        exampleBoard.transform.localPosition = new Vector3((float)(-1 * (0.5 * boardWidth * (1 + exampleScale) + spacingBetweenBoards)), 0, 0);
        transform.localPosition += new Vector3((float)(0.5 * (boardWidth * (exampleScale) + spacingBetweenBoards)), 0, 0);
    }
}
