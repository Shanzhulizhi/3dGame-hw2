using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class UserGUI : MonoBehaviour {
	private UserAction action;
	public int status = 0;
	GUIStyle style;
	GUIStyle buttonStyle;

	void Start() {
		action = Director.getInstance ().currentSceneController as UserAction;

		style = new GUIStyle();
		style.fontSize = 40;
        style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;
        buttonStyle.fontStyle = FontStyle.BoldAndItalic;

    }
	void OnGUI() {
		if (status == 1) {
			GUI.Label(new Rect(Screen.width/2-90, Screen.height/2-170, 90, 40), "Game Over!", style);
			if (GUI.Button(new Rect(Screen.width/2-140, Screen.height/4, 130, 60), "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		} else if(status == 2) {
			GUI.Label(new Rect(Screen.width/2-90, Screen.height/2-170, 90, 40), "Success!", style);
			if (GUI.Button(new Rect(Screen.width/2-140, Screen.height/4, 130, 60), "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		}
	}
}
