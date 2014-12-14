using UnityEngine;
using System.Collections;

public class SassAttack : MonoBehaviour {
    float _zRotSpeed = 4f;
    public bool isRight = false;
    float _lifetime = 1f;
    float _timeAlive = 0.0f;
	// Use this for initialization
	void Start () {
	    if (isRight)
        {
            _zRotSpeed *= -1;
        }
	}
	
	// Update is called once per frame
	void Update () {        
        _timeAlive += Time.deltaTime;
        float newZRot = transform.rotation.z + _zRotSpeed;
        transform.Rotate(new Vector3(0, 0, _zRotSpeed));

        if (_timeAlive > _lifetime)
        {
            Destroy(gameObject);
        }

	}
}
