using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // Direction de la balle (par défaut : vers la droite)
    public float speed = 5f;                  // Vitesse de déplacement
    public int damage = 10;                   // Dégâts infligés par la balle

    void Update()
    {
        // Déplace la balle selon la direction spécifiée
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    // Vérifie si la balle touche un obstacle ou un mob
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si la balle touche un mob
        if (collision.gameObject.TryGetComponent<Mob>(out Mob mob))
        {
            // Inflige des dégâts au mob
            mob.takeDamage(damage);

            // Détruit la balle après collision
            Destroy(gameObject);
        }

        // Vérifie si la balle touche un objet du layer "Wall" (anciennement "Ground")
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // Détruire la balle si elle touche un mur
            Destroy(gameObject);
        }
    }
}
