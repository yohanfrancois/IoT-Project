using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > 9)
        {
            SceneManager.LoadScene("Cedric");
            
        } 
    }
}
