using UnityEngine;
using System.Collections;

public class Jayz : MonoAbilityHandler 
{
    [SerializeField]
    private GameObject m_Jayz = null;
    [SerializeField]
    private float m_JayZForce = 10.0f;

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
            //TODO: The actions for Jayz
            GameObject jayz;
            jayz = Instantiate(m_Jayz, transform.position, transform.rotation) as GameObject;
            jayz.transform.rigidbody2D.velocity = transform.TransformDirection(Vector3.forward * 10);
            Projectile projectile = jayz.GetComponent<Projectile>();
            projectile.sender = transform.parent;
            projectile.target = aTarget.transform;
            Rigidbody2D body = jayz.GetComponent<Rigidbody2D>();
            body.AddForce(direction * m_JayZForce, ForceMode2D.Impulse);
        }
    }
}
