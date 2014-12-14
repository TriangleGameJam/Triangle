using UnityEngine;
using System.Collections;

public class DaFlip : MonoBehaviour
{
    [SerializeField]
    private float m_FlipDelay = 1.0f;
    [SerializeField]
    private float m_KnockupForce = 10.0f;
    [SerializeField]
    private float m_RotationSpeed = 5.0f;
    [SerializeField]
    private GameObject m_SwappedObject = null;
	// Use this for initialization
	void Start () 
    {
        StartCoroutine(FlipRoutine());
	}
	
	IEnumerator FlipRoutine()
    {
        yield return new WaitForSeconds(m_FlipDelay);
        rigidbody2D.AddForce(Vector2.up * m_KnockupForce, ForceMode2D.Impulse);
        rigidbody2D.angularVelocity = m_RotationSpeed;
        yield return new WaitForSeconds(m_FlipDelay*5.0f);
        GameObject obj = (GameObject)Instantiate(m_SwappedObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
