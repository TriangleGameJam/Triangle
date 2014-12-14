using UnityEngine;
using System.Collections;

public class SassPositioning : MonoBehaviour 
{
    GameObject _bum;
	// Use this for initialization
	void Start ()
    {
        PlayerController controller = FindObjectOfType<PlayerController>();
        if(controller != null)
        {
            Debug.Log("Gg");
            _bum = controller.gameObject;
        }
        else
        {
            _bum = GameObject.Find("Player");
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = _bum.transform.position;
	}
}
