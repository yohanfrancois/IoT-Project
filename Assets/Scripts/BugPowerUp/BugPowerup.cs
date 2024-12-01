using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BugPowerup : MonoBehaviour
{
    [SerializeField] private PowerupType unlocked;
    private Camera cam;
    private bool is_unlocked = false;
    private float originalFOV;
    private float smoothTime = 0.25f;
    private float velocity = 0f;
    private float zoomMultiplier = 40f;
    private float returnDelay = 2f; // Temps avant de revenir au FOV d'origine

    enum PowerupType
    {
        Platform,
        Jump,
        Inventory,
        WallJump
    }

    private void Start()
    {
        cam = Camera.main;
        originalFOV = cam.fieldOfView;
    switch (unlocked)
            {
                case PowerupType.Platform:
                    if (GameManager.Instance.unlockedPlatform){
                        this.gameObject.SetActive(false);
                    }
                    break;
                case PowerupType.Jump:
                    if (GameManager.Instance.unlockedJump){
                        this.gameObject.SetActive(false);
                    }
                    break;
                case PowerupType.Inventory:
                    if (GameManager.Instance.unlockedInventory){
                        this.gameObject.SetActive(false);
                    }
                    break;
                case PowerupType.WallJump:
                    if (GameManager.Instance.unlockedWallJump){
                        this.gameObject.SetActive(false);
                    }
                    break;
            }
    }

    private void Update()
    {
        if (is_unlocked)
        {
            Debug.Log("is_unlocked is true");
            float targetFOV = originalFOV - zoomMultiplier;
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref velocity, smoothTime);

            if (Mathf.Abs(cam.fieldOfView - targetFOV) < 0.1f)
            {
                cam.fieldOfView = targetFOV;
                is_unlocked = false;
                StartCoroutine(ReturnToOriginalFOV());
            }
        }

        /*if (unlocked == PowerupType.Platform && GameManager.Instance.unlockedPlatform == true)
        {
            gameObject.SetActive(false);
        }

        if (unlocked == PowerupType.Jump && GameManager.Instance.unlockedJump == true)
        {
            gameObject.SetActive(false);
        }

        if (unlocked == PowerupType.Inventory && GameManager.Instance.unlockedInventory == true)
        {
            gameObject.SetActive(false);
        }

        if (unlocked == PowerupType.WallJump && GameManager.Instance.unlockedWallJump == true)
        {
            gameObject.SetActive(false);
        }*/
    }

    private IEnumerator ReturnToOriginalFOV()
    {
        yield return new WaitForSeconds(returnDelay);
        Debug.Log("is_unlocked is false");
        float velocity = 0f;
        while (Mathf.Abs(cam.fieldOfView - originalFOV) > 0.1f)
        {
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, originalFOV, ref velocity, smoothTime);
            yield return null;
        }

        cam.fieldOfView = originalFOV;
        gameObject.SetActive(false); // Désactiver l'objet après avoir terminé
        PlayerController.Instance.canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out _))
        {
            is_unlocked = true;
            PlayerController.Instance.canMove = false;
            Debug.Log("Collision avec le joueur détectée, activation du zoom.");

            switch (unlocked)
            {
                case PowerupType.Platform:
                    GameManager.Instance.hasEyes = true;
                    GameManager.Instance.SetUnlockedPlatform(true);
                    break;
                case PowerupType.Jump:
                    GameManager.Instance.SetUnlockedJump(true);
                    break;
                case PowerupType.Inventory:
                    GameManager.Instance.SetUnlockedInventory(true);
                    break;
                case PowerupType.WallJump:
                    GameManager.Instance.SetUnlockedWallJump(true);
                    break;
            }
            
        }
    }
}
