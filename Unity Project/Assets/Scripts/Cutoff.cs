using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutoff : MonoBehaviour 
{
    private static Cutoff s_Instance = null;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float m_WidthPercent = 1.0f;
    Image m_Image = null;
	// Use this for initialization
	void Awake () 
    {
        s_Instance = this;
        
	}
    void Start()
    {
        m_Image = GetComponent<Image>();
    }
    void OnDestroy()
    {
        s_Instance = null;
    }
	
	// Update is called once per frame
	void Update () 
    {
        m_Image.material.SetFloat("_CutoffX", m_WidthPercent);
        m_Image.material.SetColor("_Color", m_Image.color);
	}

    public float widthPercent
    {
        get { return m_WidthPercent; }
        set { m_WidthPercent = Mathf.Clamp01(value); }
    }
    public static Cutoff instance
    {
        get { return s_Instance; }
    }
}
