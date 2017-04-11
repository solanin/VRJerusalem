﻿using UnityEngine;
using System.Collections;

public class ZoomManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Zoom(true);
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			Zoom(false);
		}
	}

	public void Zoom(bool zoomIn)
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("zoom");
        foreach (GameObject g in temp)
		{
			if (zoomIn) g.transform.localScale *= 2;
			else g.transform.localScale /= 2;
        }
    }
}