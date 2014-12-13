using UnityEngine;
using System.Collections;

/// <summary>
/// A piece of the mothers body. Always following the piece infront of it.
/// </summary>
public class MomBodyPart : MonoBehaviour 
{
    [SerializeField]
    private Transform m_Target = null;
    [SerializeField]
    private float m_Distance = 1.0f;
    [SerializeField]
    private float m_MovementSpeed = 2.0f;
	
	// Update is called once per frame
	void Update () 
    {
	    //Calculate the direction from the target to this transform
        Vector3 direction = (transform.position - m_Target.position).normalized;
        Vector3 targetPoint = m_Target.position + direction * m_Distance;
        transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * m_MovementSpeed);
	}

    public Transform target
    {
        get { return m_Target; }
        set { m_Target = value; }
    }
    public float distance
    {
        get { return m_Distance; }
        set { m_Distance = value; }
    }
    public float movementSpeed
    {
        get { return m_MovementSpeed; }
        set { m_MovementSpeed = value; }
    }
}
