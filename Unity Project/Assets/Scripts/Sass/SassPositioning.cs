using UnityEngine;
using System.Collections;

public class SassPositioning : MonoBehaviour {
    GameObject _bum;
	// Use this for initialization
	void Start () {
        _bum = GameObject.Find("BumParent");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = _bum.transform.position;
	}
}
