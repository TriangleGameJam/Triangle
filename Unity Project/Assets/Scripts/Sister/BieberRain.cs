using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BieberRain : MonoBehaviour
{
    public Sprite _bieber1;
    public Sprite _bieber2;
    public Sprite _bieber3;
    public Sprite _bieber4;
    
    float _rainLifetime = 10.0f;
    float _lifetimeCounter = 0.0f;
    float _rotationSpeed = 4.0f;

    float _ySpeed = -0.05f;

    void Start()
    {
        List<Sprite> sprites = new List<Sprite>
        {
            _bieber1,
            _bieber2,
            _bieber3,
            _bieber4
        };

        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 4)];        
    }

    void Update()
    {
        _lifetimeCounter += Time.deltaTime;
        if (_lifetimeCounter > _rainLifetime)
        {
            Destroy(gameObject);
        }

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + _ySpeed, transform.position.z);
        transform.Rotate(new Vector3(0, 0, _rotationSpeed));
        transform.position = newPos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}