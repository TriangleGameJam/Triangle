using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SisterAI : MonoBehaviour 
{
    float _moveSpeed = .05f;
    float _speedChangeCount = 0.0f;
    float _currentChangeSpeed;
    public bool _isBattling = false;

    float _spawnClotheTimer = 0.0f;
    float _spawnClotheLimit = 4.0f;

    GameObject _hobo;
    GameObject _clotheSpawns;

	// Use this for initialization
	void Start () 
    {
        _hobo = GameObject.Find("BumParent");
        _clotheSpawns = GameObject.Find("ClothesPoints");
        _currentChangeSpeed = Random.Range(.2f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Mathf.Abs(transform.position.magnitude - _hobo.transform.position.magnitude) < 1)
        {
            GetComponent<DialogMockup>()._showMenu = true;
        }
        
        if (_isBattling)
        {
            Vector3 newPos = new Vector3(transform.position.x + _moveSpeed, transform.position.y, transform.position.z);
            transform.position = newPos;

            _speedChangeCount += Time.deltaTime;
            _spawnClotheTimer += Time.deltaTime;

            if (_speedChangeCount > _currentChangeSpeed)
            {
                _moveSpeed *= -1;
                _currentChangeSpeed = Random.Range(.2f, 1.0f);
                _speedChangeCount = 0;
            }

            if (_spawnClotheTimer > _spawnClotheLimit)
            {
               // GameObject[] spawns = _clotheSpawns.GetComponentsInChildren<GameObject>();
            }
        }
	}
}