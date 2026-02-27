using NUnit.Framework;
using UnityEngine;

/// <summary>
/// A class representing an item useable in the inventory:
/// GetName()
/// GetDescription()
/// </summary>
public class Item
{
    private string name;
    private Sprite sprite;
    private string description;
    public Item(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
    public void setDescription(string des)
    {
        description = des;
    }

    public string GetDescription()
    {
        return "" + description;
    }
    public string GetName()
    {
        return "" + name;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }

}