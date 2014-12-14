using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

    [SerializeField]
    private Transform m_Target = null;
    [SerializeField]
    private SpriteRenderer m_Bounds = null;
    [SerializeField]
    private Vector2 m_Offset = Vector2.zero;
    private Rect m_BoundingRect = new Rect();

    private Vector2 m_ShakeAmount = Vector2.zero;
	// Use this for initialization
	void Start () 
    {
        Vector2 extents = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height,Camera.main.orthographicSize);
        Vector2 boundsSize = m_Bounds.bounds.size;
        m_BoundingRect.x = -boundsSize.x * 0.5f + extents.x;
        m_BoundingRect.width = boundsSize.x * 0.5f - extents.x;
        m_BoundingRect.height = extents.y - boundsSize.y * 0.5f;
        m_BoundingRect.y = boundsSize.y * 0.5f - extents.y;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(m_Target != null)
        {
            Vector3 pos = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
            pos.x = Mathf.Clamp(pos.x, m_BoundingRect.x + m_Offset.x, m_BoundingRect.width + m_Offset.x);
            pos.y = Mathf.Clamp(pos.y, m_BoundingRect.height + m_Offset.y, m_BoundingRect.y + m_Offset.y);
            transform.position = pos;
            transform.position += new Vector3(m_ShakeAmount.x, m_ShakeAmount.y, 0.0f);

        }
	}

    public Vector2 shakeAmount
    {
        get { return m_ShakeAmount; }
        set { m_ShakeAmount = value; }
    }

}
