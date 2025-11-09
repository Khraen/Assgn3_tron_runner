using UnityEngine;

public class ChunkMove : MonoBehaviour
{
    public float moveSpeed = 5f;  // Units per second

    void Update()
    {
        // Move the platform left every frame
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
