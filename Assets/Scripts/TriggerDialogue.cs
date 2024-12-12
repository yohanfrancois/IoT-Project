using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogue : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter" + (collision.name));
        if(collision.TryGetComponent<PlayerController>(out _))
        {
           if (DialogueManager.TryDialogueIndex() == 9)
            {
                print("e(nter" + (collision.name));

                Dialogue dialogue = new Dialogue
                {
                    text = "Ha regarde ce truc jaune, ça à l’air d’être un bug.",
                    audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                    characterSprite = DialogueManager.Instance.spritesList[5],
                    characterPosition = new Vector3(-635, -270, 0),
                    characterRotation = new Vector3(0, 0, -70)
                };

                DialogueManager.Instance.StartDialogue(dialogue);

                Dialogue dialogue2 = new Dialogue
                {
                    text = "Regarde si y’a pas moyen de le corriger !",
                    audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                    characterSprite = DialogueManager.Instance.spritesList[4],
                    characterPosition = new Vector3(0, 110, 0),
                    characterRotation = new Vector3(0, 0, -100)
                };

                DialogueManager.Instance.StartDialogue(dialogue2);
                Destroy(gameObject);

            }

            else if (DialogueManager.TryDialogueIndex() == 15)
            {
                Dialogue dialogue = new Dialogue
                {
                    text = "Arf ! Le saut marche, mais pas le wall jump...",
                    audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                    characterSprite = DialogueManager.Instance.spritesList[2],
                    characterPosition = new Vector3(-650, -290, 0),
                    characterRotation = new Vector3(0, 0, -100)
                };

                DialogueManager.Instance.StartDialogue(dialogue);
                Destroy(gameObject);

            }

            else if (DialogueManager.TryDialogueIndex() == 18)
            {
                Dialogue dialogue = new Dialogue
                {
                    text = "Ho daaamn… C’est le boss !",
                    audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                    characterSprite = DialogueManager.Instance.spritesList[5],
                    characterPosition = new Vector3(750, -350, 0),
                    characterRotation = new Vector3(0, 0, -110)
                };
                DialogueManager.Instance.StartDialogue(dialogue);

                Dialogue dialogue2 = new Dialogue
                {
                    text = "Et je crois que le code pour que t’ai un pistolet fonctionne pas encore.",
                    audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                    characterSprite = DialogueManager.Instance.spritesList[2],
                    characterPosition = new Vector3(600, 135, 0),
                    characterRotation = new Vector3(0, 0, -90)
                };

                DialogueManager.Instance.StartDialogue(dialogue2);
                Destroy(gameObject);

            }




        }
    }
}
