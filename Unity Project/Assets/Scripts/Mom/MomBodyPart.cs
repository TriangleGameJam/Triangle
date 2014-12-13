using UnityEngine;
using System.Collections;

public class MomBodyPart : MonoBehaviour 
{

    private MomBodyMotor m_Motor = null;
    private Transform m_Target = null;
    private MomBodyPart m_Next = null;
    private Vector3 m_LastPosition = Vector3.zero;
    private Vector3 m_TargetPosition = Vector3.zero;

    private float m_CurrentTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(m_Motor != null)
        {
            transform.position = Vector3.Lerp(m_LastPosition, m_TargetPosition, m_Motor.currentTime);
        }
        //if(m_CurrentTime > 1.0f)
        //{
        //    transform.position = Vector3.Lerp(m_LastPosition, m_TargetPosition, 1.0f);
        //    m_CurrentTime = 0.0f;
        //    UpdateTarget();
        //}
	}
    void UpdateTarget()
    {
        if(m_Target != null)
        {
            m_LastPosition = transform.position;
            m_TargetPosition = m_Target.position;
        }
    }

    /// <summary>
    /// Gets called initially by the MomBodyMotor to update the positions of all the body parts.
    /// </summary>
    /// <param name="aLast">The last position (start position) to move from.</param>
    /// <param name="aTarget">The target position to move towards</param>
    public void UpdatePositions(Vector3 aLast, Vector3 aTarget)
    {
        ///If theres another body part in the chain update its position first with this ones current
        if (m_Next != null)
        {
            m_Next.UpdatePositions(m_LastPosition, m_TargetPosition);
        }
        //Update the new positions
        m_LastPosition = aLast;
        m_TargetPosition = aTarget;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta * 0.65f;
        Gizmos.DrawSphere(m_LastPosition, 0.15f);
        Gizmos.DrawLine(m_LastPosition, m_TargetPosition);
        Gizmos.DrawSphere(m_TargetPosition, 0.15f);
    }

    public Vector3 lastPosition
    {
        get { return m_LastPosition; }
        set { m_LastPosition = value; }
    }
    public Vector3 targetPosition
    {
        get { return m_TargetPosition; }
        set { m_TargetPosition = value; }
    }
    public MomBodyMotor motor
    {
        get { return m_Motor; }
        set { m_Motor = value; }
    }
    public MomBodyPart next
    {
        get { return m_Next; }
        set { if (m_Next != this) { m_Next = value; } }
    }
}
