using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> chunkPrefabs;   // All chunks (Chunk0 should be at index 0)
    public Transform player;
    public float spawnTriggerDistance = 10f;
    public int maxChunks = 5;

    public bool isBossPhase = false; // controlled by GameManager

    private List<GameObject> activeChunks = new List<GameObject>();
    private Transform lastEndPoint;

    void Start()
    {
        GameObject firstChunk = GameObject.Find("Chunk0");
        if (firstChunk == null)
        {
            Debug.LogError("Spawner: Could not find 'Chunk0' in the scene!");
            return;
        }

        activeChunks.Add(firstChunk);

        lastEndPoint = firstChunk.transform.Find("EndPt");
        if (lastEndPoint == null)
        {
            Debug.LogError("Spawner: 'Chunk0' is missing an 'EndPt' child transform.");
        }
    }

    void Update()
    {
        if (player == null || lastEndPoint == null)
            return;

        if (Vector2.Distance(player.position, lastEndPoint.position) < spawnTriggerDistance)
        {
            SpawnNextChunk();
        }
    }

    void SpawnNextChunk()
    {
        GameObject newChunk;

        if (isBossPhase)
        {
            // Always spawn Chunk0 during boss phase
            newChunk = Instantiate(chunkPrefabs[0]);
        }
        else
        {
            // Random chunk otherwise (excluding Chunk0 if desired)
            int index = Random.Range(1, chunkPrefabs.Count);
            newChunk = Instantiate(chunkPrefabs[index]);
        }

        Transform newStart = newChunk.transform.Find("StartPt");
        Transform newEnd = newChunk.transform.Find("EndPt");

        if (newStart == null || newEnd == null)
        {
            Debug.LogError("Spawner: New chunk prefab is missing StartPt or EndPt!");
            Destroy(newChunk);
            return;
        }

        // Align new chunk
        Vector3 offset = lastEndPoint.position - newStart.position;
        newChunk.transform.position += offset;

        lastEndPoint = newEnd;
        activeChunks.Add(newChunk);

        // Clean up old chunks
        if (activeChunks.Count > maxChunks)
        {
            Destroy(activeChunks[0]);
            activeChunks.RemoveAt(0);
        }
    }
}
