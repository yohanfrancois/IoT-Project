using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    public static GameManager Instance ;

    public bool unlockedPlatform ;
    public bool unlockedJump ;
    public bool unlockedInventory ;
    public bool unlockedWallJump ;
    public bool canResetToMenu ;



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
        Dialogue dialogue = new Dialogue
        {
            text = "Bonjour, ceci est un test de dialogue.",
            audioClip = DialogueManager.Instance.dialoguesList[0],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(0, 5, 0),
            characterRotation = new Vector3(0, 0, 0)
        };

        DialogueManager.Instance.StartDialogue(dialogue);
    }   

    public void SetUnlockedJump(bool value)
    {
        unlockedJump = value;
        Dialogue dialogue = new Dialogue
        {
            text = "Oui",
            audioClip = DialogueManager.Instance.dialoguesList[1],
            characterSprite = DialogueManager.Instance.spritesList[1],
            characterPosition = new Vector3(-10, 0, 0),
            characterRotation = new Vector3(0, 0, 0)
        };

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void SetUnlockedInventory(bool value)
    {
        unlockedInventory = value;
        Dialogue dialogue = new Dialogue
        {
            text = "NOOOOOOON",
            audioClip = DialogueManager.Instance.dialoguesList[2],
            characterSprite = DialogueManager.Instance.spritesList[2],
            characterPosition = new Vector3(5, 0, 0),
            characterRotation = new Vector3(0, 0, -90)
        };

        DialogueManager.Instance.StartDialogue(dialogue);
    }
    public void SetUnlockedWallJump(bool value)
    {
        unlockedWallJump = value;
        Dialogue dialogue = new Dialogue
        {
            text = "Bonjour,klndllkdnd",
            audioClip = DialogueManager.Instance.dialoguesList[3],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(10, 0, 0),
            characterRotation = new Vector3(0, 0, 90)
        };

        DialogueManager.Instance.StartDialogue(dialogue);
    }
    public void SetCanResetToMenu(bool value)
    {
        canResetToMenu = value;
        
    }

    public void ResetGame()
    {
        canResetToMenu = false;
        if(unlockedPlatform){
            GameObject[] unityObjects = GameObject.FindGameObjectsWithTag("Plateforme");
            // Loop through each object and perform an action
            foreach (GameObject obj in unityObjects)
            {
                // Perform an action on each object, for example:
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f,1f,1f,1f);
            }
        }
    }

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void AffichePlateformes(){
        if(unlockedPlatform){
            GameObject[] unityObjects = GameObject.FindGameObjectsWithTag("Plateforme");
            // Loop through each object and perform an action
            foreach (GameObject obj in unityObjects)
            {
                // Perform an action on each object, for example:
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f,1f,1f,1f);
            }
        }
        else
        {
            GameObject[] unityObjects = GameObject.FindGameObjectsWithTag("Plateforme");
            // Loop through each object and perform an action
            foreach (GameObject obj in unityObjects)
            {
                // Perform an action on each object, for example:
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

}
