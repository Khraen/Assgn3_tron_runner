using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("References")]
    public Transform arm;               // The arm where projectiles spawn
    public GameObject projectilePrefab; // The projectile prefab
    public Animator animator;           // The boss's animator
    public Transform player;            // Reference to player
    public GameObject beam;             // Beam attack (child object)

    [Header("Attack Settings")]
    public float minShootDelay = 2f;   // Minimum time between attacks
    public float maxShootDelay = 5f;   // Maximum time between attacks

    private Coroutine attackCoroutine;
    public Spawner spawner_script;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (beam != null)
            beam.SetActive(false); // Make sure beam is initially off
    }

    void OnEnable()
    {
        attackCoroutine = StartCoroutine(AttackRoutine());
    }

    void OnDisable()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        // Make sure beam is off if boss is disabled
        if (beam != null)
            beam.SetActive(false);
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1f); // Optional: small delay before first attack

        while (true)
        {
            float waitTime = Random.Range(minShootDelay, maxShootDelay);
            yield return new WaitForSeconds(waitTime);

            if (spawner_script.isBossPhase == false)
                continue; // Skip if it's not boss phase

            // Randomly choose attack: 0 = projectile, 1 = beam
            int attackChoice = Random.Range(0, 2);

            if (attackChoice == 0)
            {
                // Projectile attack
                animator.SetTrigger("Projectile");
            }
            else
            {
                // Beam attack
                if (beam != null)
                {
                    animator.SetTrigger("Beam"); // Optional: trigger beam animation
                    beam.SetActive(true);
                }
            }
        }
    }

    // Called by Animation Event for projectile
    public void FireProjectile()
    {
        if (spawner_script.isBossPhase == false)
            return;

        GameObject projectile = Instantiate(projectilePrefab, arm.position, Quaternion.identity);
        HomingProjectile proj = projectile.GetComponent<HomingProjectile>();
        if (proj != null)
            proj.SetTarget(player);
    }

}
