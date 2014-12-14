using System.Collections;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Handles user input for main character
/// Ctrl + M, Ctrl + L (or P) to stop hiding all code
/// Ctrl + M, Ctrl + O to hide all code
/// </summary>
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

    [SerializeField]
    private float m_JumpForce = 500;
    private float m_MoveSpeed = 0.05f;
    private float m_Health;
    private bool m_IsGrounded;
    private float m_Time;
    private float m_DodgeTime;
    private float m_BuffTime;
    private bool m_IsFacingRight;
    private bool m_IsLookingUp;
    private float m_DamageBuff;
    private bool m_IsDodging;
    private bool m_IsDead;

    /// <summary>
    /// Make it easier to do combos
    /// </summary>
    [SerializeField]
    private bool NathansHackBool;

    [SerializeField]
    private GameObject m_Enemy;

    [SerializeField]
    private GameObject m_Table;

    [SerializeField]
    private GameObject m_Jayz;

    private ArrayList m_KeysPressed = new ArrayList();
    private StringBuilder m_KeySequence = new StringBuilder();

    private Vector2 m_Position;

    [SerializeField]
    private CameraShake m_CameraShake = null;
    [SerializeField]
    private float m_CameraShakeTime = 0.5f;
    private List<IAbilityHandler> m_AbilityHandlers = new List<IAbilityHandler>();

    private Animator m_PlayerAnimator;
    private string m_CurrAnimation;

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
        m_IsDodging = false;
        m_DamageBuff = ZERO;
        m_PlayerAnimator = GetComponent<Animator>();
        m_CurrAnimation = string.Empty;
        NathansHackBool = true;
	}
	
	void Update () 
    {
        NathansHack();

        CheckTimers();
        // check for keys pressed
        foreach (KeyCode key in m_KeysPressed)
        {
            CheckKeyPressed(key);
            CheckCombo();
            // set to idle
            if (!this.m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
            {
                m_PlayerAnimator.SetInteger("Ability", 0);
            }
            // player is button mashing and did not invoke a combo
            if (m_KeySequence.Length > MAX_SEQUENCE_LENGTH)
            {
                m_KeySequence = new StringBuilder();
            }
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
                    m_PlayerAnimator.SetBool("FacingRight", true);
                    m_PlayerAnimator.SetBool("Idle", false);
                    break;
                case KeyCode.LeftArrow:
                    m_Position = transform.position;
                    tmp = new Vector2(m_Position.x - m_MoveSpeed, m_Position.y);
                    transform.position = tmp;
                    m_IsFacingRight = false;
                    m_PlayerAnimator.SetBool("FacingRight", false);
                    m_PlayerAnimator.SetBool("Idle", false);
                    break;
            }
        }
        if (Input.GetKeyUp(key))
        {
            switch (key)
            {
                case KeyCode.RightArrow:
                case KeyCode.LeftArrow:
                    m_PlayerAnimator.SetBool("Idle", true);
                    break;
            }
        }
        if (Input.GetButton("Jump"))
        {
            if (m_IsGrounded)
            {
                rigidbody2D.AddForce(Vector3.up * m_JumpForce * Time.deltaTime, ForceMode2D.Impulse);
                m_IsGrounded = false;
                m_PlayerAnimator.SetBool("Jumping", true);
            }
        }
        if (Input.GetKeyDown(key))
        {
            m_Time = Time.time;
            switch (key)
            {
                case KeyCode.Q:
                case KeyCode.W:
                case KeyCode.E:
                case KeyCode.R:
                    if ((Time.time - m_Time) * 1000 < DELAY_IN_MILLISECONDS)
                    {
                        m_KeySequence.Append(key);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Temporary key press sequences for now
    /// </summary>
    void CheckCombo()
    {
        if ((Time.time - m_Time) * 1000 > DELAY_IN_MILLISECONDS && m_KeySequence.Length > 0)
        {
            Debug.Log(m_KeySequence.ToString());
            switch (m_KeySequence.ToString())
            {
                case PlayerCombos.SHOULDER_SHRUG:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.ShoulderShrug, 1);
                    break;
                case PlayerCombos.SASS_BLAST:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.SassBlast, 2);
                    break;
                case PlayerCombos.WHATEVA_WAVE:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.WhatevaWave, 3);
                    break;
                case PlayerCombos.SLOUCH:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.Slouch, 4);
                    break;
                case PlayerCombos.JAYZ:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.JayZ, 5);
                    break;
                case PlayerCombos.TABLE_FLIP:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.TableFlip, 6);
                    break;
                case PlayerCombos.IM_NOT_LISTENING:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.ImNotListening, 7);
                    break;
                case PlayerCombos.WALK_AWAY:
                    m_KeySequence = new StringBuilder();
                    AnimateAndExecute(AbilityType.WalkAway, 8);
                    break;
            }
            m_KeySequence = new StringBuilder();
        }
    }

    private void AnimateAndExecute(AbilityType ability, int animation)
    {
        m_PlayerAnimator.SetInteger("Ability", animation);
        ExecuteAbility(ability);
    }

    /// <summary>
    /// Check timers for buffs and dodges
    /// </summary>
    private void CheckTimers()
    {
        // remove buff after 10 secs
        if ((Time.time * 1000) - m_BuffTime >= BUFF_TIME && m_DamageBuff > 0)
        {
            Debug.Log("Buff removed");
            m_DamageBuff = ZERO;
        }
        // stop dodging after 1/2 second
        if ((Time.time * 1000) - m_DodgeTime >= DODGE_TIME && m_IsDodging)
        {
            Debug.Log("Stopped dodging");
            m_IsDodging = false;
        }
    }

    #endregion

    #region Player methods

    /// <summary>
    /// Check player grounded
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && !m_IsGrounded)
        {
            Debug.Log("Touched Ground");
            m_IsGrounded = true;
            m_PlayerAnimator.SetBool("Jumping", false);
        }
    }

    /// <summary>
    /// Subtract damage from player health
    /// </summary>
    /// <param name="dmg"></param>
    void TakeDamage(int dmg)
    {
        if (m_IsDodging)
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

    #endregion

    #region Abilities

    /// <summary>
    /// Used to register for events
    /// </summary>
    /// <param name="aHandler"></param>
    public void Register(IAbilityHandler aHandler)
    {
        m_AbilityHandlers.Add(aHandler);
    }
    /// <summary>
    /// Used to unregister from events.
    /// </summary>
    /// <param name="aHandler"></param>
    public void Unregister(IAbilityHandler aHandler)
    {
        m_AbilityHandlers.Remove(aHandler);
    }

    public void ExecuteAbility(AbilityType aAbility)
    {
        m_CameraShake.Shake(m_CameraShakeTime);
        if(m_AbilityHandlers.Count > 0)
        {
            Debug.Log("Executing ability " + aAbility.ToString());
            m_AbilityHandlers.ForEach(Element => Element.OnExecuteAbility(m_Enemy, aAbility));
        }
    }

    public void NathansHack()
    {
        transform.rotation = Quaternion.identity;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AnimateAndExecute(AbilityType.JayZ, 5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnimateAndExecute(AbilityType.TableFlip, 6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AnimateAndExecute(AbilityType.ImNotListening, 7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AnimateAndExecute(AbilityType.WalkAway, 8);
        }
    }

    #endregion

    #region Public accessors

    public float buffTime
    {
        get { return m_BuffTime; }
        set { m_BuffTime = value; }
    }
    public float damageBuff
    {
        get { return m_DamageBuff; }
        set { m_DamageBuff = value; }
    }
    public float dodgeTime
    {
        get { return m_DodgeTime; }
        set { m_DodgeTime = value; }
    }
    public bool isDodging
    {
        get { return m_IsDodging; }
        set { m_IsDodging = value; }
    }
    public bool isDead
    {
        get { return m_IsDead; }
        set { m_IsDead = value; }
    }

    #endregion
}
