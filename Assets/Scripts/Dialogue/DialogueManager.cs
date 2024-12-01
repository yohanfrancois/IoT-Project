using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;
    public RectTransform imageTransform;
    public Image characterImage;
    public AudioSource audioSource;
    public GameObject dialoguePanel;

    private Queue<Dialogue> dialoguesQueue;
    private bool isDialogueActive = false;
    private bool isAudioPlaying = false;
    private Queue<string> currentDialogueSegments;

    public List<AudioClip> dialoguesList;
    public List<Sprite> spritesList;

    private static int currentDialogueIndex = 0;

    public static int GetDialogueIndex()
    {
        print(currentDialogueIndex);
        currentDialogueIndex++;
        return currentDialogueIndex-1;
    }

    public static int TryDialogueIndex()
    {
        return currentDialogueIndex;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguesQueue = new Queue<Dialogue>();
        currentDialogueSegments = new Queue<string>();
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (currentDialogueSegments.Count > 0)
            {
                dialogueText.text = currentDialogueSegments.Dequeue();
            }

            else if (isDialogueActive && !isAudioPlaying)
            {
                DisplayNextSentence();
            }
        }
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialoguesQueue.Enqueue(dialogue);

        if (!isDialogueActive)
        {
            isDialogueActive = true;
            dialoguePanel.SetActive(true);
            DisplayNextSentence();
        }
    }

    void DisplayNextSentence()
    {
        if (audioSource.isPlaying)
        {
            return;
        }

        if (currentDialogueSegments.Count == 0)
        {
            if (dialoguesQueue.Count == 0)
            {
                EndDialogue();
                return;
            }

            Dialogue dialogue = dialoguesQueue.Dequeue();
            SplitDialogueText(dialogue.text);
            characterImage.sprite = dialogue.characterSprite;
            imageTransform.localPosition = dialogue.characterPosition;
            imageTransform.rotation = Quaternion.Euler(dialogue.characterRotation);

            audioSource.clip = dialogue.audioClip;
            audioSource.Play();
            isAudioPlaying = true;
            print("Playing audio"+ currentDialogueSegments.Count );
            dialogueText.text = currentDialogueSegments.Dequeue();
            StartCoroutine(WaitForAudioToEnd());
        }

        
    }

    void SplitDialogueText(string text)
    {
        currentDialogueSegments.Clear();
        int maxLength = 60; // Maximum length of each segment
        for (int i = 0; i < text.Length; i += maxLength)
        {
            if (i + maxLength < text.Length)
            {
                currentDialogueSegments.Enqueue(text.Substring(i, maxLength));
            }
            else
            {
                currentDialogueSegments.Enqueue(text.Substring(i));
            }
        }
    }

    IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        isAudioPlaying = false;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        currentDialogueSegments.Clear(); // Clear the segments queue after ending the dialogue
        dialoguesQueue.Clear();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Cedric")
        {
            IfLevelScene();
        }
        else if (scene.name == "Baptiste")
        {
            IfMenuScene();
        }
        // Ajoutez d'autres conditions pour d'autres scènes si nécessaire
    }

    public void IfLevelScene()
    {
        if(TryDialogueIndex() == 5)
        {
            Dialogue dialogue = new Dialogue
            {
                text = "Voilà, ça c’est censé être le niveau 1 ! enfin ce qu’il en reste...",
                audioClip = Instance.dialoguesList[GetDialogueIndex()],
                characterSprite = Instance.spritesList[3],
                characterPosition = new Vector3(-750, 300, 0),
                characterRotation = new Vector3(0, 0, -90)
            };

            Instance.StartDialogue(dialogue);

            Dialogue dialogue2 = new Dialogue
            {
                text = "Y’a rien qui marche, même la luminosité fonctionne plus !",
                audioClip = Instance.dialoguesList[GetDialogueIndex()],
                characterSprite = Instance.spritesList[1],
                characterPosition = new Vector3(480, 150, 0),
                characterRotation = new Vector3(0, 0, -100)
            };

            Instance.StartDialogue(dialogue2);
        }
        else if(TryDialogueIndex() == 7)
        {
            Dialogue dialogue = new Dialogue
            {
                text = "Ok super… Maintenant on peut voir que même les textures des sols ne chargent pas… trop cool !",
                audioClip = Instance.dialoguesList[GetDialogueIndex()],
                characterSprite = Instance.spritesList[1],
                characterPosition = new Vector3(480, 150, 0),
                characterRotation = new Vector3(0, 0, -100)
            };

            Instance.StartDialogue(dialogue);
        }
    }

    public void IfMenuScene()
    {

    }
}