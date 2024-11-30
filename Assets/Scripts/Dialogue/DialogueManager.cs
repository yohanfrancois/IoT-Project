using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public AudioSource audioSource;
    public GameObject dialoguePanel;

    private Queue<Dialogue> dialoguesQueue;
    private bool isDialogueActive = false;

    public List<AudioClip> dialoguesList;
    public List<Sprite> spritesList;


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
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if(isDialogueActive)
        {
            DisplayNextSentence();
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
        if (dialoguesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue dialogue = dialoguesQueue.Dequeue();
        dialogueText.text = dialogue.text;
        characterImage.sprite = dialogue.characterSprite;
        characterImage.transform.position = dialogue.characterPosition;
        characterImage.transform.rotation = Quaternion.Euler(dialogue.characterRotation);

        audioSource.clip = dialogue.audioClip;
        audioSource.Play();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}