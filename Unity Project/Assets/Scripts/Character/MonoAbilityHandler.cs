using UnityEngine;
using System.Collections;

public class MonoAbilityHandler : MonoBehaviour , IAbilityHandler
{
    [SerializeField]
    protected AbilityType m_AbilityHandled = AbilityType.None;
    protected PlayerController m_PlayerController = null;

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
    }

    public virtual void OnExecuteAbility(GameObject aTarget, AbilityType aAbility)
    {
        
    }
}
