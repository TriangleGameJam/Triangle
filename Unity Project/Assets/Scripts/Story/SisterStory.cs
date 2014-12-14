using UnityEngine;
using System.Collections;

public class SisterStory : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    private bool showDialog;
    private string fight = "Sister_Boss_Fight";
    
    void Start()
    {

    }

    void Update()
    {
        if (showDialog)
            (player.GetComponent(typeof(PlayerStory)) as PlayerStory).enabled = false;
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

            GUI.Box(new Rect(10, 10, 100, 120), "MAKE ME FOOD!");

            if (GUI.Button(new Rect(20, 40, 80, 20), "Yes"))
            {
                showDialog = false;
            }
            if (GUI.Button(new Rect(20, 70, 80, 20), "No"))
            {
                Application.LoadLevel(fight);
            }
            if (GUI.Button(new Rect(20, 100, 80, 20), "..."))
            {
                Application.LoadLevel(fight);
            }

            GUI.EndGroup();
        }
    }
}
