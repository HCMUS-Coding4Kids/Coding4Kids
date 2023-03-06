using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (BlockBarManager.Instance.unlimitedUse)
            {
                item.count = 999;
                uiBlock.countText.transform.parent.gameObject.SetActive(false);
            }
            index++;
            blocks.Add(uiBlock);
        }
        GridLayoutGroup grid = blockBar.GetComponent<GridLayoutGroup>();
        RectTransform rect = blockBar.GetComponent<RectTransform>();
        if(grid.constraintCount == 2)
        {
            rect.localScale = Vector3.one / 1.5f;
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
