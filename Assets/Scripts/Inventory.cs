using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject itemsPrefab;
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
