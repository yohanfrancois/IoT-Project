using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // Direction de la balle, par défaut vers la droite
    public float speed = 5f;                  // Vitesse de la balle
    public int damage = 10;                   // Dégâts infligés par la balle

    void Update()
    {
        // Déplace la balle selon la direction spécifiée
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    // Cette méthode permet de définir la direction de la balle
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
    // Gère les collisions avec d'autres objets
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si la balle touche un joueur
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            // Applique des dégâts au joueur (à implémenter dans PlayerController)
            Destroy(gameObject); // Détruit la balle après collision
        }
        // Vérifie si la balle touche un mob
        else if (collision.gameObject.TryGetComponent<Mob>(out Mob mob))
        {
            Destroy(gameObject); // Détruit la balle après collision
        }
        // Vérifie si la balle touche un mur (Layer "Wall")
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject); // Détruit la balle si elle touche un mur
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("StartingBlock"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject); // Détruit la balle si elle touche un mur
        }
    }
}

