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
        transform.Rotate(0.0f, 0.0f, m_OrbitSpeed * Time.deltaTime);
	}
}
