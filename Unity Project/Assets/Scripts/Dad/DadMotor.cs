using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DadMotor : MonoBehaviour
{
    private enum State
    {
        Idle,
        Waiting,
        Attacking,
        MovingToGoal
    }

    [SerializeField]
    private Transform m_Player = null;

    private Vector3 m_Target = Vector3.zero;
    [SerializeField]
    private float m_Timer = 1.0f;
    [SerializeField]
    private float m_MovementSpeed = 5.0f;
    private float m_CurrentTime = 0.0f;

    [SerializeField]
    private Transform m_WaypointRoot = null;
    [SerializeField]
    private List<Transform> m_Waypoints = new List<Transform>();
    [SerializeField]
    private Transform m_TargetWayPoint = null;


	// Use this for initialization
	void Start () 
    {
        if(m_WaypointRoot != null)
        {
            foreach(Transform child in m_WaypointRoot)
            {
                m_Waypoints.Add(child);
            }
        }

        UpdateTarget();
	}
	
	void Update()
    {
        m_CurrentTime += Time.deltaTime;
        if(m_CurrentTime > m_Timer)
        {
            UpdateTarget();
        }
        Vector3 direction = (m_Target - transform.position).normalized;
        Vector3 position = transform.position + direction * m_MovementSpeed * Time.deltaTime;
        float distanceA = Vector3.Distance(m_Target, transform.position);
        float distanceB = Vector3.Distance(m_Target, position);
        if(distanceB > distanceA)
        {
            transform.position = m_Target;
        }
        else
        {
            transform.position = position;
        }
        
    }

    void UpdateTarget()
    {
        m_Target = m_Player.transform.position;
        m_CurrentTime = 0.0f;
        GetTargetWaypoint();
        if (m_TargetWayPoint != null)
        {
            m_Target = m_TargetWayPoint.position;
        }
    }

    void GetTargetWaypoint()
    {
        Transform bestWaypoint = null;
        float shortestDistance = float.MaxValue;
        foreach(Transform waypoint in m_Waypoints)
        {
            float distance = (m_Target - waypoint.position).sqrMagnitude;
            if(distance < shortestDistance)
            {
                bestWaypoint = waypoint;
                shortestDistance = distance;
            }
        }
        m_TargetWayPoint = bestWaypoint;
    }
}
