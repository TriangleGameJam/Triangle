using UnityEngine;
using System.IO;
using System.Collections;

public class MomBodyBehaviour : ScriptableObject
{
    #region CREATE METHOD
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/AIBehaviour/Base")]
    private static void CreateBase()
    {
        if (!Directory.Exists(Application.dataPath + "\\AIBehaviours\\"))
        {
            UnityEditor.AssetDatabase.CreateFolder("Assets", "AIBehaviours");
        }
        MomBodyBehaviour asset = ScriptableObject.CreateInstance<MomBodyBehaviour>();
        UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/AIBehaviours/NewBehaviour.asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = asset;
    }
#endif
    #endregion


    private MomBodyMotor m_Motor = null;
    [SerializeField]
    private float m_WaitDelay = 1.0f;
    [SerializeField]
    private Orientation m_Orientation = Orientation.Top;

    /// <summary>
    /// A callback for when the object gets created.
    /// </summary>
    protected virtual void OnEnable()
    { 

    }
    /// <summary>
    /// A callback for when the object gets destroyed
    /// </summary>
    protected virtual void OnDestroy()
    {

    }

    /// <summary>
    /// A callback for what the AI should do while updating.
    /// </summary>
    public virtual void UpdateState()
    {
        if(isMoving)
        {
            if (currentTime > 1.0f)
            {
                m_Motor.GoalReached();
            }
        }
        else if(isWaiting)
        {

        }

    }
    /// <summary>
    /// A callback for when the AI ceaches their goal.
    /// </summary>
    public virtual void OnGoalReached()
    {
        m_Motor.BeginWait();
    }
    /// <summary>
    /// A yield statement for right after the AI reaches their goal
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Yield()
    {
        yield return new WaitForSeconds(m_WaitDelay);
    }
    /// <summary>
    /// A callback for when a new goal should be made on the AI.
    /// </summary>
    public virtual void OnNewGoal()
    {
        m_Motor.BeginMove();
        GeneratePosition();
    }

    /// <summary>
    /// A base class method which generates a random position based on orientation.
    /// </summary>
    public virtual void GeneratePosition()
    {
        m_Motor.currentTime = 0.0f;
        m_Motor.currentPosition = m_Motor.targetPosition;
        if (m_Orientation == Orientation.Top)
        {
            m_Orientation = Orientation.Bottom;
        }
        else
        {
            m_Orientation = Orientation.Top;
        }
        m_Motor.targetPosition = m_Motor.GeneratePosition(m_Orientation);
    }
    /// <summary>
    /// An accessor to the motor component.
    /// </summary>
    public MomBodyMotor motor
    {
        get { return m_Motor; }
        set { m_Motor = value; }
    }
    /// <summary>
    /// A safe accessor to the current position
    /// </summary>
    public Vector3 currentPosition
    {
        get { return m_Motor == null ? Vector3.one : m_Motor.currentPosition; }
        set { if(m_Motor != null ){m_Motor.currentPosition = value;} }
    }
    /// <summary>
    /// A safe accessor to the target position
    /// </summary>
    public Vector3 targetPosition
    {
        get { return m_Motor == null ? Vector3.one : m_Motor.targetPosition; }
        set { if (m_Motor != null) { m_Motor.targetPosition = value; } }
    }
        
    public float movementSpeed
    {
        get { return m_Motor == null ? 0.0f : m_Motor.movementSpeed; }
        set { if (m_Motor != null) { m_Motor.movementSpeed = value; } }
    }

    public float currentTime
    {
        get { return m_Motor.currentTime; }
    }

    public bool isMoving
    {
        get { return m_Motor != null ? m_Motor.isMoving : false; }
    }
    public bool isWaiting
    {
        get { return m_Motor != null ? m_Motor.isWaiting : false; }
    }
    
}
