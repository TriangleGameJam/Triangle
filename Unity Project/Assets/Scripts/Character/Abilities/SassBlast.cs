using UnityEngine;
using System.Collections;

public class SassBlast : MonoAbilityHandler 
{

    [SerializeField]
    private GameObject _sassAttack = null;

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
            Vector3 pos = transform.position;
            Quaternion rot = Quaternion.identity;

            Instantiate(_sassAttack, pos, rot);
            ScreenShake();
        }
    }
}
