using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestedItemZone : MonoBehaviour
{
    void Start() {
        ItemRequester itemRequester = FindObjectOfType<ItemRequester>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemRequester itemRequester = FindObjectOfType<ItemRequester>();
        CheckableItem checkableItem = other.GetComponent<CheckableItem>();
        
        if (checkableItem != null)
        {
            Debug.Log(checkableItem.item);
            
            if (checkableItem.CheckMatch())
            {
                Debug.Log("Correct item placed!");
                itemRequester.CreateFloatingText("Правильный предмет!");
                
                Destroy(other.gameObject);
                StartCoroutine(RequestNewItemAfterDelay(itemRequester));
            }
            else
            {
                Debug.Log("Wrong item placed!");
                itemRequester.CreateFloatingText(checkableItem.item.itemNameForeign);
                itemRequester.CreateFloatingText("Неправильный предмет!");
            }
        }
    }

    private IEnumerator RequestNewItemAfterDelay(ItemRequester itemRequester)
    {
        yield return new WaitForSeconds(2f);
        itemRequester.RequestedItem = itemRequester.GetRandomItem();
        itemRequester.CreateFloatingText(itemRequester.RequestedItem.itemNameForeign);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckableItem checkableItem = other.GetComponent<CheckableItem>();
        if (checkableItem != null)
        {
            Debug.Log("Item removed from zone");
        }
    }
}