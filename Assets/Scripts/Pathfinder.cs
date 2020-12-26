using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWayPoint, endWayPoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down
    };
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Dictionary<Vector2Int, Waypoint> exploredNodes = new Dictionary<Vector2Int, Waypoint>();
    bool isRunning;
    Waypoint currentSearchCenter;

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
    }

    private void Pathfind()
    {
        StartPathFinder();
        if (startWayPoint == endWayPoint) {
            Debug.Log("End point is the same as starting point! Stopping pathfinder...");
            return;
        } else {
            queue.Enqueue(startWayPoint);

            while(queue.Count > 0 && isRunning) {
                currentSearchCenter = queue.Dequeue();
                if (isEndpoint()) {
                    StopPathFinder();
                } else {
                    ExploreNeighbours(currentSearchCenter);
                }
            }
        }
    }

    private bool isEndpoint() {
        return currentSearchCenter == endWayPoint;
    }

    private void StartPathFinder() {
        isRunning = true;
    }

    private void StopPathFinder() {
        isRunning = false;
    }

    private void ExploreNeighbours(Waypoint centerNode)
    {
        foreach(Vector2Int direction in directions) {
            Vector2Int neighbourCoordinates = centerNode.GetGridPos() + direction;
            EnqueueNewNeighbour(neighbourCoordinates);
        }
        exploredNodes.Add(centerNode.GetGridPos(), centerNode);
        centerNode.SetTopColor(Color.black);
    }

    private void EnqueueNewNeighbour(Vector2Int neighbourCoordinates) {
        if (grid.ContainsKey(neighbourCoordinates)) {
            Waypoint neighbour = grid[neighbourCoordinates];

            bool isNodeExplored = exploredNodes.ContainsKey(neighbourCoordinates);
            if (!isNodeExplored && !queue.Contains(neighbour)) {
                queue.Enqueue(neighbour);
                neighbour.previousWaypoint = currentSearchCenter;
            }
        }
    }

  private void ColorStartAndEnd()
    {
        startWayPoint.SetTopColor(Color.blue);
        endWayPoint.SetTopColor(Color.red);
    }

    void LoadBlocks() {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints) {
            bool isOverlaping = grid.ContainsKey(waypoint.GetGridPos());
            if (!isOverlaping) {
                grid.Add(waypoint.GetGridPos(), waypoint);
            } else {
                Debug.LogWarning(
                    "Skipping duplicate found at position: " +
                    waypoint.GetGridPos().x + 
                    "," +
                    waypoint.GetGridPos().y
                );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
