using UnityEngine;
using System.Collections;

public class MomBossFight : MonoBehaviour
{
    #region SINGLETON
    private static MomBossFight s_Instance = null;
    private static MomBossFight instance
    {
        get { return s_Instance;}
    }


    private static bool SetInstance(MomBossFight aInstance)
    {
        if(s_Instance == null)
        {
            s_Instance = aInstance;
            return true;
        }
        return false;
    }
    private static void DestroyInstance(MomBossFight aInstance)
    {
        if(s_Instance == aInstance)
        {
            s_Instance = null;
        }
    }

    #endregion



    [SerializeField]
    private MomBodyMotor m_Motor = null;
    [SerializeField]
    private float[] m_EventTimes = new float[] { 20.0f, 30.0f, 20.0f };
    [SerializeField]
    private MomBodyBehaviour[] m_EventBehaviours = null;


    private int m_EventIndex = 0;
    private float m_EventTime = 0.0f;

    [SerializeField]
    private GameObject m_TearPrefab = null;
    [SerializeField]
    private float m_TearSpawnTime = 5.0f;
    [SerializeField]
    private float m_TearSpawnDecayTime = 2.0f;
    [SerializeField]
    private float m_MinimumSpawnTime = 1.0f;
	
    

    void OnEnable()
    {
        if(m_EventTimes.Length == 0)
        {
            enabled = false;
            return;
        }
        if(m_EventBehaviours.Length == 0)
        {
            enabled = false;
            return;
        }
        NextEvent();
    }

    void Awake()
    {
        if(!SetInstance(this))
        {
            Destroy(this);
            return;
        }
    }
    void OnDestroy()
    {
        DestroyInstance(this);
    }
	
	void Update ()
    {
        m_EventTime += Time.deltaTime;
        if(m_EventTime > m_EventTimes[m_EventIndex])
        {
            NextEvent();
            if(m_Motor.currentBehaviour.GetType() == typeof(MomSquareBehaviour))
            {
                Debug.Log("Start Tear Drop");
                StartCoroutine(TearDropRoutine());
            }
        }
	}

    IEnumerator TearDropRoutine()
    {
        while (m_Motor.currentBehaviour.GetType() == typeof(MomSquareBehaviour))
        {
            yield return new WaitForSeconds(m_TearSpawnTime);
            if (m_Motor.currentBehaviour.GetType() == typeof(MomSquareBehaviour))
            {
                m_TearSpawnTime = Mathf.Clamp(m_TearSpawnTime - m_TearSpawnDecayTime, m_MinimumSpawnTime, float.MaxValue);
                Vector3 spawnPoint = m_Motor.GetRandomPosition();
                Instantiate(m_TearPrefab, spawnPoint, Quaternion.identity);
            }
        }
        Debug.Log("End Tear Drop");
    }
    void NextEvent()
    {
        m_EventTime = 0.0f;

        m_Motor.currentBehaviour = m_EventBehaviours[m_EventIndex];
        m_Motor.RefreshBehaviour();
        m_EventIndex++;
        ///Reset when we reach the end
        if (m_EventIndex >= Mathf.Min(m_EventBehaviours.Length, m_EventTimes.Length))
        {
            m_EventIndex = 0;
        }
    }
}
