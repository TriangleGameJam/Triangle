using UnityEngine;
using System.Collections;

public class DadStory : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    private bool showDialog;
    private string fight = "Dad_Boss_Fight";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (showDialog)
            (player.GetComponent( typeof(PlayerStory) ) as PlayerStory).enabled = false;
        else
            (player.GetComponent(typeof(PlayerStory)) as PlayerStory).enabled = true;
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        showDialog = true;
    }

    public void OnGUI()
    {
        if (showDialog)
        {
            GUI.BeginGroup(new Rect(0, 0, 110, 130));

            GUI.Box(new Rect(10, 10, 100, 120), "I PITY DA FOO!");

            if (GUI.Button(new Rect(20, 40, 80, 20), "Yes"))
            {
                Application.LoadLevel(fight);
            }
            if (GUI.Button(new Rect(20, 70, 80, 20), "No"))
            {
                showDialog = false;
            }
            if (GUI.Button(new Rect(20, 100, 80, 20), "..."))
            {
                Application.LoadLevel(fight);
            }

            GUI.EndGroup();
        }
    }
}
