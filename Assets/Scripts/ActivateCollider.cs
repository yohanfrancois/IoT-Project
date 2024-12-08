using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCollider : MonoBehaviour
{

    private void Start()
    {
        if (DialogueManager.TryDialogueIndex() >= 9)
        {
            gameObject.SetActive(false);
        }
    }
}
