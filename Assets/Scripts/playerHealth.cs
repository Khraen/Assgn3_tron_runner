using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{

  private int health = 1;
  private bool isDead = false;
  private SpriteRenderer sprite_;
  public GameManager game_manager_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    sprite_ = GetComponent<SpriteRenderer>();
    // Auto-find GameManager if not assigned in inspector
    if (game_manager_script == null)
        game_manager_script = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
  {
  }
    private void Death()
  {
    isDead = true;
    // play death cue. Then end game. Maybe glow red and pause everythign with fail screen.
    //death error
    game_manager_script.GameOver();

    }

  public void takeDmg()
  {
    health--;
    StartCoroutine(GlowRed());
    Debug.Log("Took dmg");
    if(health <= 0)
    {
      Death();
    }
    
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("enemy"))
    {
      Debug.Log("Player took damage.");
      takeDmg();
    }
  }

  public bool DeathStatus()
  {
    return isDead;
  }
  public void kill()
  {
    isDead = true;
  }
  private IEnumerator GlowRed()
{
    if (sprite_ != null)
    {
        Color originalColor = sprite_.color;
        sprite_.color = Color.red;

        // Wait using unscaled time
        yield return new WaitForSecondsRealtime(0.25f);

        sprite_.color = originalColor;
    }
    else
    {
        Debug.LogWarning("SpriteRenderer not found on this GameObject!");
    }
}

}
