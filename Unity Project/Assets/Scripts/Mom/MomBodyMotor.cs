using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// The main controller of the mothers head.
/// </summary>
public class MomBodyMotor : MonoBehaviour 
{
    /// <summary>
    /// How many body parts to create initially.
    /// </summary>
    [SerializeField]
    private int m_BodyPartCount = 0;
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
    private Vector3 m_LastPosition = Vector3.zero;
    /// <summary>
    /// A list of body parts the motor controls
    /// </summary>
    private List<MomBodyPart> m_Parts = new List<MomBodyPart>();
    /// <summary>
    /// The orientation to generate a position for.
    /// </summary>
    private Orientation m_OrientationTarget = Orientation.Top;
    private State m_State = State.Waiting;

    private Vector2 m_MinMaxYBounds = new Vector2(10.0f, 20.0f);
    
    private enum State
    {
        Waiting,
        Moving
    }

    private void OnEnable()
    {
        ///Check requirements
        if(m_Head == null)
        {
            enabled = false;
            return;
        }
        if(m_BodyPrefab == null)
        {
            enabled = false;
            return;
        }
        if(m_BodyPartCount <= 0)
        {
            enabled = false;
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
            obj.name = obj.name.Remove(obj.name.Length - 7) + "_" + i.ToString();
        }
        ///If there was no parts created (because of a bad prefab) then disable the object
        if(m_Parts.Count == 0)
        {
            enabled = false;
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
        StartCoroutine(GoalReachedRoutine());
    }

    private void Update()
    {
        m_CurrentTime += Time.deltaTime * m_MovementSpeed;
        switch(m_State)
        {
            case State.Moving:
                {
                    if (m_CurrentTime > 1.0f)
                    {
                        m_Head.position = Vector3.Lerp(m_LastPosition, m_TargetPosition, 1.0f);
                        StartCoroutine(GoalReachedRoutine());
                    }
                    else
                    {
                        m_Head.position = Vector3.Lerp(m_LastPosition, m_TargetPosition, m_CurrentTime);
                    }
                }
                break;
            case State.Waiting:

                break;
        }
        
    }
    /// <summary>
    /// The routine which runs a delay when a goal has been reached.
    /// </summary>
    /// <returns></returns>
    private IEnumerator<YieldInstruction> GoalReachedRoutine()
    {
        m_State = State.Waiting;
        yield return new WaitForSeconds(m_WaitTime);
        GenerateNextPosition();
        m_State = State.Moving;
        m_CurrentTime = 0.0f;
    }
    /// <summary>
    /// Generates the next position for the head to move to.
    /// The rest of the body parts should follow the head.
    /// </summary>
    private void GenerateNextPosition()
    {
        m_CurrentTime = 0.0f;
        m_LastPosition = m_TargetPosition;
        if(m_OrientationTarget == Orientation.Top)
        {
            m_OrientationTarget = Orientation.Bottom;
        }
        else
        {
            m_OrientationTarget = Orientation.Top;
        }
        m_TargetPosition = GeneratePosition(m_OrientationTarget);
    }

    /// <summary>
    /// Generates a random position based on an orientation given. The random position is kept outside of the level bounds.
    /// </summary>
    /// <param name="aOrientation"></param>
    /// <returns></returns>
    private Vector3 GeneratePosition(Orientation aOrientation)
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
        Gizmos.DrawSphere(m_LastPosition, 0.15f);
        Gizmos.DrawLine(m_LastPosition, m_TargetPosition);
        Gizmos.DrawSphere(m_TargetPosition, 0.15f);
    }

    public float currentTime
    {
        get { return m_CurrentTime; }
        set { m_CurrentTime = value; }
    }

}
