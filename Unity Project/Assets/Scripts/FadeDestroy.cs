using UnityEngine;
using System.Collections;

public class FadeDestroy : MonoBehaviour 
{
    [SerializeField]
    private float m_Lifetime = 2.0f;
    private float m_CurrentTime = float.MaxValue;
	
	// Update is called once per frame
	void Update ()
    {
        if(m_Lifetime  > 0.0f)
        {
            m_Lifetime -= Time.deltaTime;
        }
        
        if(m_Lifetime < 0.0f)
        {
            m_Lifetime = 0.0f;
            m_CurrentTime = 1.0f;
        }
        m_CurrentTime -= Time.deltaTime;
        if(renderer != null)
        {
            Color color = renderer.material.color;
            color.a = Mathf.Clamp01(m_CurrentTime);
            renderer.material.color = color;
        }
        
        if(m_CurrentTime < 0.0f)
        {
            Destroy(gameObject);
        }


	}
}
