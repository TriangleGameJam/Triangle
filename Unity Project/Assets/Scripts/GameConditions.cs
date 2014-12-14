
using UnityEngine;
using System.Collections;

public class GameConditions : MonoBehaviour
{

    public string m_NextScene = string.Empty;
    public float m_Delay = 1.0f;

    private static GameConditions s_Instance = null;
    public static GameConditions instance
    {
        get { return s_Instance; }
    }
    void Awake()
    {
        s_Instance = this;
    }
    void OnDestroy()
    {
        s_Instance = null;
    }


    public void OnPlayerDeath()
    {
        StartCoroutine(PlayerDeathRoutine());
    }
    public void OnEnemyDeath()
    {
        StartCoroutine(EnemyDeathRoutine());
    }

    IEnumerator PlayerDeathRoutine()
    {
        yield return new WaitForSeconds(m_Delay);
        Application.LoadLevel(Application.loadedLevelName);
    }
    IEnumerator EnemyDeathRoutine()
    {
        yield return new WaitForSeconds(m_Delay);
        Application.LoadLevel(m_NextScene);
    }
}
