using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBarManager : MonoBehaviour
{
    public static BlockBarManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
}
