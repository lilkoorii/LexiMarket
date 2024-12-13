using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        // Создаем текст 
        CreateFloatingText(RequestedItem.itemName);
    }

    private void Update()
    {
        if (textContainer != null)
        {
            // Позиционируем контейнер над игроком
            Vector3 newPosition = new Vector3(
                transform.position.x,
                transform.position.y + 2f,
                transform.position.z
            );

            textContainer.transform.position = newPosition;

            // Поворачиваем лицом к камере
            textContainer.transform.LookAt(Camera.main.transform);
            textContainer.transform.Rotate(0, 180, 0);
        }
    }

    // Получить один случайный айтем
    public Item GetRandomItem()
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("No items found!");
            return null;
        }
        return allItems[Random.Range(0, allItems.Length)];
    }

    private GameObject textContainer; // Ссылка на объект с текстом

    private void CreateFloatingText(string itemName)
    {
        // Создаем новый GameObject для фона
        textContainer = GameObject.CreatePrimitive(PrimitiveType.Quad);

        // Настраиваем материал для фона
        MeshRenderer meshRenderer = textContainer.GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Unlit/Color"));
        meshRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

        // Задаем размер фона
        textContainer.transform.localScale = new Vector3(2f, 1f, 1f);

        // Создаем новый GameObject для текста
        GameObject textObject = new GameObject("FloatingText");
        textObject.transform.SetParent(textContainer.transform);
        textObject.transform.localPosition = new Vector3(0, 0, -0.5f); // Немного впереди фона

        // Добавляем компонент TextMeshPro
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();

        // Настраиваем параметры текста
        textMesh.text = itemName;
        textMesh.fontSize = 4;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.color = Color.white;
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
