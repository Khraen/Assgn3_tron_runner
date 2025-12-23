using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour
{
    private Animator animator_;
  private int health = 2;
  private SpriteRenderer sprite_;
  public GameObject AttkBox;
  Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    animator_ = GetComponent<Animator>();
    sprite_ = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();

    }

  // Update is called once per frame
  void Update()
  {

  }
    

    private IEnumerator GlowRed()
{


    if (sprite_ != null)
    {
        
        Color originalColor = sprite_.color;

        
        sprite_.color = Color.red;

        yield return new WaitForSeconds(0.25f); // glow duration
        sprite_.color = originalColor;
    }
    else
    {
        Debug.LogWarning("SpriteRenderer not found on this GameObject!");
    }
}

    private void Die()
  {
    AttkBox.SetActive(false);
    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    GetComponent<BoxCollider2D>().enabled = false;
    StartCoroutine(FadeOut(1.5f));
    
    

  }
  
  private void Hurt()
  {
    StartCoroutine(GlowRed());
    health--;
    if(health <= 0)
    {
      Die();
    }
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("bullet"))
    {
      Hurt();
    }
  }
  private IEnumerator FadeOut(float duration = 1f)
{
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (sr == null) yield break;

    Color originalColor = sr.color;
    float elapsed = 0f;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        yield return null;
    }

    // Make sure it's fully invisible at the end
    sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    Destroy(gameObject);

}

}
