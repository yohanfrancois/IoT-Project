using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject itemsPrefab;
    [SerializeField] private GameObject platFormPrefab;
    [SerializeField] private GameObject bg;
    public static Inventory Instance;
    private List<GameObject> _items = new();

    private void Awake()
    {
        Instance = this;
    }

    public int GetItemCount()
    {
        return _items.Count;
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

    private void Update()
    {
        if (GameManager.Instance.unlockedInventory)
        {
            DisplayItems();
        }
        else
        {
            HideItems();
        }
    }

    public void DisplayItems()
    {
        foreach (var item in _items)
        {
            item.SetActive(true);
        }

        bg.SetActive(true);
    }

    public void HideItems()
    {
        //Debug.Log("hiding items");
        foreach (var item in _items)
        {
            item.SetActive(false);
        }

        bg.SetActive(false);
    }
}
