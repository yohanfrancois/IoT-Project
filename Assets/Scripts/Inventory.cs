using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject itemsPrefab;
    [SerializeField] private GameObject platFormPrefab;
    public static Inventory Instance;
    private List<GameObject> _items = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(GameObject item, bool visible)
    {
        item.transform.SetParent(itemsPrefab.transform);
        _items.Add(item);
        item.SetActive(visible);
    }

    public void AddPlatToInventory(bool visible)
    {
        GameObject item = Instantiate(platFormPrefab, itemsPrefab.transform, true);
        _items.Add(item);
        item.SetActive(true);
    }
    public void RemoveLastItem()
    {
        GameObject lastItem = _items[_items.Count - 1];
        _items.Remove(lastItem);
        Destroy(lastItem);
    }

    public void DisplayItems()
    {
        foreach (var item in _items)
        {
            item.SetActive(true);
        }
    }
}
