using UnityEngine;
using System.Collections;

public class LaserBeamMotor : MonoBehaviour {

    [SerializeField]
    private float m_Translation = 0.5f;
    [SerializeField]
    private float m_OrbitSpeed = 5.0f;
    private float m_CurrentRotation = 0.0f;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //transform.rotation = Quaternion.Euler(0.0f, -90.0f, m_CurrentRotation);
        transform.position = transform.parent.position + Quaternion.Euler(0.0f,0.0f,m_CurrentRotation) * Vector3.up * m_Translation;

        Vector3 direction = (transform.position - transform.parent.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        m_CurrentRotation += Time.deltaTime * m_OrbitSpeed;
        m_CurrentRotation = Utilities.ClampAngle(m_CurrentRotation);
	}
}
