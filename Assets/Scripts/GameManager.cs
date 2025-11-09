using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Spawner spawner;           
    public Transform bossTransform;   
    public float bossMoveSpeed = 2f;  
    private float bossRiseAmount = 8f; 

    private Vector3 bossHiddenPos;    
    private Vector3 bossVisiblePos;   
    public Boss boss_script;
    public GameObject DeathPanel;
    public bool bossDead = false;
    public GameObject WinPanel;
    private bool game_over = false;
    public GameObject Boss_object;
    private AudioSource audio;

    void Start()
    {
        Time.timeScale = 1f;
        audio = GetComponent<AudioSource>();
        game_over = false;
        if (spawner == null || bossTransform == null)
        {
            Debug.LogError("GameManager: Missing references!");
            return;
        }

        bossHiddenPos = bossTransform.position;
        bossVisiblePos = bossHiddenPos + new Vector3(0, bossRiseAmount, 0);

        StartCoroutine(BossPhaseCycle());
    }

    IEnumerator BossPhaseCycle()
    {
        yield return new WaitForSeconds(20f); // initial delay

        while (true)
        {
            // Check if boss is dead
            if (bossDead)
            {
                // Stop boss attacks
                if (boss_script != null) boss_script.enabled = false;

                // Keep boss phase active
                spawner.isBossPhase = true;

                // Move boss down 10 units
                Vector3 fallPos = bossTransform.position - new Vector3(0, 10f, 0);
                yield return StartCoroutine(MoveBoss(bossTransform.position, fallPos));

                // Trigger win
                WinGame();
                yield break; // stop the cycle
            }

            // Regular boss phase OFF
            spawner.isBossPhase = false;
            Boss_object.GetComponent<BoxCollider2D>().enabled = false;
            if (boss_script != null) boss_script.enabled = false;
            yield return StartCoroutine(MoveBoss(bossTransform.position, bossHiddenPos));
            
            // Keep boss hidden for a while
            yield return new WaitForSeconds(15f);

            // Boss phase ON
            spawner.isBossPhase = true;
            Boss_object.GetComponent<BoxCollider2D>().enabled = true;
            yield return StartCoroutine(MoveBoss(bossTransform.position, bossVisiblePos));
            if (boss_script != null) boss_script.enabled = true;

            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator MoveBoss(Vector3 from, Vector3 to)
    {
        var animator = bossTransform.GetComponent<Animator>();
        if (animator != null) animator.enabled = false;

        float distance = Vector3.Distance(from, to);
        float duration = distance / bossMoveSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            bossTransform.position = Vector3.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        bossTransform.position = to;

        if (animator != null) animator.enabled = true;
    }

    public void GameOver()
    {
        DeathPanel.SetActive(true);
        audio.Stop();
        Time.timeScale = 0f;
        game_over = true;
    }

    public void RestartBTN()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitBTN()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void WinGame()
    {
        
        game_over = true;
        WinPanel.SetActive(true);
        audio.Stop();
        Time.timeScale = 0f;
        // Here you can load a Win scene or show a Win UI
        // Example: SceneManager.LoadScene("WinScene");
    }
    public bool GetGameStatus()
  {
        return game_over;
  }
}
