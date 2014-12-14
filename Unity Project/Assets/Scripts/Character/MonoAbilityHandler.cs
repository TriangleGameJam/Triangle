using UnityEngine;
using System.Collections;

public class MonoAbilityHandler : MonoBehaviour , IAbilityHandler
{
    [SerializeField]
    protected AbilityType m_AbilityHandled = AbilityType.None;
    [SerializeField]
    protected float m_ScreenShakeTime = 0.1f;
    protected PlayerController m_PlayerController = null;
    protected CameraShake m_CameraShake = null;

    protected void InitPlayerController()
    {
        m_PlayerController = GetComponentInParent<PlayerController>();
        if(m_PlayerController == null)
        {
            m_PlayerController = GetComponent<PlayerController>();
            if(m_PlayerController == null)
            {
                m_PlayerController = GetComponentInChildren<PlayerController>();
            }
        }

        if(Camera.main != null)
        {
            m_CameraShake = Camera.main.GetComponent<CameraShake>();
        }
    }

    protected void ScreenShake()
    {
        if(m_CameraShake != null && m_ScreenShakeTime != 0.0f)
        {
            m_CameraShake.Shake(m_ScreenShakeTime);
        }
    }

    public virtual void OnExecuteAbility(GameObject aTarget, AbilityType aAbility)
    {
        
    }
}
