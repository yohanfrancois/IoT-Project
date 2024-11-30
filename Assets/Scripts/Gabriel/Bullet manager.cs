using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject ballPrefab; // Référence au Prefab de la ball

    // Update is called once per frame
    void Update()
    {
         // Vérifie si la touche "A" est pressée
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnNewBall();
        }
    }
    void SpawnNewBall()
    {
        // Instancie une nouvelle balle à la position et rotation du spawnPoint
        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation);
    }

}
