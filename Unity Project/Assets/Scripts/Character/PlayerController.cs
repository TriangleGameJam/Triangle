using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    #region Constants

    public const float DELAY_IN_MILLISECONDS = 200;
    public const float MAX_SEQUENCE_LENGTH = 6;
    public const float BUFF_TIME = 2000;
    public const float BUFF_COOLDOWN = 10000;
    public const float DAMAGE_BUFF = 10;
    public const float DODGE_TIME = 500;
    public const float ZERO = 0;

    #endregion

    #region Properties

    private float m_MoveSpeed = 0.05f;

    [SerializeField]
    private float m_JumpForce = 500;
    private float m_Health;

    [SerializeField]
    private bool m_IsGrounded;
    private float m_Time;
    private float m_DodgeTime;
    private float m_BuffTime;
    private bool m_IsFacingRight;
    private bool m_IsLookingUp;

    private float m_DamageBuff;

    public bool m_isDodging;
    public bool m_IsDead;

    [SerializeField]
    private GameObject m_Enemy;

    [SerializeField]
    private GameObject m_Table;
    private float m_TableForce = 5.0f;

    [SerializeField]
    private GameObject m_Jayz;
    private float m_JayzForce = 2.0f;

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
        m_isDodging = false;
        m_DamageBuff = ZERO;
	}
	
	void Update () 
    {
        // remove buff after 10 secs
        if ((Time.time * 1000) - m_BuffTime >= BUFF_TIME && m_DamageBuff != 0)
        {
            UnityEngine.Debug.Log("Buff removed");
            m_DamageBuff = ZERO;
        }
        // stop dodging after 1/2 second
        if ((Time.time * 1000) - m_DodgeTime >= DODGE_TIME && m_isDodging)
        {
            UnityEngine.Debug.Log("Stopped dodging");
            m_isDodging = false;
        }
        // check for keys pressed
        foreach(KeyCode key in m_KeysPressed)
        {
            // player stopped pressing buttons, check for combo
            if (((Time.time - m_Time) * 1000 > DELAY_IN_MILLISECONDS && m_KeySequence.Length > 0))
            {
                UnityEngine.Debug.Log("Call CheckCombo");
                CheckCombo();
            }
            // player is button mashing and did not invoke a combo
            if (m_KeySequence.Length > MAX_SEQUENCE_LENGTH)
            {
                m_KeySequence = new StringBuilder();
            }

            CheckKeyPressed(key);
        }
	}

    #region Key Pressing

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    void CheckKeyPressed(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            Vector2 tmp;
            switch (key)
            {
                case KeyCode.RightArrow:
                    m_Position = transform.position;
                    tmp = new Vector2(m_Position.x + m_MoveSpeed, m_Position.y);
                    transform.position = tmp;
                    m_IsFacingRight = true;
                    break;
                case KeyCode.LeftArrow:
                    m_Position = transform.position;
                    tmp = new Vector2(m_Position.x - m_MoveSpeed, m_Position.y);
                    transform.position = tmp;
                    m_IsFacingRight = false;
                    break;
            }
        }
        if (Input.GetButton("Jump"))
        {
            if (m_IsGrounded)
            {
                rigidbody2D.AddForce(Vector3.up * m_JumpForce * Time.deltaTime, ForceMode2D.Impulse);
                m_IsGrounded = false;
            }
        }
        if (Input.GetKeyDown(key))
        {
            m_Time = Time.time;
            switch (key)
            {
                case KeyCode.Q:
                    ShoulderShrug();
                    InputSequence(key.ToString());
                    break;
                case KeyCode.W:
                    SassBlast();
                    InputSequence(key.ToString());
                    break;
                case KeyCode.E:
                    WhatevaWave();
                    InputSequence(key.ToString());
                    break;
                case KeyCode.R:
                    Slouch();
                    InputSequence(key.ToString());
                    break;
            }
        }
    }

    /// <summary>
    /// Keys pressed within 200 milliseconds of each other
    /// </summary>
    /// <param name="key"></param>
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
                JayZ();
                break;
            case "WWEQRQ":
                UnityEngine.Debug.Log("Do the Tableflip");
                m_KeySequence = new StringBuilder();
                TableFlip();
                break;
            case "EQRQ":
                // GET FACING DIRECTION
                UnityEngine.Debug.Log("Do the I'm Not Listening");
                m_KeySequence = new StringBuilder();
                ImNotListening();
                break;
            case "EEQ":
                // GET FACING DIRECTION
                UnityEngine.Debug.Log("Do the Walk Away");
                m_KeySequence = new StringBuilder();
                WalkAway();
                break;
        }
        m_KeySequence = new StringBuilder();
    }

    #endregion

    #region Melee Attacks

    /// <summary>
    /// Simple melee attack
    /// </summary>
    void ShoulderShrug()
    {
        float distance = 1;
        if (EnemyDistanceAndDirection(distance))
        {
            // deal damage to enemy
            UnityEngine.Debug.Log("Do the Shoulder Shrug");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void SassBlast()
    {
        float distance = 1;
        if (EnemyDistanceAndDirection(distance))
        {
            // deal damage to enemy
            UnityEngine.Debug.Log("Do the Sass Blast");
        }
    }

    /// <summary>
    /// Melee counter-attack
    /// </summary>
    void ImNotListening()
    {
        float distance = 1;
        if (EnemyDistanceAndDirection(distance))
        {
            // deal damage to enemy
            UnityEngine.Debug.Log("Do the I'm Not Listening");
        }
    }

    bool EnemyDistanceAndDirection(float minDistance)
    {
        var heading = m_Enemy.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.
        // enemy on right positive, enemy on left negative
        if (distance < minDistance)
        {
            if ((direction.x > 0 && m_IsFacingRight) || (direction.x < 0 && !m_IsFacingRight))
            {
                UnityEngine.Debug.Log("Facing enemy. dir: " + direction + " heading: " + heading);
                return true;
            }
        }
        UnityEngine.Debug.Log("Not facing enemy. dir: " + direction + " heading: " + heading);
        return false;
    }

    #endregion

    #region Ranged Attacks

    /// <summary>
    /// The table is a long-ranged, low damage attack
    /// </summary>
    void TableFlip()
    {
        CreateProjectile(m_Table, m_TableForce, 5.0f);
    }

    /// <summary>
    /// Short-ranged, high damage attack that needs to charge
    /// </summary>
    void JayZ()
    {
        CreateProjectile(m_Jayz, m_JayzForce, 10.0f);
    }

    /// <summary>
    /// Projectiles being thrown
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="force"></param>
    /// <param name="damage"></param>
    void CreateProjectile(GameObject prefab, float force, float damage)
    {
        Vector2 direction = (m_Enemy.transform.position - transform.position).normalized;
        Vector2 position = transform.position;
        GameObject go = (GameObject)Instantiate(prefab, position + direction, Quaternion.identity);
        Rigidbody2D body = go.GetComponent<Rigidbody2D>();
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.sender = transform;
        projectile.target = m_Enemy.transform;
        projectile.damage = damage + m_DamageBuff;
        body.AddForce(direction * force, ForceMode2D.Impulse);
    }

    #endregion

    #region Dodge

    /// <summary>
    /// Dodge with ?
    /// </summary>
    void WhatevaWave()
    {
        if ((Time.time * 1000) - m_DodgeTime < DODGE_TIME)
        {
            UnityEngine.Debug.Log("Already dodging");
        }
        else
        {
            UnityEngine.Debug.Log("WHATEVAWAVE");
            m_DodgeTime = Time.time;
            m_isDodging = true;
        }
    }

    /// <summary>
    /// Dodge with dust cloud?
    /// </summary>
    void WalkAway()
    {
        if ((Time.time * 1000) - m_DodgeTime < DODGE_TIME)
        {
            UnityEngine.Debug.Log("Already dodging");
        }
        else
        {
            UnityEngine.Debug.Log("WALKAWAY");
            m_DodgeTime = Time.time;
            m_isDodging = true;
        }
    }

    #endregion

    #region Buffs

    /// <summary>
    /// Buff
    /// </summary>
    void Slouch()
    {
        if ((Time.time * 1000) - m_BuffTime < BUFF_COOLDOWN)
        {
            UnityEngine.Debug.Log("Buff is on cooldown");
        }
        else
        {
            UnityEngine.Debug.Log("Buff has been applied");
            m_BuffTime = Time.time * 1000;
            m_DamageBuff += DAMAGE_BUFF;
        }
    }

    #endregion

    /// <summary>
    /// Check player grounded
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && !m_IsGrounded)
        {
            UnityEngine.Debug.Log("Touched Ground");
            m_IsGrounded = true;
        }
    }

    /// <summary>
    /// Subtract damage from player health
    /// </summary>
    /// <param name="dmg"></param>
    void TakeDamage(int dmg)
    {
        if (m_isDodging)
            return;

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
