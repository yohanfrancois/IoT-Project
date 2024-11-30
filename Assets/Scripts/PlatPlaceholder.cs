using System.Collections.Generic;
using UnityEngine;

public class PlatPlaceholder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer highlighterSpriteRenderer;
    [SerializeField] private GameObject plat;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    private static List<PlatPlaceholder> _placeholders;
    private SpriteRenderer _renderer;
    private bool _mouseHovering;
    private bool _spawnedPlatform;
    
    private void Awake()
    {
        _placeholders ??= new List<PlatPlaceholder>();
    }
    
    private void Start()
    {
        _placeholders.Add(this);
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnMouseOver()
    {
        _mouseHovering = !_spawnedPlatform && Inventory.Instance.GetItemCount() > 0 && GameManager.Instance.unlockedInventory;
    }
    
    private void OnMouseExit()
    {
        _mouseHovering = false;
    }
    
    private void Update()
    {
        highlighterSpriteRenderer.enabled = _mouseHovering;
        if (Inventory.Instance.GetItemCount() < 1 || !GameManager.Instance.unlockedInventory)
        {
            _renderer.color = disabledColor;
        }
        else
        {
            _renderer.color = enabledColor;
        }
    }

    public void SpawnPlatform()
    {
        Inventory.Instance.RemoveLastItem();
        _spawnedPlatform = true;
        plat.SetActive(true);
    }
    
    private void ClickRegistered()
    {
        if (!_mouseHovering) return;
        SpawnPlatform();
    }
    
    public static void MouseClicked()
    {
        if (Inventory.Instance.GetItemCount() < 1 || !GameManager.Instance.unlockedInventory) return;
        foreach (var collectible in _placeholders)
        {
            collectible.ClickRegistered();
        }
    }
}
