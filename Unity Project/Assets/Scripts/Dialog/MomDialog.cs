using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dialog;

public class MomDialog : MonoBehaviour
{
    public bool _showMenu = false;
    bool _alreadyShowed = false;

    void OnGUI()
    {
        GUI.skin.box.wordWrap = true;
        if (_showMenu && !_alreadyShowed)
        {
            List<string> dialogOptions = DialogOptions.MomDialog();
            string boxOption = dialogOptions[0];
            GUI.Box(new Rect(10, 10, 450, 150), boxOption);

            if (GUI.Button(new Rect(50, 60, 350, 20), dialogOptions[1]))
            {
                Debug.Log("First button pressed");
                //GetComponent<SisterAI>()._isBattling = true;
                _alreadyShowed = true;
            }

            // Make the second button.
            if (GUI.Button(new Rect(50, 100, 350, 20), dialogOptions[2]))
            {
                Debug.Log("Second button pressed");
                //GetComponent<SisterAI>()._isBattling = true;
                _alreadyShowed = true;
            }

            // Make the second button.
            if (GUI.Button(new Rect(50, 140, 350, 20), dialogOptions[3]))
            {
                Debug.Log("Third button pressed");
                //GetComponent<SisterAI>()._isBattling = true;
                _alreadyShowed = true;
            }
        }
    }
}