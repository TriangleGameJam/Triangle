using UnityEngine;
using System.Collections.Generic;

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
    /// The position the character is currently moving towards.
    /// </summary>
    [SerializeField]
    private Vector3 m_TargetPosition = Vector3.zero;
    /// <summary>
    /// The last position the character was at.
    /// </summary>
    [SerializeField]
    private Vector3 m_LastPosition = Vector3.zero;
    /// <summary>
    /// A list of body parts the motor controls
    /// </summary>
    [SerializeField]
    private List<MomBodyPart> m_Parts = new List<MomBodyPart>();
    /// <summary>
    /// The head of the motor
    /// </summary>
    [SerializeField]
    private Transform m_Head = null;
#if UNITY_EDITOR
    /// <summary>
    /// These attributes are for debugging only. NOTE: CurrentTime should only be within values of 0 to 1
    /// </summary>
    [Range(0.0f,1.0f)]
    [SerializeField]
#endif
    private float m_CurrentTime = 0.0f;
    /// <summary>
    /// How fast the character moves.
    /// </summary>
    [SerializeField]
    private float m_MovementSpeed = 1.0f;
    [SerializeField]
    private float m_Distance = 0.5f;

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
            part.motor = this;
            part.transform.parent = transform;
            obj.name = obj.name.Remove(obj.name.Length - 7) + "_" + i.ToString();
        }
        ///If there was no parts created (because of a bad prefab) then disable the object
        if(m_Parts.Count == 0)
        {
            enabled = false;
            return;
        }


        for (int i = 1; i < m_Parts.Count; i++)
        {
            m_Parts[i - 1].next = m_Parts[i];
        }

        ///Update all the positions
        m_Parts[0].UpdatePositions(m_Head.position + m_Head.up * 1.0f, m_Head.position);
    }

    private void Update()
    {
        m_CurrentTime += Time.deltaTime * m_MovementSpeed;
        if(m_CurrentTime > 1.0f)
        {
            m_CurrentTime = 1.0f;
            m_Head.position = Vector3.Lerp(m_LastPosition, m_TargetPosition, m_CurrentTime);
            GenerateNextPosition();
        }
        else
        {
            m_Head.position = Vector3.Lerp(m_LastPosition, m_TargetPosition, m_CurrentTime);
        }
        
    }

    private void GenerateNextPosition()
    {
        m_CurrentTime = 0.0f;
        m_Parts[0].UpdatePositions(m_LastPosition, m_TargetPosition);
        m_LastPosition = m_TargetPosition;
        Vector3 offset = new Vector3(Random.Range(-m_Distance, m_Distance), Random.Range(-m_Distance, m_Distance), 0.0f);
        m_TargetPosition = m_TargetPosition + m_Head.rotation * offset;
        
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
