using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlockBar : MonoBehaviour
{
    public static UIBlockBar Instance { get; private set; }

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

    public GameObject blockBar;
    public GameObject blockPrefab;
    public List<UIBlock> blocks = new List<UIBlock>();

    private void Start()
    {
        int index = 0;
        foreach (BlockBarItem item in BlockBarItemList.Instance.items)
        {
            GameObject newBlock = Instantiate(blockPrefab);
            newBlock.transform.SetParent(blockBar.transform);
            newBlock.transform.localScale = Vector3.one;
            UIBlock uiBlock = newBlock.GetComponent<UIBlock>();
            uiBlock.Init(item.blockData, index);
            index++;
            blocks.Add(uiBlock);
        }
        UpdateBar();
    }
    public void UpdateBar()
    {
        for(int i = 0; i < blocks.Count; i++)
        {
            blocks[i].UpdateBlock(BlockBarItemList.Instance.items[i].count);
        }
    }
}
