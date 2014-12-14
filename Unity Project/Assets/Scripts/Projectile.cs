using UnityEngine;
using System.Collections;

public enum DestroyFlags
{
    None,
    Target,
    Collision,
}

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// The one who created the projectile
    /// </summary>
    private Transform m_Sender = null;
    /// <summary>
    /// The target the projectile should hit.
    /// </summary>
    private Transform m_Target = null;
    /// <summary>
    /// Flags to determine what can destroy the projectile.
    /// </summary>
    [SerializeField]
    private DestroyFlags m_DestroyFlags = DestroyFlags.None;
    /// <summary>
    /// The damage the projectile causes
    /// </summary>
    [SerializeField]
    private float m_Damage = 0.0f;
    /// <summary>
    /// How long the projectile will live in the scene for.
    /// </summary>
    [SerializeField]
    private float m_Lifetime = 5.0f;

	// Use this for initialization
	protected virtual void Start () 
    {
        StartCoroutine(LifeRoutine());
	}

    void OnDestroy()
    {
        StopCoroutine(LifeRoutine());
    }
	
	void OnCollisionEnter2D(Collision2D aInfo)
    {
        ///Ignore self
        if(aInfo.collider.transform == m_Sender)
        {
            Debug.Log("Ignoring Selt");
            return;
        }
        switch(m_DestroyFlags)
        {
            case DestroyFlags.Collision:
                Destroy(gameObject);
                break;
            case DestroyFlags.Target:
                if(aInfo.transform == m_Target)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }


    IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(m_Lifetime);
        Destroy(gameObject);
    }

    public Transform sender
    {
        get { return m_Sender; }
        set { m_Sender = value; }
    }
    public Transform target
    {
        get { return m_Target; }
        set { m_Target = value; }
    }
    public DestroyFlags destroyFlags
    {
        get { return m_DestroyFlags; }
        set { m_DestroyFlags = value; }
    }
    public float damage
    {
        get { return m_Damage; }
        set { m_Damage = value; }
    }
}
