using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject ballPrefab; // Référence au Prefab de la balle
    public PlayerController player; // Référence au PlayerController

    void Start()
    {
        // Recherche du PlayerController dans la scène
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>(); // Recherche du PlayerController dans la scène
        }
    }

    void Update()
    {
        // Vérifie si le joueur appuie sur LeftShift pour tirer
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SpawnNewBall(); // Crée une nouvelle balle
        }
    }

    void SpawnNewBall()
    {
        // Instancie une nouvelle balle à la position actuelle
        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation);

        // Définit la direction de la balle en fonction de l'orientation du joueur
        Bullet bulletScript = newBall.GetComponent<Bullet>();
        if (bulletScript != null && player != null)
        {
            // Si le joueur fait face à droite, la direction est `Vector2.right`, sinon `Vector2.left`
            Vector2 direction = player.isFacingRight ? Vector2.right : Vector2.left;
            bulletScript.SetDirection(direction); // Définit la direction de la balle
        }
    }
}
