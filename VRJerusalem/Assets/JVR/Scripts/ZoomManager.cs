using UnityEngine;
using System.Collections;

// No longer used in App
// Zooms in/out of image on Z and X key press

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
