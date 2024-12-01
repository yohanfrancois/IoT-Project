using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    public static GameManager Instance ;
    public bool buttonAnimationPlayed = false;

    public bool unlockedPlatform ;
    public bool unlockedJump ;
    public bool unlockedInventory ;
    public bool unlockedWallJump ;
    public bool canResetToMenu ;
    public bool hasEyes;
    public bool hasGun=false;
    public bool hasPressedStart;



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
        Dialogue dialoguex = new Dialogue
        {
            text = "Ok... Super… Maintenant on peut voir que même les textures des sols ne chargent pas… trop cool !",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[2],
            characterPosition = new Vector3(-22, 17, 0),
            characterRotation = new Vector3(0, 0, -292)
        };

        DialogueManager.Instance.StartDialogue(dialoguex);

        Dialogue dialogue = new Dialogue
        {
            text = "Bon. On a réparé les textures, mais tu peux toujours pas sauter…",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[3],
            characterPosition = new Vector3(750, -390, 0),
            characterRotation = new Vector3(0, 0, -110)
        };

        DialogueManager.Instance.StartDialogue(dialogue);

        Dialogue dialogue2 = new Dialogue
        {
            text = "Essaye de voir si la touche R fonctionne toujours pour réinitialiser le jeu",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(0, 0, 0),
            characterRotation = new Vector3(0, 0, -90)
        };

        DialogueManager.Instance.StartDialogue(dialogue2);

        AudioManager.Instance.PlayMusic(2);
    }   

    public void SetUnlockedJump(bool value)
    {
        unlockedJump = value;
        Dialogue dialogue = new Dialogue
        {
            text = "Mais noooon ? T’as réparé le saut !!!",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[5],
            characterPosition = new Vector3(-630, 175, 0),
            characterRotation = new Vector3(0, 0, -65)
        };

        DialogueManager.Instance.StartDialogue(dialogue);

        Dialogue dialogue2 = new Dialogue
        {
            text = "Vas y mon petit lapin, bondis maintenant !",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[4],
            characterPosition = new Vector3(535, 175, 0),
            characterRotation = new Vector3(0, 0, -105)
        };

        DialogueManager.Instance.StartDialogue(dialogue2);
        GameManager.Instance.hasEyes = false;


        PlayerController.Instance.GlitchAnim();
    }

    public void SetUnlockedInventory(bool value)
    {
        unlockedInventory = value;
        Dialogue dialogue = new Dialogue
        {
            text = "Ah par contre ça c’est cool ! Tu devrais pouvoir placer des blocs en cliquant :D",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(765, -210, 0),
            characterRotation = new Vector3(0, 0, -105)
        };

        DialogueManager.Instance.StartDialogue(dialogue);
    }
    public void SetUnlockedWallJump(bool value)
    {
        unlockedWallJump = value;
        Dialogue dialogue = new Dialogue
        {
            text = "Au début, je croyais pas en toi… Mais le jeu commence à bien remarcher là",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[4],
            characterPosition = new Vector3(50, 150, 0),
            characterRotation = new Vector3(0, 0, -70)
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

    public void LaunchEndScene()
    {
        StartCoroutine(LaunchEndCoroutine());
    }

    public IEnumerator LaunchEndCoroutine()
    {
        Dialogue dialogue = new Dialogue
        {
            text = "Incroyable, je crois que le jeu refonctionne bien",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[4],
            characterPosition = new Vector3(-680, 210, 0),
            characterRotation = new Vector3(0, 0, -90)
        };

        DialogueManager.Instance.StartDialogue(dialogue);

        Dialogue dialogue2 = new Dialogue
        {
            text = "Bon bah tu peux passer au niveau suivant maintenant",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(630, 140, 0),
            characterRotation = new Vector3(0, 0, -90)
        };

        DialogueManager.Instance.StartDialogue(dialogue2);

        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("ComingSoon");

        Dialogue dialogue7 = new Dialogue
        {
            text = "AH non ! j’avais oublié ! Les devs ont toujours pas codé la suite",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[4],
            characterPosition = new Vector3(0, -350, 0),
            characterRotation = new Vector3(0, 0, -90)
        };

        DialogueManager.Instance.StartDialogue(dialogue7);

        Dialogue dialogue20 = new Dialogue
        {
            text = " Mais, c’est à venir... Faudra juste payer le DLC à 45€ !",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(350, -350, 0),
            characterRotation = new Vector3(0, 0, -120)
        };

        DialogueManager.Instance.StartDialogue(dialogue20);

    }

}
