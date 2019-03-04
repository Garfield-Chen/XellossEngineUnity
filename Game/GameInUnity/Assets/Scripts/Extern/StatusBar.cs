using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AndroidStatusBar.dimmed = true;
        AndroidStatusBar.statusBarState = AndroidStatusBar.States.TranslucentOverContent;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
