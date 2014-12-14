using UnityEngine;
using System.Collections;

/// <summary>
/// A piece of the mothers body. Always following the piece infront of it.
/// </summary>
public class MomBodyPart : MonoBehaviour 
{
    private enum State
    {
        Dead,
        Vulnerable,
        Regenerating,
        Attached
    }

    [SerializeField]
    private Transform m_Target = null;
    [SerializeField]
    private MomBodyMotor m_Motor = null;
    [SerializeField]
    private float m_Distance = 1.0f;
    [SerializeField]
    private float m_MovementSpeed = 2.0f;

    [SerializeField]
    private float m_RecouperateTimer = 5.0f;
    

    private State m_State = State.Attached;
    private float m_CurrentTime = 0.0f;
    
	
	// Update is called once per frame
	void Update () 
    {
        switch(m_State)
        {
            case State.Attached:
                if (m_Target != null)
                {
                    //Calculate the direction from the target to this transform
                    Vector3 direction = (transform.position - m_Target.position).normalized;
                    Vector3 targetPoint = m_Target.position + direction * m_Distance;
                    transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * m_MovementSpeed);
                    m_CurrentTime = 0.0f;
                }
                break;
            case State.Vulnerable:
                if(m_Motor != null)
                {
                    m_CurrentTime += Time.deltaTime;
                    if(m_CurrentTime > m_RecouperateTimer)
                    {
                        MomBodyPart part = m_Motor.GetBack(this);
                        if(part != null)
                        {
                            m_Target = part.transform;
                            m_State = State.Regenerating;
                            m_CurrentTime = 0.0f;
                        }
                    }
                }
                break;
            case State.Regenerating:
                if(m_Target != null)
                {
                    m_CurrentTime += Time.deltaTime;
                    Vector3 direction = (transform.position - m_Target.position).normalized;
                    Vector3 targetPoint = m_Target.position + direction * m_Distance;
                    transform.position = Vector3.Lerp(transform.position, targetPoint, m_CurrentTime);
                    if(m_CurrentTime > 1.0f)
                    {
                        m_State = State.Attached;
                        m_CurrentTime = 0.0f;
                        collider2D.isTrigger = true;
                        rigidbody2D.isKinematic = true;
                    }
                }
                break;
            default:
                //Dead:
                break;
        }
      
        
	}

    public void OnTriggerEnter2D(Collider2D aCollider)
    {
        if(m_State == State.Attached)
        {
            Projectile proj = aCollider.GetComponent<Projectile>();
            //TODO: Check for a projectile hit and then detach
            if(proj != null && proj.sender != transform)
            {
                m_Motor.Detach(this);
                collider2D.isTrigger = false;
                rigidbody2D.isKinematic = false;
            }
            
        }
        else if(m_State == State.Vulnerable)
        {
            //TODO: Check for player contact. Destroy body part.
            if(aCollider.GetComponent<PlayerController>())
            {
                gameObject.SetActive(false);
                m_Motor.Destroy(this);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (m_State == State.Vulnerable)
        {
            //TODO: Check for player contact. Destroy body part.
            if (aCollision.collider.GetComponent<PlayerController>())
            {
                gameObject.SetActive(false);
                m_Motor.Destroy(this);
            }
        }
    }

    public void OnDetached()
    {
        m_State = State.Vulnerable;
    }


    public Transform target
    {
        get { return m_Target; }
        set { m_Target = value; }
    }
    public MomBodyMotor motor
    {
        get { return m_Motor; }
        set { m_Motor = value; }
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
    public bool isActive
    {
        get { return m_State == State.Regenerating || m_State == State.Attached; }
    }
    public bool isAttached
    {
        get { return m_State == State.Attached; }
    }
}
