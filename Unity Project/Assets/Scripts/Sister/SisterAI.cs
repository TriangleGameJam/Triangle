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
    float _throwCellphoneTimer = 0.0f;
    float _throwCellphoneLimit = 2.0f;
    float _bieberRainTimer = 0.0f;
    float _bieberRainLimit = 2.0f;

    GameObject _hobo;
    GameObject _clotheSpawns;
    GameObject _bieberSpawns;
    public GameObject _clothes;
    public GameObject _cellphone;
    public GameObject _bieber;

	// Use this for initialization
	void Start () 
    {
        _hobo = GameObject.Find("BumParent");
        _clotheSpawns = GameObject.Find("ClothesPoints");
        _bieberSpawns = GameObject.Find("BieberRainPoints");
        _currentChangeSpeed = Random.Range(.2f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Mathf.Abs(transform.position.magnitude - _hobo.transform.position.magnitude) < 1)
        {
            GetComponent<SisterDialog>()._showMenu = true;
        }
        
        if (_isBattling)
        {
            Vector3 newPos = new Vector3(transform.position.x + _moveSpeed, transform.position.y, transform.position.z);
            transform.position = newPos;

            _speedChangeCount += Time.deltaTime;
            _spawnClotheTimer += Time.deltaTime;
            _throwCellphoneTimer += Time.deltaTime;
            _bieberRainTimer += Time.deltaTime;

            if (_speedChangeCount > _currentChangeSpeed)
            {
                _moveSpeed *= -1;
                _currentChangeSpeed = Random.Range(.2f, 1.0f);
                _speedChangeCount = 0;
            }

            if (_spawnClotheTimer > _spawnClotheLimit)
            {
                foreach (Transform t in _clotheSpawns.GetComponentInChildren<Transform>())
                {
                    //25% chance of spawning at a certain point
                    if (Random.Range(0.0f, 1.0f) < .25f)
                    {
                        Instantiate(_clothes, t.position, t.rotation);
                    }
                }

                _spawnClotheTimer = 0;
            }

            if (_throwCellphoneTimer > _throwCellphoneLimit)
            {
                Instantiate(_cellphone, transform.position, transform.rotation);
                _throwCellphoneTimer = 0;
            }

            if (_bieberRainTimer > _bieberRainLimit)
            {
                foreach (Transform t in _bieberSpawns.GetComponentsInChildren<Transform>())
                {
                    //25% chance of spawning a bieber head
                    if (Random.Range(0.0f, 1.0f) < .25f)
                    {
                        Instantiate(_bieber, t.position, t.rotation);
                    }
                }

                _bieberRainTimer = 0;
            }
        }
	}
}