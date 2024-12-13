using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Фрукт,
    Овощ
}

[CreateAssetMenu(fileName = "New Item", menuName = "LexiMarket/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string itemNameForeign;
    public string shape;
    public string shapeForeign;
    public string color;
    public string colorForeign;
    public ItemType type = ItemType.Фрукт;
    public string typeForeign;
    public Sprite icon = null;
}
