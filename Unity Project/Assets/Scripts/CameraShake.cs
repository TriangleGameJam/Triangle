using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
    [SerializeField]
    private Vector2 m_ShakeAmount = Vector2.zero;
    private float m_ShakeTime = 0.0f;
    private CameraController m_Controller = null;
	// Use this for initialization
	void Start ()
    {
        m_Controller = GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_ShakeTime -= Time.deltaTime;
	    if(m_ShakeTime > 0.0f)
        {
            m_Controller.shakeAmount = new Vector2(Random.Range(-m_ShakeAmount.x, m_ShakeAmount.x),
                                                    Random.Range(-m_ShakeAmount.y, m_ShakeAmount.y));
        }
        else
        {
            m_Controller.shakeAmount = Vector2.zero;
        }
	}

    public void Shake(float aTime)
    {
        m_ShakeTime = aTime;
    }
    public void Shake(float aTime, Vector2 aAmount)
    {
        m_ShakeTime = aTime;
        m_ShakeAmount = aAmount;
    }

}
