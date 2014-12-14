using UnityEngine;
using System.Collections;

public class SassImproved : MonoBehaviour {
    float _lifetime = .6f;
    float _timeAlive = 0.0f;
    float _changeColorTime = 0.0f;

	// Use this for initialization
	void Start () {
        float rColor = Random.Range(0.0f, 1.0f);
        float gColor = Random.Range(0.0f, 1.0f);
        float bColor = Random.Range(0.0f, 1.0f);
        renderer.material.color = new Color(rColor, gColor, bColor);
	}
	
	// Update is called once per frame
	void Update () {
        _changeColorTime += Time.deltaTime;
        _timeAlive += Time.deltaTime;
        if (_changeColorTime > .1f)
        {
            Debug.Log("This works");
            float rColor = Random.Range(0.0f, 1.0f);
            float gColor = Random.Range(0.0f, 1.0f);
            float bColor = Random.Range(0.0f, 1.0f);
            renderer.material.color = new Color(rColor, gColor, bColor);
        }
	}
}
