using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour 
{
    [SerializeField]
    private string m_SceneToLoad = string.Empty;
    [SerializeField]
    private Animator[] m_Animators = null;
    [SerializeField]
    private Animator[] m_OptionsAnimators = null;
    [SerializeField]
    private bool m_MainMenuIn = false;
    [SerializeField]
    private bool m_OptionsIn = false;

    public void Update()
    {
        m_OptionsIn = !m_MainMenuIn;
        foreach (Animator animator in m_Animators)
        {
            animator.SetBool("MainMenuIn", m_MainMenuIn);
        }
        foreach( Animator animator in m_OptionsAnimators)
        {
            animator.SetBool("OptionsIn", m_OptionsIn);
        }
    }

    public void Play()
    {
        Application.LoadLevel(m_SceneToLoad);
    }
    public void Options()
    {
        m_MainMenuIn = false;
        
    }
    public void Back()
    {
        m_MainMenuIn = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ShakeAmount(float aValue)
    {
        Debug.Log("Value " + aValue);
        GameOptions.shakeAmount = aValue;
    }
}
