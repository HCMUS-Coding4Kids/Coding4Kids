using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBarItemList : MonoBehaviour
{
    public static BlockBarItemList Instance { get; private set; }

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

    public BlockBarItem[] items;


    public void Add(BlockData data)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (data == items[i].blockData)
            {
                items[i].count++;
                UIBlockBar.Instance.UpdateBar();
                return;
            }
        }
    }
}
