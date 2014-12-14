using UnityEngine;
using System.Collections;

public class WalkAway : MonoAbilityHandler
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
            //TODO: The actions for WalkAway
            if ((Time.time * 1000) - m_PlayerController.dodgeTime < PlayerController.DODGE_TIME)
            {
                UnityEngine.Debug.Log("Already dodging");
            }
            else
            {
                UnityEngine.Debug.Log("WALKAWAY");
                m_PlayerController.dodgeTime = Time.time;
                m_PlayerController.isDodging = true;
            }
        }
    }
}
