using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextColor : MonoBehaviour {
    float _scrollSpeed = .005f;
    float _lifetime = .6f;
    float _timeAlive = 0.0f;
    float _changeColorTime = 0.0f;

    void Start()
    {
        var collideText = new List<string>
        {
            "FABULOUS",
            "SUGOI~",
            "YASSSS"
        };

        guiText.text = collideText[Random.Range(0, 3)];
    }
    
    void Update()
    {
        _changeColorTime += Time.deltaTime;
        _timeAlive += Time.deltaTime;
        if (_changeColorTime > .1f)
        {
            float rColor = Random.Range(0.0f, 1.0f);
            float gColor = Random.Range(0.0f, 1.0f);
            float bColor = Random.Range(0.0f, 1.0f);
            guiText.color = new Color(rColor, gColor, bColor);
            _changeColorTime = 0;
        }

        if (_timeAlive > _lifetime)
        {
            Destroy(gameObject);
        }

        float newPosY = guiText.transform.position.y + _scrollSpeed;
        guiText.transform.position = new Vector3(guiText.transform.position.x, newPosY, 0);
    }
}
