using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    public void Quit()
    {
        if(!DialogueManager.Instance.audioSource.isPlaying)
        {
            Application.Quit();
        }
    }
}
