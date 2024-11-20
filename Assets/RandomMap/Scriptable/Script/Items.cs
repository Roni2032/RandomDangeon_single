using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class  ItemData
{
    public string ID;

    public string displayName;

    public Sprite sprite;

    
}
[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public ItemData GetItemData(string id)
    {
        foreach (var item in items)
        {
            if(item.ID == id) return item;
        }
        return null;
    }
    public List<ItemData> items = new List<ItemData>();
}
