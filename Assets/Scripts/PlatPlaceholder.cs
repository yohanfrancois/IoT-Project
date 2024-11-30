using System.Collections.Generic;
using UnityEngine;

public class PlatPlaceholder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer highlighterSpriteRenderer;
    [SerializeField] private GameObject plat;
    private static List<PlatPlaceholder> _placeholders;
    private bool _mouseHovering;
    private bool _spawnedPlatform;
    
    private void Awake()
    {
        _placeholders ??= new List<PlatPlaceholder>();
    }
    
    private void Start()
    {
        _placeholders.Add(this);
    }
    
    private void OnMouseOver()
    {
        _mouseHovering = true && !_spawnedPlatform;
    }
    
    private void OnMouseExit()
    {
        _mouseHovering = false;
    }
    
    private void Update()
    {
        highlighterSpriteRenderer.enabled = _mouseHovering;
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
        if (Inventory.Instance.GetItemCount() < 1) return;
        foreach (var collectible in _placeholders)
        {
            collectible.ClickRegistered();
        }
    }
}
