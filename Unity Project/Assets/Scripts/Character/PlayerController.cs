using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public const float DELAY = 0.5f;
    public const int MAX_SEQUENCE_LENGTH = 6;

    #region Properties

    private float m_MoveSpeed = 0.05f;
    private float m_JumpForce = 2.2f;
    private float m_Health;
    private float m_Time;
    private bool m_IsDead;

    private ArrayList m_KeysPressed = new ArrayList();
    private StringBuilder m_KeySequence = new StringBuilder();

    private Vector2 m_Position;
    #endregion

	void Start () 
    {
        m_KeysPressed.Add(KeyCode.RightArrow);
        m_KeysPressed.Add(KeyCode.LeftArrow);
        m_KeysPressed.Add(KeyCode.Space);
        m_KeysPressed.Add(KeyCode.Q);
        m_KeysPressed.Add(KeyCode.W);
        m_KeysPressed.Add(KeyCode.E);
        m_KeysPressed.Add(KeyCode.R);
        m_Health = 100;
        m_IsDead = false;
	}
	
	void Update () 
    {
        foreach(KeyCode key in m_KeysPressed)
        {
            if ((Time.time - m_Time) > DELAY)
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
                    m_Position = transform.position;
                    Vector2 tmp = new Vector2(m_Position.x, m_Position.y + m_JumpForce);
                    transform.position = tmp;
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

    void InputSequence(string key)
    {
        if ((Time.time - m_Time) < DELAY)
        {
            m_KeySequence.Append(key);
            UnityEngine.Debug.Log(m_KeySequence.ToString());
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
