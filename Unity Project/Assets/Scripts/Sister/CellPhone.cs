using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CellPhone : MonoBehaviour
{
    GameObject _bum;
    float _xVelocity;
    float _yVelocity;
    float _rotationSpeed = 4.0f;

    void Start()
    {
        _bum = GameObject.Find("Player");
        Vector3 diff = _bum.transform.position - transform.position;
        diff.Normalize();

        _xVelocity = diff.x * .1f;
        _yVelocity = diff.y * .1f;
    }

    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x + _xVelocity,
                                     transform.position.y + _yVelocity,
                                     transform.position.z);
        transform.position = newPos;

        transform.Rotate(new Vector3(0, 0, _rotationSpeed));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);            
        }
    }
}