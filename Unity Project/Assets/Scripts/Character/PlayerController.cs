using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    enum Abilities
    {
        None,
        ShoulderShrug,
        SassBlast,
        JayZ,
        TableFlip,
        WhatevaWave,
        ImNotListening,
        WalkAway,
        Slouch
    }
    public const float DELAY_IN_MILLISECONDS = 200;
    public const int MAX_SEQUENCE_LENGTH = 6;

    #region Properties

    private float m_MoveSpeed = 0.05f;

    [SerializeField]
    private float m_JumpForce = 500;
    private float m_Health;
    private bool m_IsGrounded;
    //private bool m_IsComboCompleted;
    private float m_Time;

    public bool m_IsDead;

    [SerializeField]
    private GameObject m_Enemy;
    [SerializeField]
    private GameObject m_Table;
    [SerializeField]
    private GameObject m_Jayz;

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
        m_IsGrounded = true;
        //m_IsComboCompleted = false;
	}
	
	void Update () 
    {
        foreach(KeyCode key in m_KeysPressed)
        {
            if (((Time.time - m_Time) * 1000 > DELAY_IN_MILLISECONDS && m_KeySequence.Length > 0))
                //|| ((Time.time - m_Time) * 1000 > DELAY_IN_MILLISECONDS && m_KeySequence.Length <= MAX_SEQUENCE_LENGTH))
            {
                UnityEngine.Debug.Log("Call CheckCombo");
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
                    Attack(Abilities.ShoulderShrug);
                    InputSequence(key.ToString());
                    break;
                case KeyCode.W:
                    Attack(Abilities.SassBlast);
                    InputSequence(key.ToString());
                    break;
                case KeyCode.E:
                    Dodge(Abilities.WhatevaWave);
                    InputSequence(key.ToString());
                    break;
                case KeyCode.R:
                    Dodge(Abilities.Slouch);
                    InputSequence(key.ToString());
                    break;
            }
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
                m_KeySequence = new StringBuilder();
                Attack(Abilities.JayZ);
                break;
            case "WWEQRQ":
                UnityEngine.Debug.Log("Do the Tableflip");
                Attack(Abilities.TableFlip);
                m_KeySequence = new StringBuilder();
                
                break;
            case "EQRQ":
                // GET FACING DIRECTION
                UnityEngine.Debug.Log("Do the I'm Not Listening");
                m_KeySequence = new StringBuilder();
                Dodge(Abilities.ImNotListening);
                break;
            case "EEQ":
                // GET FACING DIRECTION
                UnityEngine.Debug.Log("Do the Walk Away");
                m_KeySequence = new StringBuilder();
                Dodge(Abilities.WalkAway);
                break;
        }
        m_KeySequence = new StringBuilder();
    }

    void Attack(Abilities attack)
    {
        var heading = m_Enemy.transform.position - transform.position;

        switch (attack)
        {
            case Abilities.ShoulderShrug:
                if (heading.sqrMagnitude < 1) // maxRange * maxRange)
                {
                    UnityEngine.Debug.Log("Target is within range");
                }
                break;
            case Abilities.SassBlast:
                if (heading.sqrMagnitude < 1) // maxRange * maxRange)
                {
                    UnityEngine.Debug.Log("Target is within range");
                }
                break;
            case Abilities.JayZ:
                try
                {
                    GameObject jayz;
                    jayz = Instantiate(m_Jayz, transform.position, transform.rotation) as GameObject;
                    jayz.transform.rigidbody2D.velocity = transform.TransformDirection(Vector3.forward * 10);
                }
                catch (NullReferenceException) { }
                break;
            case Abilities.TableFlip:
                try
                {
                    GameObject table;
                    table = Instantiate(m_Table, transform.position, transform.rotation) as GameObject;
                    table.transform.rigidbody2D.velocity = transform.TransformDirection(Vector3.forward * 10);
                }
                catch (NullReferenceException) { }
                break;
        }
    }

    void Dodge(Abilities dodge)
    {
        // makes player invulnerable to certain things
        switch (dodge)
        {
            case Abilities.WhatevaWave:
                
                break;
            case Abilities.ImNotListening:
                
                break;
            case Abilities.WalkAway:
                
                break;
            case Abilities.Slouch:
                
                break;
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
