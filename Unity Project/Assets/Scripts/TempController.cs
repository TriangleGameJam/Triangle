using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class TempController : MonoBehaviour 
{
    [SerializeField]
    private float m_MovementSpeed = 3.0f;
    [SerializeField]
    private float m_JumpForce = 5.0f;
    [SerializeField]
    private float m_MaxMovementSpeed = 3.0f;
    [SerializeField]
    private float m_CollisionError = 0.1f;


    [SerializeField]
    private bool m_IsGrounded = false;
    private Vector2 m_NetForce = Vector2.zero;


	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        bool jump = Input.GetButton("Jump");

        //float yForce = rigidbody2D.velocity.y;
        //rigidbody2D.velocity = new Vector2(hInput * m_MovementSpeed * Time.deltaTime, yForce);
        rigidbody2D.AddForce(new Vector2(hInput * m_MovementSpeed,0.0f), ForceMode2D.Impulse);
        if(jump && m_IsGrounded)
        {
            rigidbody2D.AddForce(new Vector2(0.0f, m_JumpForce), ForceMode2D.Impulse);
        }


        rigidbody2D.AddForce(m_NetForce);
        rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x, -m_MaxMovementSpeed, m_MaxMovementSpeed), rigidbody2D.velocity.y);
        rigidbody2D.angularVelocity = 0.0f;
        transform.rotation = Quaternion.identity;
	}

    void FixedUpdate()
    {
        Vector2 topLeft = transform.position;
        Vector2 bottomRight = transform.position;

        BoxCollider2D bCol = collider2D as BoxCollider2D;
        if(bCol != null)
        {
            topLeft.x += -bCol.size.x * 0.5f - m_CollisionError;
            topLeft.y += bCol.size.y * 0.5f + m_CollisionError;
            bottomRight.x += bCol.size.x * 0.5f + m_CollisionError;
            bottomRight.y += -bCol.size.y * 0.5f - m_CollisionError;
        }

        int layerMask = 1 <<  gameObject.layer;
        layerMask = ~layerMask;

        Collider2D col = Physics2D.OverlapArea(topLeft, bottomRight, layerMask );

        if (col != null )
        {
            m_IsGrounded = true;
            if(col.transform.position.y > transform.position.y)
            {
                m_IsGrounded = false;
            }
        }
        else
        {
            m_IsGrounded = false;
        }
        rigidbody2D.angularVelocity = 0.0f;
    }

    void AddForce(Vector2 aForce)
    {
        m_NetForce += aForce;
    }
    
    void OnDrawGizmos()
    {
        Vector2 topLeft = transform.position;
        Vector2 bottomRight = transform.position;

        BoxCollider2D bCol = collider2D as BoxCollider2D;
        if (bCol != null)
        {
            topLeft.x += -bCol.size.x * 0.5f - m_CollisionError;
            topLeft.y += bCol.size.y * 0.5f + m_CollisionError;
            bottomRight.x += bCol.size.x * 0.5f + m_CollisionError;
            bottomRight.y += -bCol.size.y * 0.5f - m_CollisionError;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(topLeft, bottomRight);

    }
}
