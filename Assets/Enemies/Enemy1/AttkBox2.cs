using UnityEngine;

public class AttkBox2 : MonoBehaviour
{
    // public GameObject player_object;
  public FlyingE enemy_script;
  private Animator animator_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
  {
    
 animator_ = enemy_script.GetComponent<Animator>();
  }


    

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.gameObject.CompareTag("Player"))
    {
      animator_.SetTrigger("Attk");
      Debug.Log("animating attk");
      
    }
    
  }
}

