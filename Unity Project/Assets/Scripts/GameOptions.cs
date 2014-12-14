using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour
{
    private static GameOptions s_Instance = null;
    public static GameOptions instance
    {
        get { return s_Instance; }
    }
    [Range(0.0f,1.0f)]
    [SerializeField]
    private float m_ShakeAmount = 1.0f;

    void Awake()
    {
        s_Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void OnDestroy()
    {
        s_Instance = null;
    }

    public static float shakeAmount
    {
        get { return s_Instance == null ? 0.0f : s_Instance.m_ShakeAmount; }
        set { if (s_Instance != null) { s_Instance.m_ShakeAmount = Mathf.Clamp01(value); } }
    }
}
