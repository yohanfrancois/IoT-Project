using System.Collections;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private int lifePoint; // Points de vie du mob
    [SerializeField] private GameObject ballPrefab; // Référence au prefab de la balle
    [SerializeField] private float fireRate; // Temps entre chaque tir (en secondes)
    [SerializeField] private Vector2 offset; // Décalage pour la position de spawn des balles
    [SerializeField] private Collider2D headCollider; // Collider pour la tête du mob
    private Animator _animator;
    private int _playerInRangeTriggerHash;
    private int _enemyDeathTriggerHash;

    void Start()
    {
        //StartCoroutine(FireBulletEveryXSeconds());         // Démarre le tir automatique
        _animator = GetComponent<Animator>();
        _playerInRangeTriggerHash = Animator.StringToHash("PlayerInRange");
        _enemyDeathTriggerHash = Animator.StringToHash("EnemyDeathTrigger");
    }

    void Update()
    {
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

    // Cette méthode gère le tir automatique toutes les `fireRate` secondes
    IEnumerator FireBulletEveryXSeconds()
    {
        while (true) // Boucle infinie pour tirer des balles en continu
        {
            SpawnNewBall(); // Appelle la fonction pour créer une nouvelle balle
            yield return new WaitForSeconds(fireRate); // Attend `fireRate` secondes avant de tirer à nouveau
        }
    }

    // Méthode pour instancier une nouvelle balle avec une direction aléatoire
    void SpawnNewBall()
    {
        // Instancie une nouvelle balle à la position et rotation de l'objet auquel le script est attaché
        GameObject newBall = Instantiate(ballPrefab, transform.position + (Vector3)offset, transform.rotation);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.gunSound);

        // Récupère le script Bullet de la balle instanciée
        Bullet bulletScript = newBall.GetComponent<Bullet>();
        if (bulletScript)
        {
            // Génére un angle aléatoire entre -45 et 45 degrés (par exemple) pour la direction
            float randomAngle = Random.Range(-45f, 45f);

            // Convertit cet angle en une direction Vector2
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                Mathf.Sin(randomAngle * Mathf.Deg2Rad));

            // Applique la direction aléatoire à la balle
            bulletScript.SetDirection(-randomDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7) //Player layer
        {
            _animator.SetTrigger(_playerInRangeTriggerHash);
            StartCoroutine(FireBulletEveryXSeconds());
        }
    }

    // Cette méthode est appelée lorsque le mob meurt
    public void Die()
    {
        Dialogue dialogue = new Dialogue
        {
            text = "Wow tu lui as pris son pistolet ! Y’a peut être moyen de libérer ton prince avec :D",
            audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
            characterSprite = DialogueManager.Instance.spritesList[0],
            characterPosition = new Vector3(-720, 135, 0),
            characterRotation = new Vector3(0, 0, -90)
        };

        DialogueManager.Instance.StartDialogue(dialogue);

        _animator.SetTrigger(_enemyDeathTriggerHash);
        Destroy(gameObject, 0.7f); // Détruit le mob
    }

    // Méthode pour gérer les collisions avec la tête
}