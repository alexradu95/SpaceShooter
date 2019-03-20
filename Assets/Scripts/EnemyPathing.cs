using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] WaveConfig waveConfig;
    int waypointIndex = 0;
    Vector2 myPosition;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        myPosition = transform.position;
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector2 targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (myPosition == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
