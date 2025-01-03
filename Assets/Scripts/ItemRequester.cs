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
        Debug.Log(RequestedItem.itemNameForeign);

        // Создаем всплывающий текст
        CreateFloatingText(RequestedItem.itemNameForeign);
    }

    private void Update()
    {
        if (textContainer != null)
        {
            // Позиционируем контейнер над игроком
            Vector3 newPosition = new Vector3(
                transform.position.x,
                transform.position.y + 5f,
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

        // Создаем новый GameObject для общего контейнера (фон + иконка + текст)
        textContainer = new GameObject("FloatingBackground");
        
        // Добавляем Quad в качестве «фона»
        GameObject backgroundQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        backgroundQuad.name = "BackgroundQuad";
        backgroundQuad.transform.SetParent(textContainer.transform, false);

        // Настраиваем материал для фона (слегка затемнённый)
        MeshRenderer backgroundRenderer = backgroundQuad.GetComponent<MeshRenderer>();
        backgroundRenderer.material = new Material(Shader.Find("Unlit/Color"));
        backgroundRenderer.material.color = new Color(0.45f, 0.55f, 0.0f, 0.5f); // полупрозрачный чёрный

        // Задаём размер фона
        backgroundQuad.transform.localScale = new Vector3(3f, 2f, 1f);

        // Создаем контейнер для «иконка + текст», чтобы их можно было красиво разместить по центру
        GameObject contentContainer = new GameObject("ContentContainer");
        contentContainer.transform.SetParent(textContainer.transform, false);
        // Ставим в (0,0) относительно фона
        contentContainer.transform.localPosition = Vector3.zero;

        // Создаем GameObject для иконки
        GameObject iconObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        iconObject.transform.SetParent(contentContainer.transform, false);
        iconObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f); 
        // Поднимем иконку немного вверх
        iconObject.transform.localPosition = new Vector3(0f, 0.3f, -0.01f);

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
            else {
                  iconObject.SetActive(false);
            }
            
        }

        // Создаем новый GameObject для текста
        GameObject textObject = new GameObject("FloatingText");
        textObject.transform.SetParent(contentContainer.transform, false);
        // Опускаем текст чуть ниже иконки
        textObject.transform.localPosition = new Vector3(0f, -0.2f, -0.01f);

        // Добавляем компонент TextMeshPro
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();

        // Настраиваем параметры текста
        textMesh.text = itemName;
        textMesh.fontSize = 4;
        textMesh.alignment = TextAlignmentOptions.Center; 
        textMesh.color = Color.white;

        // Делаем небольшую обводку для более симпатичного вида
        textMesh.outlineWidth = 0.2f;            // толщина обводки
        textMesh.outlineColor = Color.black;     // цвет обводки

        // Настраиваем размер области текста (чтобы текст точно влез)
        textMesh.rectTransform.sizeDelta = new Vector2(2f, 1f);
    }

    // Получить несколько случайных айтемов
    public Item[] GetRandomItems(int count)
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("No items found!");
            return null;
        }

        // Ограничиваем количество запрашиваемых айтемов доступным количеством
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

public Item GetItemByName(string itemName)
{
    if (allItems == null || allItems.Length == 0)
    {
        Debug.LogWarning("No items found!");
        return null;
    }

    foreach (Item item in allItems)
    {
        if (item.itemName.ToLower() == itemName.ToLower() || 
            item.itemNameForeign.ToLower() == itemName.ToLower())
        {
            return item;
        }
    }

    Debug.LogWarning($"Item with name '{itemName}' not found!");
    return null;
}
}
