using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemRequester : MonoBehaviour
{
    [SerializeField] private Item[] allItems; // Массив всех доступных айтемов
    public Item RequestedItem;

    private GameObject textContainer;

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

    

public void CreateFloatingText(string itemName)
{
    // Удаляем предыдущий текст, если он существует
    if (textContainer != null)
    {
        Destroy(textContainer);
    }

    // Создаем новый GameObject для фона
    textContainer = GameObject.CreatePrimitive(PrimitiveType.Quad);
    
    // Настраиваем материал для фона
    MeshRenderer meshRenderer = textContainer.GetComponent<MeshRenderer>();
    meshRenderer.material = new Material(Shader.Find("Unlit/Color"));
    meshRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    // Задаем размер фона
    textContainer.transform.localScale = new Vector3(2f, 1f, 1f);

    // Создаем GameObject для иконки
    GameObject iconObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
    iconObject.transform.SetParent(textContainer.transform);
    iconObject.transform.localPosition = new Vector3(-0.5f, 0, -0.01f); // Позиционируем слева
    iconObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // Размер иконки

    // Настраиваем материал для иконки
    MeshRenderer iconRenderer = iconObject.GetComponent<MeshRenderer>();
    iconRenderer.material = new Material(Shader.Find("Unlit/Transparent"));

    // Если это запрошенный предмет, получаем его иконку
    if (RequestedItem != null && RequestedItem.itemName == itemName)
    {
        iconRenderer.material.mainTexture = RequestedItem.icon;
    }
    else
    {
        // Ищем предмет по имени для получения иконки
        Item item = GetItemByName(itemName);
        if (item != null)
        {
            iconRenderer.material.mainTexture = item.icon;
        }
    }

    // Создаем новый GameObject для текста
    GameObject textObject = new GameObject("FloatingText");
    textObject.transform.SetParent(textContainer.transform);
    textObject.transform.localPosition = new Vector3(0.3f, 0, -0.01f); // Позиционируем справа от иконки

    // Добавляем компонент TextMeshPro
    TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();

    // Настраиваем параметры текста
    textMesh.text = itemName;
    textMesh.fontSize = 4;
    textMesh.alignment = TextAlignmentOptions.Left; // Выравнивание по левому краю
    textMesh.color = Color.white;

    // Настраиваем размер области текста
    textMesh.rectTransform.sizeDelta = new Vector2(1.4f, 1f);
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
