using UnityEngine;
using System.Collections;

public class DadHealth : MonoBehaviour
{
    [SerializeField]
    private float m_Health = 1000.0f;
    private float m_CurrentHealth = 1000.0f;
    private DadMotor m_Motor = null;
    private void Start()
    {
        m_Motor = GetComponent<DadMotor>();
        m_CurrentHealth = m_Health;
        UpdateHealthBar();
    }

	void OnTriggerEnter2D(Collider2D aInfo)
    {
        //Debug.Log(aInfo.name);
        Projectile proj = aInfo.transform.GetComponent<Projectile>();
        if(proj != null && proj.sender != transform)
        {
            m_CurrentHealth -= proj.damage;
            UpdateHealthBar();
            if(m_Motor != null)
            {
                m_Motor.Interrupt();
            }
            if (m_CurrentHealth < 0.0f)
            {
                gameObject.SetActive(false);
                GameConditions.instance.OnEnemyDeath();
            }
        }
        else if(proj != null)
        {
            Debug.LogWarning(proj.sender.name);
        }
    }

    void UpdateHealthBar()
    {
        Cutoff.instance.widthPercent = m_CurrentHealth / m_Health;
    }
}
