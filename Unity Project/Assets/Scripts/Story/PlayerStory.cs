using UnityEngine;
using System.Collections;
using System.Text;

public class PlayerStory : MonoBehaviour {

    public const float DELAY_IN_MILLISECONDS = 200;

    [SerializeField]
    private float m_JumpForce = 500;
    private float m_MoveSpeed = 0.05f;
    private bool m_IsGrounded;
    private float m_Time;
    private bool m_IsFacingRight;
    private ArrayList m_KeysPressed = new ArrayList();
    private StringBuilder m_KeySequence = new StringBuilder();
    private Vector2 m_Position;
    private Animator m_PlayerAnimator;

	// Use this for initialization
	void Start () {
        m_KeysPressed.Add(KeyCode.RightArrow);
        m_KeysPressed.Add(KeyCode.LeftArrow);
        m_KeysPressed.Add(KeyCode.Space);
        m_KeysPressed.Add(KeyCode.Q);
        m_KeysPressed.Add(KeyCode.W);
        m_KeysPressed.Add(KeyCode.E);
        m_KeysPressed.Add(KeyCode.R);

        m_IsGrounded = true;
        m_PlayerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach (KeyCode key in m_KeysPressed)
        {
            CheckKeyPressed(key);
            CheckCombo();
            // set to idle
            if (!this.m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
            {
                m_PlayerAnimator.SetInteger("Ability", 0);
            }
        }
	}

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

    void CheckCombo()
    {
        if ((Time.time - m_Time) * 1000 > DELAY_IN_MILLISECONDS && m_KeySequence.Length > 0)
        {
            switch (m_KeySequence.ToString())
            {
                case PlayerCombos.SHOULDER_SHRUG:
                    m_KeySequence = new StringBuilder();
                    Animate(1);
                    break;
                case PlayerCombos.SASS_BLAST:
                    m_KeySequence = new StringBuilder();
                    Animate(2);
                    break;
                case PlayerCombos.WHATEVA_WAVE:
                    m_KeySequence = new StringBuilder();
                    Animate(3);
                    break;
                case PlayerCombos.SLOUCH:
                    m_KeySequence = new StringBuilder();
                    Animate(4);
                    break;
                case PlayerCombos.JAYZ:
                    m_KeySequence = new StringBuilder();
                    Animate(5);
                    break;
                case PlayerCombos.TABLE_FLIP:
                    m_KeySequence = new StringBuilder();
                    Animate(6);
                    break;
                case PlayerCombos.IM_NOT_LISTENING:
                    m_KeySequence = new StringBuilder();
                    Animate(7);
                    break;
                case PlayerCombos.WALK_AWAY:
                    m_KeySequence = new StringBuilder();
                    Animate(8);
                    break;
            }
            m_KeySequence = new StringBuilder();
        }
    }

    private void Animate(int animation)
    {
        m_PlayerAnimator.SetInteger("Ability", animation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && !m_IsGrounded)
        {
            m_IsGrounded = true;
            m_PlayerAnimator.SetBool("Jumping", false);
        }
    }
}
