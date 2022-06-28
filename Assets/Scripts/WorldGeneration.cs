using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private Vector2Int mapSize = new Vector2Int(16,16);
    [SerializeField] private Vector2Int targetPoint;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject deadZonePrefab;
    [Range(0,1)]
    [SerializeField] private float wallRate = 0.5f;
    [Range(0,1)]
    [SerializeField] private float deadZoneRate = 0.2f;

    [SerializeField] private NavMeshSurface planeNavMeshSurface;

    private int[,] map;

    public void GenerateWorld()
    {
        map = new int[mapSize.x, mapSize.y];
        map[0, 0] = 1;
        
        GenerateFreeWay(Vector2Int.zero, targetPoint);
        GenerateMap();
        planeNavMeshSurface.BuildNavMesh();

        map[0, 0] = 2;
        map[0, 1] = 2;
        map[1, 0] = 2;
        map[1, 1] = 2;
        
        GenerateDeadZones();
    }

    // One 100% free way to end point
    private void GenerateFreeWay(Vector2Int start, Vector2Int end) 
    {
        Vector2Int currentPont = start;

        while (currentPont != end)
        {
            if (currentPont.x >= mapSize.x-1)
            {
                currentPont += Vector2Int.up;
            }
            else if (currentPont.y >= mapSize.y-1)
            {
                currentPont += Vector2Int.right;
            }
            else
            {
                currentPont += Random.Range(0, 2) == 0 ? Vector2Int.up : Vector2Int.right;   
            }
            
            map[currentPont.x,currentPont.y] = 1;
        }
    }
    private void GenerateMap()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (map[x, y] != 1 && Random.Range(0f, 1f) <= wallRate)
                {
                    map[x, y] = 2;
                    Instantiate(wallPrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                }
            }
        }
    }
    private void GenerateDeadZones()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (map[x, y] != 2 && Random.Range(0f, 1f) <= deadZoneRate)
                {
                    GameObject deadZone = Instantiate(deadZonePrefab, new Vector3(x, 0.1f, y), Quaternion.identity);
                    InteractableObject interZone = deadZone.GetComponent<InteractableObject>();
                    interZone.onTriggerEnter.AddListener(playerController.SpawnParticle);
                    interZone.onTriggerEnter.AddListener(playerController.ResetPosition);
                }
            }
        }
    }
}
