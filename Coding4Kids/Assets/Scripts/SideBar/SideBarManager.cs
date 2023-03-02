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
        if (enterFunctionSlot) {
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, funcBlockHolder);
            newCodeBlock.GetComponent<CodeBlock>().blockData = data;
            if (targetIndex != -1)
            {
                newCodeBlock.transform.SetSiblingIndex(targetIndex);
            }
            enterFunctionSlot = false;
            newCodeBlock.GetComponent<CodeBlock>().parent = CodeBlock.Parent.Function;
            return;
        }
        if(enterSlot)
        {
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, blockHolder);
            newCodeBlock.GetComponent<CodeBlock>().blockData = data;
            if(targetIndex != -1)
            {
                newCodeBlock.transform.SetSiblingIndex(targetIndex);
            }
            enterSlot = false;
        }
    }

    
}
