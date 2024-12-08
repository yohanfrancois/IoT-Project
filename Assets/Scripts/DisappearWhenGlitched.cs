using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearWhenGlitched : MonoBehaviour
{
    [SerializeField] private int minDialogNumber;
    [SerializeField] private int maxDialogNumber;


    void Start()
    {
        if(DialogueManager.TryDialogueIndex() >= minDialogNumber && DialogueManager.TryDialogueIndex() < maxDialogNumber)
        {
            gameObject.SetActive(false);
        }
    }

}
