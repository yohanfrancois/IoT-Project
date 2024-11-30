using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject ballPrefab; // Référence au Prefab de la balle

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            SpawnNewBall();
        }
    }

    void SpawnNewBall()
    {
        // Instancie une nouvelle balle à la position et rotation de l'objet auquel le script est attaché
        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation);
    }
}
