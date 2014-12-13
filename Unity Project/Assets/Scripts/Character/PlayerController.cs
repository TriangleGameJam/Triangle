using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public const float DELAY_IN_MILLISECONDS = 200;
    public const int MAX_SEQUENCE_LENGTH = 6;

    #region Properties

    private float m_MoveSpeed = 0.05f;

    [SerializeField]
    private float m_JumpForce = 500;
    private float m_Health;
    private bool m_IsGrounded;
    private bool m_IsDead;
    private float m_Time;

    [SerializeField]
    private CircleCollider2D m_FeetCollider;

    private ArrayList m_KeysPressed = new ArrayList();
    private StringBuilder m_KeySequence = new StringBuilder();

    private Vector2 m_Position;
    #endregion

	void Start () 
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        m_KeysPressed.Add(KeyCode.RightArrow);
        m_KeysPressed.Add(KeyCode.LeftArrow);
        m_KeysPressed.Add(KeyCode.Space);
        m_KeysPressed.Add(KeyCode.Q);
        m_KeysPressed.Add(KeyCode.W);
        m_KeysPressed.Add(KeyCode.E);
        m_KeysPressed.Add(KeyCode.R);
        m_Health = 100;
        m_IsDead = false;
        m_IsGrounded = true;
	}
	
	void Update () 
    {
        foreach(KeyCode key in m_KeysPressed)
        {
            if ((Time.time - m_Time)*1000 > DELAY_IN_MILLISECONDS || m_KeySequence.Length >= MAX_SEQUENCE_LENGTH)
            {
                CheckCombo();
            }
            // Input.GetKey
            Move(key);
            // Input.GetKeyDown
            AbilityKeys(key);
        }
	}

    void Move(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            Vector2 tmp;
            switch (key)
            {
                case KeyCode.RightArrow:
                    //UnityEngine.Debug.Log("Moving Right");
                    m_Position = transform.position;
                    tmp = new Vector2(m_Position.x + m_MoveSpeed, m_Position.y);
                    transform.position = tmp;
                    break;
                case KeyCode.LeftArrow:
                    //UnityEngine.Debug.Log("Moving Left");
                    m_Position = transform.position;
                    tmp = new Vector2(m_Position.x - m_MoveSpeed, m_Position.y);
                    transform.position = tmp;
                    break;
            }
        }
    }

    void AbilityKeys(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            m_Time = Time.time;
            switch (key)
            {
                case KeyCode.Space:
                    if (m_IsGrounded)
                    {
                        rigidbody2D.AddForce(Vector3.up * m_JumpForce * Time.deltaTime, ForceMode2D.Impulse);
                        m_IsGrounded = false;
                    }
                    
                    InputSequence(key.ToString());
                    break;
                case KeyCode.Q:
                    //PUNCH
                    Attack();
                    InputSequence(key.ToString());
                    break;
                case KeyCode.W:
                    //KICK
                    Attack();
                    InputSequence(key.ToString());
                    break;
                case KeyCode.E:
                    //DODGE
                    Dodge();
                    InputSequence(key.ToString());
                    break;
                case KeyCode.R:
                    //BLOCK
                    Dodge();
                    InputSequence(key.ToString());
                    break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && !m_IsGrounded)
        {
            UnityEngine.Debug.Log("Touched Ground");
            m_IsGrounded = true;
        }
    }

    void InputSequence(string key)
    {
        if ((Time.time - m_Time)*1000 < DELAY_IN_MILLISECONDS)
        {
            m_KeySequence.Append(key);
        }
    }

    /// <summary>
    /// Temporary key press sequences for now
    /// </summary>
    void CheckCombo()
    {
        switch(m_KeySequence.ToString())
        {
            case "WEERQQ":
                UnityEngine.Debug.Log("Do the Jay-Z");
                break;
            case "WWEQRQ":
                UnityEngine.Debug.Log("Do the Tableflip");
                break;
            case "EQRQ":
                // GET FACING DIRECTION
                UnityEngine.Debug.Log("Do the I'm Not Listening");
                break;
            case "EEQ":
                // GET FACING DIRECTION
                UnityEngine.Debug.Log("Do the Walk Away");
                break;
            case "SpaceSpace":
                UnityEngine.Debug.Log("Double Jump");
                break;
        }
        m_KeySequence = new StringBuilder();
    }

    void Attack()
    {

    }

    void Dodge()
    {

    }

    void TakeDamage(int dmg)
    {
        if (m_Health <= 0 || dmg > m_Health)
        {
            m_IsDead = true;
        }
        else
        {
            m_Health -= dmg;
        }
    }
}
