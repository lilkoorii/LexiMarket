using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRequester : MonoBehaviour
{
    [SerializeField] private Item[] allItems; // Массив всех доступных айтемов
    public Item RequestedItem;

    private void Start()
    {
        // Загружаем все Item ScriptableObjects из папки Resources
        allItems = Resources.LoadAll<Item>("Items");
        RequestedItem = GetRandomItem();
        Debug.Log(RequestedItem.itemName);
    }

    // Получить один случайный айтем
    public Item GetRandomItem()
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("No items found!");
            return null;
        }

        int randomIndex = Random.Range(0, allItems.Length);
        return allItems[randomIndex];
    }

    // Получить несколько случайных айтемов
    public Item[] GetRandomItems(int count)
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("No items found!");
            return null;
        }

        // Ограничиваем количество запрашиваемых айтем��в доступным количеством
        count = Mathf.Min(count, allItems.Length);
        
        // Создаем копию массива, чтобы не изменять оригинальный
        Item[] shuffledItems = (Item[])allItems.Clone();
        
        // Перемешиваем массив
        for (int i = shuffledItems.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Item temp = shuffledItems[i];
            shuffledItems[i] = shuffledItems[randomIndex];
            shuffledItems[randomIndex] = temp;
        }

        // Возвращаем первые count элементов
        Item[] result = new Item[count];
        System.Array.Copy(shuffledItems, result, count);
        return result;
    }

    // Получить конкретный айтем по имени
    public Item GetItemByName(string itemName)
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("No items found!");
            return null;
        }

        foreach (Item item in allItems)
        {
            if (item.itemName.ToLower() == itemName.ToLower())
            {
                return item;
            }
        }

        Debug.LogWarning($"Item with name '{itemName}' not found!");
        return null;
    }
}
