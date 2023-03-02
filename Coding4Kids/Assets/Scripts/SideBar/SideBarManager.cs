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

    [Header("Side Bar Config")]
    public static bool isSwapping = false;
    public Transform blockHolder = null;
    public GameObject codeBlockPrefab = null;
    public static int targetIndex = -1;

    bool enterSlot = false;

    public void SetEnterSlot(bool enterSlot)
    {
        this.enterSlot = enterSlot;
    }

    public void Add(BlockData data)
    {
        if(enterSlot)
        {
            GameObject newCodeBlock = Instantiate(codeBlockPrefab, blockHolder);
            newCodeBlock.GetComponent<CodeBlock>().blockData = data;
            if(targetIndex != -1)
            {
                newCodeBlock.transform.SetSiblingIndex(targetIndex);
            }
        }
    }

    
}
