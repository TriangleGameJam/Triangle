using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SisterAI : MonoBehaviour 
{
    public bool _isBattling = false;

    float _spawnClotheTimer = 0.0f;
    float _spawnClotheLimit = 4.0f;
    float _throwCellphoneTimer = 0.0f;
    float _throwCellphoneLimit = 2.0f;
    float _bieberRainTimer = 0.0f;
    float _bieberRainLimit = 4.0f;
    float _changePosTimer = 0.0f;
    float _changePosLimit = 4.0f;

    GameObject _hobo;
    GameObject _clotheSpawns;
    GameObject _bieberSpawns;
    GameObject _sisterSpawns;
    public GameObject _clothes;
    public GameObject _cellphone;
    public GameObject _bieber;

	// Use this for initialization
	void Start () 
    {
        _hobo = GameObject.Find("Player");
        _clotheSpawns = GameObject.Find("ClothesPoints");
        _bieberSpawns = GameObject.Find("BieberRainPoints");
        _sisterSpawns = GameObject.Find("SisterPoints");
	}
	
	// Update is called once per frame
	void Update () 
    {
        //GetComponent<SisterDialog>()._showMenu = true;
        
        if (_isBattling)
        {
            _spawnClotheTimer += Time.deltaTime;
            _throwCellphoneTimer += Time.deltaTime;
            _bieberRainTimer += Time.deltaTime;
            _changePosTimer += Time.deltaTime;
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

            if (_changePosTimer > _changePosLimit)
            {
                foreach (Transform t in _sisterSpawns.GetComponentsInChildren<Transform>())
                {
                    //25% chance of changing positions in each transform
                    if (Random.Range(0.0f, 1.0f) < .25f)
                    {
                        transform.position = t.position;
                    }
                }

                _changePosTimer = 0;
            }
        }
	}
}