using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private SpriteRenderer highlighterSpriteRenderer;
    [SerializeField] private GameObject inventorySprite;

    private void ToggleHighlighter(bool visible)
    {
        highlighterSpriteRenderer.enabled = visible;
    }

    private void Collect()
    {
        Inventory.Instance.AddItem(inventorySprite, false);
        enabled = false;
    }
}
