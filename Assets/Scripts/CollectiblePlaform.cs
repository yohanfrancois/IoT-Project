using System.Collections.Generic;
using UnityEngine;

public class CollectiblePlaform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer highlighterSpriteRenderer;
    private static List<CollectiblePlaform> _collectibles;
    private bool _mouseHovering;

    private void Awake()
    {
        _collectibles ??= new List<CollectiblePlaform>();
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
        if (_collectibles == null) return;
        foreach (var collectible in _collectibles)
        {
            collectible.ClickRegistered();
        }
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.collectibleSound);
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
