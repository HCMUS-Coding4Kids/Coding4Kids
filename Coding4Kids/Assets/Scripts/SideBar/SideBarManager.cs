using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarManager : MonoBehaviour
{
    public static SideBarManager Instance { get; private set; }

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

    public void Start()
    {
        function.SetActive(hasFunction);
    }

    [Header("Side Bar Config")]
    public static bool isSwapping = false;
    public Transform blockHolder = null;
    public GameObject codeBlockPrefab = null;
    public static int targetIndex = -1;

    [Header("Function Slot")]
    public bool hasFunction = false;
    public GameObject function = null;
    public Transform funcBlockHolder = null;


    bool enterFunctionSlot = false;
    bool enterSlot = false;


    public void SetEnterSlot(bool enterSlot)
    {
        this.enterSlot = enterSlot;
    }

    public void SetEnterFunction(bool enderFunction)
    {
        enterFunctionSlot = enderFunction;
    }

    public bool isEnterFunction()
    {
        return enterFunctionSlot;
    }

    public void Add(BlockData data)
    {
        Transform targetHolder = null;
        if (enterFunctionSlot)
        {
            targetHolder = funcBlockHolder;
            enterFunctionSlot = false;
        }
        else
        {
            targetHolder = blockHolder;
            enterSlot = false;
        }

        GameObject newCodeBlock = Instantiate(codeBlockPrefab, targetHolder);
        newCodeBlock.GetComponent<CodeBlock>().blockData = data;
        newCodeBlock.GetComponent<CodeBlock>().background.color = data.backgroundColor;
        if(data.type == BlockData.Type.Color)
        {
            ColorBlockData colorBlockData = (ColorBlockData) data;
            newCodeBlock.GetComponent<CodeBlock>().image.color = colorBlockData.colorPalete;
        }    
        if (targetIndex != -1)
        {
            newCodeBlock.transform.SetSiblingIndex(targetIndex);
        }
        newCodeBlock.GetComponent<CodeBlock>().parent = (targetHolder == funcBlockHolder) ? CodeBlock.Parent.Function : CodeBlock.Parent.None;
    }

    
}
