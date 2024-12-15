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
        transform.Translate(speed * Time.deltaTime * direction.normalized);
    }

    // Cette méthode permet de définir la direction de la balle
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.gunSound);
    }
    // Gère les collisions avec d'autres objets
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si la balle touche un joueur
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.firstExplosion);
            Destroy(gameObject); // Détruit la balle si elle touche un mur
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("StartingBlock"))
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.secondExplosion);
            GameManager.Instance.LaunchEndScene();
            Destroy(collision.gameObject);
            Destroy(gameObject); // Détruit la balle si elle touche un mur
        }
    }
}

