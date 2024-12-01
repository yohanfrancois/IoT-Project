using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float forceRecul = 100.0f;
    public Vector2 direction = new Vector2(1,0); // Direction de la balle, par défaut vers la droite
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
            // Accéder au Rigidbody2D du joueur
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Appliquer une force en 2D
            Vector2 force = new Vector2(-forceRecul, 0); // Force vers la gauche
            rb.AddForce(force, ForceMode2D.Impulse);
        }
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
            GameManager.Instance.LaunchEndScene();
            Destroy(collision.gameObject);
            Destroy(gameObject); // Détruit la balle si elle touche un mur
        }
    }
}

