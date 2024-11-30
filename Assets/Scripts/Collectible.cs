using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private SpriteRenderer highlighterSpriteRenderer;
    //[SerializeField] private GameObject inventorySprite;
    private static List<Collectible> _collectibles;
    private bool _mouseHovering;

    private void Awake()
    {
        _collectibles ??= new List<Collectible>();
    }

    private void Start()
    {
        _collectibles.Add(this);
    }

    private void ToggleHighlighter(bool visible)
    {
        highlighterSpriteRenderer.enabled = visible;
    }

    private void Collect()
    {
        Inventory.Instance.AddPlatToInventory(false);
        Destroy(gameObject);
    }

    public static void MouseClicked()
    {
        foreach (var collectible in _collectibles)
        {
            collectible.ClickRegistered();
        }
    }

    private void ClickRegistered()
    {
        if (!_mouseHovering) return;
        Collect();
    }

    private void OnMouseOver()
    {
        _mouseHovering = true;
    }

    private void OnMouseExit()
    {
        _mouseHovering = false;
    }

    private void Update()
    {
        highlighterSpriteRenderer.enabled = _mouseHovering;
    }
}
