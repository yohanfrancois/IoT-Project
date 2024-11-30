using UnityEngine;

public class BugPowerup : MonoBehaviour
{
    [SerializeField] private PowerupType unlocked;

    enum PowerupType
    {
        Platform,
        Jump,
        Inventory,
        WallJump
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out _))
        {
            switch (unlocked)
            {
                case PowerupType.Platform:
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
            Destroy(gameObject);
        }
    }
}