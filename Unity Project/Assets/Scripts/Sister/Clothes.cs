using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    GameObject _bum;

    void Start()
    {
        _bum = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}