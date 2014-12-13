using UnityEngine;
using System.Collections;
using System.IO;

public class MomIncreaseSpeed : MomBodyBehaviour
{
    #region CREATE METHOD
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/AIBehaviour/IncreaseSpeed")]
    private static void CreateBase()
    {
        if (!Directory.Exists(Application.dataPath + "\\AIBehaviours\\"))
        {
            UnityEditor.AssetDatabase.CreateFolder("Assets", "AIBehaviours");
        }
        MomIncreaseSpeed asset = ScriptableObject.CreateInstance<MomIncreaseSpeed>();
        UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/AIBehaviours/NewBehaviour.asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = asset;
    }
#endif
    #endregion

    [SerializeField]
    private float m_Increment = 0.01f;
    [SerializeField]
    private float m_IncrementTime = 0.1f;
    private float m_CurrentTime = 0.0f;

    public override void OnGoalReached()
    {
        base.OnGoalReached();
    }
    public override void OnNewGoal()
    {
        base.OnNewGoal();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        m_CurrentTime += Time.deltaTime;
        if(m_CurrentTime > m_IncrementTime)
        {
            m_CurrentTime = 0.0f;
            movementSpeed += m_Increment;
        }
    }
}

