using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BumMockup : MonoBehaviour 
{
    float _moveSpeed = 5.0f;
    public GameObject _sass;
    public GameObject _sassAttack;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        bool moveRight = Input.GetKey(KeyCode.RightArrow);
        bool moveLeft = Input.GetKey(KeyCode.LeftArrow);

        if (moveRight)
        {
            rigidbody2D.AddForce(Vector2.right * _moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (moveLeft)
        {
            rigidbody2D.AddForce(-Vector2.right * _moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            
        //    Quaternion rot = new Quaternion(0, 0, 0, 0);
        //    Vector2 speed = new Vector2((pos.x - gameObject.rigidbody2D.position.x),
        //                                (pos.y - gameObject.rigidbody2D.position.y));

        //    GameObject proj = (GameObject)Instantiate(_sass, new Vector3(gameObject.rigidbody2D.position.x, gameObject.rigidbody2D.position.y, 0f), rot);
                                                
        //    speed.Normalize();
        //    proj.rigidbody2D.AddForce(speed * 5.0f, ForceMode2D.Impulse);
                        
        //}

        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 pos = transform.position;
            Quaternion rot = Quaternion.identity;

            Instantiate(_sassAttack, pos, rot);
        }
	}
}
