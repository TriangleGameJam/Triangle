using UnityEngine;
using System.Collections;
using System.IO;

public class MomSquareBehaviour : MomBodyBehaviour
{
    #region CREATE METHOD
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/AIBehaviour/Square")]
    private static void CreateBase()
    {
        if (!Directory.Exists(Application.dataPath + "\\AIBehaviours\\"))
        {
            UnityEditor.AssetDatabase.CreateFolder("Assets", "AIBehaviours");
        }
        MomSquareBehaviour asset = ScriptableObject.CreateInstance<MomSquareBehaviour>();
        UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/AIBehaviours/NewBehaviour.asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = asset;
    }
#endif
    #endregion
    [SerializeField]
    private float m_DistanceInset = 1.0f;
    [SerializeField]
    private int m_Lap = 0;
    private int m_Hits = 0;

    [SerializeField]
    private float m_Increment = 0.01f;
    [SerializeField]
    private float m_IncrementTime = 0.1f;
    private float m_CurrentTime = 0.0f;


    public override void UpdateState()
    {
        base.UpdateState();
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime > m_IncrementTime)
        {
            m_CurrentTime = 0.0f;
            movementSpeed += m_Increment;
            movementSpeed = Mathf.Clamp(movementSpeed, 0.0f, 2.0f);
        }
    }


    //y top = 5.24
    //y bottom = -5
    //x left = -8.68
    //x right = 8.6
    public override void GeneratePosition()
    {
        Vector3 origin = Vector3.zero;
        Vector3 direction = Vector3.zero;

        switch (m_Orientation)
        {
            case Orientation.TopLeft:
                origin = new Vector3(-8.6f, 5.0f, 0.0f);
                m_Orientation = Orientation.TopRight;
                break;
            case Orientation.TopRight:
                origin = new Vector3(8.6f, 5.0f, 0.0f);
                m_Orientation = Orientation.BottomRight;
                break;
            case Orientation.BottomRight:
                origin = new Vector3(8.6f, -5.0f, 0.0f);
                m_Orientation = Orientation.BottomLeft;
                break;
            case Orientation.BottomLeft:
                origin = new Vector3(-8.6f, -5.0f, 0.0f);
                m_Orientation = Orientation.TopLeft;
                break;
        }

        direction = (Vector3.zero - origin).normalized;

        currentPosition = targetPosition;
        targetPosition = origin + direction * m_DistanceInset * (m_Lap + 1);
        m_Hits++;
        if(m_Hits > 3)
        {
            m_Hits = 0;
            m_Lap++;
        }
        if(m_Lap > 5)
        {
            m_Lap = 0;
        }
    }

    private void ResetBehaviour()
    {
        m_Lap = 0;
        m_Hits = 0;
        m_Orientation = Orientation.TopLeft;
    }

    public override void OnBehaviourSet()
    {
        
    }
}
