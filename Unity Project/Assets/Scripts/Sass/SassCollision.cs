using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SassCollision : MonoBehaviour {
    Collider2D _hobo;
    public GameObject _text;

	// Use this for initialization
	void Start () {
        _hobo = GameObject.Find("Player").collider2D;
        Physics2D.IgnoreCollision(collider2D, _hobo);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D()
    {
        Vector3 collidePos = transform.position;
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        Instantiate(_text, collidePos, rot);

        Object.Destroy(gameObject);
    }
}
