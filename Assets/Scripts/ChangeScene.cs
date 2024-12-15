using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter" + (collision.name));
        if (collision.TryGetComponent<PlayerController>(out _))
        {
            if (DialogueManager.redDialogueIndex >= 5)
            {
                SceneManager.LoadScene("Cedric");

            }

        }
    }
}