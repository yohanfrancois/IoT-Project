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
    private bool isAudioPlaying = false;
    private Queue<string> currentDialogueSegments;

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

            if (isDialogueActive && !isAudioPlaying)
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
            characterImage.transform.position = dialogue.characterPosition;
            characterImage.transform.rotation = Quaternion.Euler(dialogue.characterRotation);

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
}