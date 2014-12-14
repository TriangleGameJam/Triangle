using UnityEngine;
using System.Collections;

public class StoryScript : MonoBehaviour {
    public const int TIME_TO_FADE = 1;

    [SerializeField]
    private GameObject fade;
    private float fadeTime;

    [SerializeField]
    private GameObject family;

    private bool advance;
    private bool showDialog;
    private bool showTutorial;
    private bool levelStarted;

	void Start () {
        advance = false;
        showDialog = false;
        showTutorial = true;
	}
	
	void Update () {
        if (Time.time - fadeTime >= TIME_TO_FADE)
            fade.SetActiveRecursively(false);

        if (advance)
        {
            fade.SetActiveRecursively(true);
            family.SetActiveRecursively(true);
            fadeTime = Time.time;
            showDialog = advance = false;
            collider2D.isTrigger = false;
        }
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        showDialog = true;
        showTutorial = false;
    }

    public void OnGUI()
    {
        if (showDialog)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 150, 100));

            GUI.Box(new Rect(10, 10, 100, 90), "Enter House?");

            if (GUI.Button(new Rect(20, 40, 80, 20), "Yes"))
            {
                advance = true;
                levelStarted = true;
                showTutorial = false;
            }

            if (GUI.Button(new Rect(20, 70, 80, 20), "No"))
            {
                advance = false;
                showDialog = false;
                showTutorial = true;
            }

            GUI.EndGroup();
        }
        if (showTutorial)
        {
            GUI.BeginGroup(new Rect(0, 0, 230, 310));

            GUI.Box(new Rect(10, 10, 220, 300), "Controls");

            GUI.Box(new Rect(20, 40, 200, 20), "Shoulder Shrug - Q");
            GUI.Box(new Rect(20, 70, 200, 20), "Sass Blast - W");
            GUI.Box(new Rect(20, 100, 200, 20), "Whateva Wave - E");
            GUI.Box(new Rect(20, 130, 200, 20), "Slouch - R");
            GUI.Box(new Rect(20, 160, 200, 20), "Jay-Z - W, E, E, R, Q, Q");
            GUI.Box(new Rect(20, 190, 200, 20), "Table Flip - W, W, E, Q, R, Q");
            GUI.Box(new Rect(20, 220, 200, 20), "I'm not Listening - E, Q, R, Q");
            GUI.Box(new Rect(20, 250, 200, 20), "Left and Right arrows to move");
            GUI.Box(new Rect(20, 280, 200, 20), "Spacebar to jump");

            GUI.EndGroup();
        }
    }
}
