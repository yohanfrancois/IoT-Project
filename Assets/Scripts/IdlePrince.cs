using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePrince : MonoBehaviour
{
    public static IdlePrince Instance { get; private set; }
    [SerializeField] private float switchSpriteSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite glitchedSprite;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchSprite());
    }

    private IEnumerator SwitchSprite()
    {
        while (!GameManager.Instance.hasPressedStart)
        {
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(switchSpriteSpeed);
            spriteRenderer.sprite = sprite2;
            yield return new WaitForSeconds(switchSpriteSpeed);
        }
        StartCoroutine(GameManager.Instance.GlitchAnimation(spriteRenderer, sprite1, sprite2, 1.5f, 0.3f));
    }
}
