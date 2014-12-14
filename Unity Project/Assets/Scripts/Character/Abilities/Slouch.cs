using UnityEngine;
using System.Collections;

public class Slouch : MonoAbilityHandler 
{
    private void Start()
    {
        InitPlayerController();
        if (m_PlayerController != null)
        {
            m_PlayerController.Register(this);
        }
    }
    private void OnDestroy()
    {
        if (m_PlayerController != null)
        {
            m_PlayerController.Unregister(this);
        }
    }

    public override void OnExecuteAbility(GameObject aTarget, AbilityType aAbility)
    {
        if (m_AbilityHandled == aAbility)
        {
            //TODO: The actions for Slouch
            if ((Time.time * 1000) - m_PlayerController.buffTime < PlayerController.BUFF_COOLDOWN)
            {
                UnityEngine.Debug.Log("Buff is on cooldown");
            }
            else
            {
                UnityEngine.Debug.Log("Buff has been applied");
                m_PlayerController.buffTime = Time.time * 1000;
                m_PlayerController.damageBuff += PlayerController.DAMAGE_BUFF;
            }
        }
    }
}
