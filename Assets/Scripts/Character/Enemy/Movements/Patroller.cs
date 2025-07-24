using System;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;
    [SerializeField] private EnemyMover _enemyMover;

    private int _currentWaypointIndex;

    private void OnEnable()
    {
        _enemyMover.ReachedWaypoint += SetNextWaypoint;
    }

    private void OnDisable()
    {
        _enemyMover.ReachedWaypoint -= SetNextWaypoint;
    }

    private void Awake()
    {
        _currentWaypointIndex = 0;
        _enemyMover.SetNewWaypoint(GetWaypoint());
    }

    private void SetNextWaypoint()
    {
        _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        _enemyMover.SetNewWaypoint(GetWaypoint());
    }

    private Waypoint GetWaypoint()
    {
        return _waypoints[_currentWaypointIndex];
    }

    public void FindClosestWaypoint()
    {
        float minDistance = Int32.MaxValue;
        Waypoint closestWaypoint = null;

        foreach(Waypoint waypoint in _waypoints)
        {
            float currentDistance = (_enemyMover.transform.position - waypoint.transform.position).sqrMagnitude;

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                closestWaypoint = waypoint;
            }
        }

        _currentWaypointIndex = Array.IndexOf(_waypoints, closestWaypoint);
        _enemyMover.SetNewWaypoint(closestWaypoint);
    }
}
