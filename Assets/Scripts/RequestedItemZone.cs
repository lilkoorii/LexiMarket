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
        // Проверяем, есть ли у объекта компонент CheckableItem
        CheckableItem checkableItem = other.GetComponent<CheckableItem>();
        
        if (checkableItem != null)
        {
            // Проверяем, совпадает ли предмет с запрошенным
            if (checkableItem.CheckMatch())
            {
                Debug.Log("Correct item placed!");

                itemRequester.CreateFloatingText("Правильный предмет!");
                // Здесь можно добавить логику для правильного предмета
                // Например, начислить очки или показать эффект
            }
            else
            {
                Debug.Log("Wrong item placed!");
                itemRequester.CreateFloatingText(checkableItem.item.itemName);
                itemRequester.CreateFloatingText("Неправильный предмет!");
                // Здесь можно добавить логику для неправильного предмета
                // Например, показать сообщение об ошибке
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CheckableItem checkableItem = other.GetComponent<CheckableItem>();
        if (checkableItem != null)
        {
            // Здесь можно добавить логику при удалении предмета из зоны
            Debug.Log("Item removed from zone");
        }
    }
}
