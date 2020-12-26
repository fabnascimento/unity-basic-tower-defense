using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        Stack<Waypoint> path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(Stack<Waypoint> pathToFollow)
    {
        while(pathToFollow.Count > 0)
        {
            Waypoint waypoint = pathToFollow.Pop();
            yield return MoveTo(waypoint.transform.position);
        }
    }

    IEnumerator MoveTo(Vector3 to)
    {
        while(Vector3.Distance(transform.position, to) > 0)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                to,
                speed * Time.deltaTime
            );
            yield return new WaitForEndOfFrame();
        }
    }
}
