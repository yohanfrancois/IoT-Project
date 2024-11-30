using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private int lifePoint = 100;       // Points de vie du mob
    [SerializeField] private float speed = 3f;          // Vitesse de déplacement du mob
    [SerializeField] private float detectionRadius = 5f; // Rayon de détection du joueur
    [SerializeField] private float moveRange = 5f;      // Plage de déplacement pour le va-et-vient (gauche / droite)
    private Transform player;                           // Référence au joueur
    private bool isChasing = false;                     // Indicateur pour savoir si le mob poursuit le joueur
    private Rigidbody2D rb;                             // Référence au Rigidbody2D du mob
    private float initialPositionX;                     // Position initiale en X
    private bool movingRight = true;                    // Direction du mouvement automatique (gauche/droite)
    private float patrolTargetX;                        // Cible du mouvement de patrouille en X

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                // Récupère le Rigidbody2D du mob
        initialPositionX = transform.position.x;        // Sauvegarde la position initiale
        patrolTargetX = initialPositionX + moveRange;    // La première cible de patrouille est initialement `moveRange` à droite
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            // Calcul de la direction sur l'axe X uniquement pour suivre le joueur
            Vector2 directionToPlayer = new Vector2(player.position.x - transform.position.x, 0).normalized;

            // Déplacement du mob uniquement sur l'axe X
            rb.velocity = new Vector2(directionToPlayer.x * speed, rb.velocity.y); // La vitesse y reste inchangée pour éviter de sauter ou de tomber
        }
        else
        {
            // Mouvement automatique de gauche à droite lorsqu'il ne poursuit pas le joueur
            PatrolMovement();
        }

        // Si les points de vie du mob sont à zéro ou moins, il meurt
        if (lifePoint <= 0)
        {
            Die();
        }
    }

    // Cette méthode est appelée pour infliger des dégâts au mob
    public void takeDamage(int damage)
    {
        lifePoint -= damage;

        // Vérifie si la vie du mob est à zéro ou moins
        if (lifePoint <= 0)
        {
            Die(); // Appelle la méthode Die() si la vie est épuisée
        }
    }

    // Cette méthode est appelée lorsque le mob meurt
    private void Die()
    {
        Destroy(gameObject); // Détruit le mob
    }

    // Cette méthode gère le mouvement automatique de gauche à droite (patrouille)
    private void PatrolMovement()
    {
        // Déplace le mob vers la cible (patrolTargetX) à chaque frame
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolTargetX, transform.position.y), speed * Time.deltaTime);

        // Si le mob a atteint sa cible, on inverse la direction
        if (Mathf.Abs(transform.position.x - patrolTargetX) < 0.1f)
        {
            // Inverse la direction du mouvement
            movingRight = !movingRight;
            
            // Met à jour la cible de patrouille
            patrolTargetX = movingRight ? initialPositionX + moveRange : initialPositionX - moveRange;
        }
    }

    // Cette méthode est appelée lorsqu'un objet entre dans la zone de détection
}
