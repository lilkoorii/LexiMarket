using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckableItem : MonoBehaviour
{
    public Item item;
    private ItemRequester itemRequester;

    void Start()
    {
        itemRequester = FindObjectOfType<ItemRequester>();
        if (itemRequester == null)
        {
            Debug.LogError("ItemRequester not found in the scene!");
        }
    }

    public bool CheckMatch()
    {
        if (itemRequester == null || item == null)
            return false;
            
        return item == itemRequester.RequestedItem;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
