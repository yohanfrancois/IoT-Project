using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    public static GameManager Instance { get; private set; }

    public bool unlockedPlatform { get; private set; }
    public bool unlockedJump { get; private set; }
    public bool unlockedInventory { get; private set; }
    public bool unlockedWallJump { get; private set; }
    public bool canResetToMenu { get; private set; }



    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
    }

    public void SetUnlockedPlatform(bool value)
    {
        unlockedPlatform = value;
    }   

    public void SetUnlockedJump(bool value)
    {
        unlockedJump = value;
    }

    public void SetUnlockedInventory(bool value)
    {
        unlockedInventory = value;
    }
    public void SetUnlockedWallJump(bool value)
    {
        unlockedWallJump = value;
    }
    public void SetCanResetToMenu(bool value)
    {
        canResetToMenu = value;
    }

    public void ResetGame()
    {
        canResetToMenu = false;
    }

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

}
