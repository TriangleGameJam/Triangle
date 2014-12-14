using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
    public float m_MoveSpeed = 5.0f;
    public float m_JumpForce = 250.0f;

    void Awake()
    {
        Debug.Log("hi");
    }
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        bool moveRight = Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow);
        bool moveLeft = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        if(moveRight)
        {
            Debug.Log("Moving Right");
            rigidbody2D.AddForce(Vector2.right * m_MoveSpeed * Time.deltaTime,ForceMode2D.Impulse);
        }
        if(moveLeft)
        {
            Debug.Log("Moving Left");
            rigidbody2D.AddForce(-Vector2.right * m_MoveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if(jump)
        {
            Debug.Log("Jumping");
            rigidbody2D.AddForce(Vector2.up * m_JumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }

	}
    void LateUpdate()
    {

    }
    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider aCollider)
    {

    }
    void OnTriggerExit(Collider aCollider)
    {

    }
    void OnTriggerStay(Collider aCollider)
    {

    }
}
