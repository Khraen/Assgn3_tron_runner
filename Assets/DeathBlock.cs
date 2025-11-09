using Unity.VisualScripting;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    public GameManager game_manager_script;
    public playerHealth player_health_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
        {
            player_health_script.kill();
            game_manager_script.GameOver();
    }
  }
}
