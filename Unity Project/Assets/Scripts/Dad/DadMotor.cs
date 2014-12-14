using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DadMotor : MonoBehaviour
{
    private enum State
    {
        Action1,
        Action2
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

    [SerializeField]
    private float m_TossBeerTimer = 5.0f;
    [SerializeField]
    private float m_TossForce = 0.0f;
    [SerializeField]
    private GameObject m_TossBeerPrefab = null;

    [SerializeField]
    private float m_ActionQueueTime = 10.0f;
    private float m_ActionTimer = 0.0f;

    private State m_State = State.Action1;
    private float m_StateSwitchTimer = 30.0f;
    private float m_StateTimer = 0.0f;

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
        switch(m_State)
        {
            case State.Action1:
                {
                    m_StateTimer += Time.deltaTime;
                    m_CurrentTime += Time.deltaTime;
                    if (m_CurrentTime > m_Timer)
                    {
                        UpdateTarget();
                    }
                    Vector3 direction = (m_Target - transform.position).normalized;
                    Vector3 position = transform.position + direction * m_MovementSpeed * Time.deltaTime;
                    float distanceA = Vector3.Distance(m_Target, transform.position);
                    float distanceB = Vector3.Distance(m_Target, position);
                    if (distanceB > distanceA)
                    {
                        transform.position = m_Target;
                    }
                    else
                    {
                        transform.position = position;
                    }


                    m_ActionTimer += Time.deltaTime;
                    if (m_ActionTimer > m_ActionQueueTime)
                    {
                        //TODO: Choose a random action and queue it up
                        StartCoroutine(TossBeerRoutine());
                        m_ActionTimer = 0.0f;
                    }
                    if (m_StateTimer > m_StateSwitchTimer)
                    { 
                    }
                }
                break;
            case State.Action2:
                {

                }
                break;
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

    IEnumerator TossBeerRoutine()
    {
        yield return new WaitForSeconds(m_TossBeerTimer);
        TossBeer();

    }
    private void TossBeer()
    {
        Vector2 direction = (m_Player.transform.position - transform.position).normalized;
        Vector2 position = transform.position;
        GameObject go = (GameObject)Instantiate(m_TossBeerPrefab, position + direction, Quaternion.identity);
        Rigidbody2D body = go.GetComponent<Rigidbody2D>();
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.sender = transform;
        projectile.target = m_Player;
        projectile.damage = 5.0f;
        body.AddForce(direction * m_TossForce + Vector2.up * 5.0f, ForceMode2D.Impulse);
    }
}
