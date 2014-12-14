using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The main controller of the mothers head.
/// </summary>
public class MomBodyMotor : MonoBehaviour 
{
    /// <summary>
    /// An internal state variable for the mom.
    /// </summary>
    private enum State
    {
        Waiting,
        Moving
    }
    /// <summary>
    /// How many body parts to create initially.
    /// </summary>
    [SerializeField]
    private int m_BodyPartCount = 0;
    private int m_CurrentBodyParts = 0;
    /// <summary>
    /// A prefab dedicated to 
    /// </summary>
    [SerializeField]
    private GameObject m_BodyPrefab = null;
    /// <summary>
    /// The head of the motor
    /// </summary>
    [SerializeField]
    private Transform m_Head = null;
    /// <summary>
    /// How fast the character moves.
    /// </summary>
    [SerializeField]
    private float m_MovementSpeed = 1.0f;
    /// <summary>
    /// How much time the mom waits when she reaches her target.
    /// </summary>
    [SerializeField]
    private float m_WaitTime = 1.0f;
    /// <summary>
    /// Defines the min and max region areas for top and bottom vector generation.
    /// </summary>
    [SerializeField]
    private Vector2 m_MinMaxYBounds = new Vector2(10.0f, 20.0f);
    /// <summary>
    /// CurrentTime should only be within values of 0 to 1
    /// </summary>
    private float m_CurrentTime = 0.0f;
    /// <summary>
    /// The position the character is currently moving towards.
    /// </summary>
    private Vector3 m_TargetPosition = Vector3.zero;
    /// <summary>
    /// The last position the character was at.
    /// </summary>
    private Vector3 m_CurrentPosition = Vector3.zero;
    /// <summary>
    /// A list of body parts the motor controls
    /// </summary>
    private List<MomBodyPart> m_Parts = new List<MomBodyPart>();
    /// <summary>
    /// The current state of the body motor.
    /// </summary>
    private State m_State = State.Waiting;

    [SerializeField]
    private MomBodyBehaviour m_Behaviour = null;

    /// <summary>
    /// Start like method. Does error checking and what not to see if the motor is good to run or naw.
    /// </summary>
    private void OnEnable()
    {
        ///Check requirements
        if(m_Head == null)
        {
            enabled = false;
            Debug.LogWarning("Missing head");
            return;
        }
        if(m_BodyPrefab == null)
        {
            enabled = false;
            Debug.LogWarning("Missing body prefab");
            return;
        }
        if(m_BodyPartCount <= 0)
        {
            enabled = false;
            Debug.LogWarning("Body count is to low, must 1 or greater.");
            return;
        }
        m_Parts.Clear();
        ///Create all the body parts and configure their transform aswell as motor.
        for(int i = 0; i < m_BodyPartCount; i++)
        {
            GameObject obj = (GameObject)Instantiate(m_BodyPrefab, m_Head.position, Quaternion.identity);
            MomBodyPart part = obj.GetComponent<MomBodyPart>();
            if(part == null)
            {
                Destroy(obj);
                continue;
            }
            m_Parts.Add(part);
            part.transform.parent = transform;
            part.motor = this;
            obj.name = obj.name.Remove(obj.name.Length - 7) + "_" + i.ToString();
            //UnityEngine.UI.Text text = obj.GetComponentInChildren<RectTransform>().GetComponentInChildren<UnityEngine.UI.Text>();
            //text.text = i.ToString();
        }
        ///If there was no parts created (because of a bad prefab) then disable the object
        if(m_Parts.Count == 0)
        {
            enabled = false;
            Debug.LogWarning("No body parts created");
            return;
        }
        float initialMovementSpeed = m_Parts[0].movementSpeed;
        for (int i = 0; i < m_Parts.Count; i++)
        {
            if(i == 0)
            {
                m_Parts[i].target = m_Head.transform;
            }
            else
            {
                m_Parts[i].target = m_Parts[i - 1].transform;
                m_Parts[i].movementSpeed = initialMovementSpeed * i * 2;
            }
        }
        if(m_Behaviour != null)
        {
            m_Behaviour.motor = this;
            StartCoroutine(GoalReachedRoutine());
        }

        m_BodyPartCount = m_Parts.Count;
        m_CurrentBodyParts = m_Parts.Count;
    }

    private void OnDisable()
    {
        for(int i = m_Parts.Count - 1; i >= 0; i--)
        {
            Destroy(m_Parts[i]);
        }
        m_Parts.Clear();
    }

    private void Update()
    {
        m_CurrentTime += Time.deltaTime * m_MovementSpeed;

        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    if (m_Parts[3].isAttached)
        //    {
        //        Detach(m_Parts[3]);
        //    }
        //}

        if (m_Behaviour != null)
        {
            m_Behaviour.motor = this;
            m_Behaviour.UpdateState();
        }
        switch(m_State)
        {
            case State.Moving:
                {
                    if (m_CurrentTime > 1.0f)
                    {
                        m_Head.position = Vector3.Lerp(m_CurrentPosition, m_TargetPosition, 1.0f);
                        m_CurrentTime = 0.0f;
                        GoalReached();
                    }
                    else
                    {
                        m_Head.position = Vector3.Lerp(m_CurrentPosition, m_TargetPosition, m_CurrentTime);
                    }
                }
                break;
        }
    }



    public void GoalReached()
    {
        StartCoroutine(GoalReachedRoutine());
    }
    /// <summary>
    /// The routine which runs a delay when a goal has been reached.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoalReachedRoutine()
    {
        if(m_Behaviour != null)
        {
            m_Behaviour.motor = this;
            m_Behaviour.OnGoalReached();
            yield return new WaitForSeconds(m_WaitTime);
            m_CurrentTime = 0.0f;
            m_Behaviour.OnNewGoal();
        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// Generates a random position based on an orientation given. The random position is kept outside of the level bounds.
    /// </summary>
    /// <param name="aOrientation"></param>
    /// <returns></returns>
    public Vector3 GeneratePosition(Orientation aOrientation)
    {
        //y top = 5.24
        //y bottom = -5
        //x left = -8.68
        //x right = 8.6
        switch(aOrientation)
        {  
            case Orientation.Top:
                return new Vector3(Random.Range(-8.6f, 8.6f), Random.Range(m_MinMaxYBounds.x, m_MinMaxYBounds.y), 0.0f);
            case Orientation.Bottom:
                return new Vector3(Random.Range(-8.6f, 8.6f), Random.Range(-m_MinMaxYBounds.x, -m_MinMaxYBounds.y), 0.0f);
            default:
                return new Vector3(Random.Range(-8.6f, 8.6f), Random.Range(10.0f, -10.0f), 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta * 0.65f;
        Gizmos.DrawSphere(m_CurrentPosition, 0.15f);
        Gizmos.DrawLine(m_CurrentPosition, m_TargetPosition);
        Gizmos.DrawSphere(m_TargetPosition, 0.15f);
    }

    /// <summary>
    /// Sets the state to moving.
    /// </summary>
    public void BeginMove()
    {
        m_State = State.Moving;
    }
    /// <summary>
    /// Sets the state to waiting.
    /// </summary>
    public void BeginWait()
    {
        m_State = State.Waiting;
    }

    public void Detach(MomBodyPart aPart)
    {
        for(int i = 0; i < m_Parts.Count; i++)
        {
            if(m_Parts[i] == aPart)
            {
                MomBodyPart next = null;
                MomBodyPart previous = null;
                MomBodyPart current = m_Parts[i];
                if(i > 0)
                {
                    next = m_Parts[i - 1];
                }
                if(i < m_Parts.Count -1)
                {
                    previous = m_Parts[i + 1];
                }

                if(previous != null && next != null)
                {
                    previous.target = next.transform;
                }
                Debug.Log("Detaching " + current.name);
                current.target = null;
                current.OnDetached();
                m_Parts.Remove(current);
                m_Parts.Add(current);
                break;
            }
        }
    }

    public void Destroy(MomBodyPart aPart)
    {
        m_CurrentBodyParts--;
        Cutoff.instance.widthPercent = (float)m_CurrentBodyParts / (float)m_BodyPartCount;
        if(m_CurrentBodyParts == 0)
        {
            GameConditions.instance.OnEnemyDeath();
        }
    }

    public MomBodyPart GetBack(MomBodyPart aSender)
    {
        for (int i = m_Parts.Count - 1; i >= 0; i--)
        {
            if (m_Parts[i].isActive && m_Parts[i] != aSender)
            {
                return m_Parts[i];
            }
        }        
        return null;
    }

    public Vector3 GetRandomPosition()
    {
        for(int i = 0; i < m_Parts.Count; i++)
        {
            int randomNumber = Random.Range(0, m_Parts.Count - 1);
            if(!m_Parts[i].isActive)
            {
                continue;
            }
            return m_Parts[i].transform.position;
        }
        return m_Head.transform.position;
    }

    public void RefreshBehaviour()
    {
        currentBehaviour.OnBehaviourSet();
    }

    public float movementSpeed
    {
        get { return m_MovementSpeed; }
        set { m_MovementSpeed = value; }
    }
    public float waitTime
    {
        get { return m_WaitTime; }
        set { m_WaitTime = value; }
    }
    public Vector3 targetPosition
    {
        get { return m_TargetPosition; }
        set { m_TargetPosition = value; }
    }
    public Vector3 currentPosition
    {
        get { return m_CurrentPosition; }
        set { m_CurrentPosition = value; }
    }
    public float currentTime
    {
        get { return m_CurrentTime; }
        set { m_CurrentTime = value; }
    }

    public bool isWaiting
    {
        get { return m_State == State.Waiting; }
    }
    public bool isMoving
    {
        get { return m_State == State.Moving; }
    }

    public MomBodyBehaviour currentBehaviour
    {
        get { return m_Behaviour; }
        set { m_Behaviour = value; }
    }

}
