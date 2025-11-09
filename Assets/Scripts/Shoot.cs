using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Animator animator_;
    private AudioSource gun_audio;
    private PlayerController controller_script;
    public playerHealth player_health_script;
    public GameManager gameManager;

    [Header("Shooting Settings")]
    public int maxBullets = 2;          // Max bullets player can have at once
    public float bulletRechargeTime = 1f; // Time to regenerate one bullet

    private int currentBullets;
    private bool canShoot = true;

    void Start()
    {
        animator_ = GetComponent<Animator>();
        controller_script = GetComponent<PlayerController>();
        gun_audio = GetComponent<AudioSource>();

        currentBullets = maxBullets;
    }

    void Update()
    {
        if (player_health_script.DeathStatus() == false && gameManager.GetGameStatus() == false)
        {
            if (Input.GetButtonDown("shoot") && currentBullets > 0)
            {
                ShootBullet();
            }
        }
    }

    private void ShootBullet()
    {
        // Play audio and animation
        gun_audio.Play();
        animator_.SetTrigger("shoot");

        // Spawn bullet
        Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);

        // Reduce bullet count and start cooldown to regenerate
        currentBullets--;
        StartCoroutine(RechargeBullet());
    }

    private IEnumerator RechargeBullet()
    {
        yield return new WaitForSeconds(bulletRechargeTime);

        currentBullets = Mathf.Min(currentBullets + 1, maxBullets);
    }

    // Optional: UI helper to show current bullets
    public int GetCurrentBullets()
    {
        return currentBullets;
    }
}
