using UnityEngine;
using System.Collections;

public class TableFlip : MonoAbilityHandler 
{
    [SerializeField]
    private GameObject m_Table = null;
    [SerializeField]
    private float m_TableForce = 0.0f;
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
            Vector2 direction = (aTarget.transform.position - transform.position).normalized;
            //TODO: The actions for TableFlip
            GameObject table;
            table = Instantiate(m_Table, transform.position, transform.rotation) as GameObject;
            Projectile projectile = table.GetComponent<Projectile>();
            projectile.sender = transform.parent;
            projectile.target = aTarget.transform;
            Rigidbody2D body = table.GetComponent<Rigidbody2D>();
            body.AddForce(direction * m_TableForce, ForceMode2D.Impulse);
            ScreenShake();
        }
    }
}
