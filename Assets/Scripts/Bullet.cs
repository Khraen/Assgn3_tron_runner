using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("In On trigger2d colliderr");
        /*if (other.CompareTag("enemy"))
        {
          //glowred
       } */
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(TilemapCollider2D other)
    {
        Debug.Log("In On trigger2d tilemapcollider");
    
        Destroy(gameObject);
    }
    
}
