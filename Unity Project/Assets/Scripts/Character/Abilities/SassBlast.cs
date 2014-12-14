using UnityEngine;
using System.Collections;

public class SassBlast : MonoAbilityHandler 
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
            //TODO: The actions for SassBlast
        }
    }
}
