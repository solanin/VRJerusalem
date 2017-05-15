using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// No longer in use by the app
// Originally used to adjust the left and rigt cameras

public class AdjustVRCameras : MonoBehaviour {

    private bool hasUpdated;
    public Camera mainCamera;

	// Use this for initialization
	void Start () {
        hasUpdated = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasUpdated){
            Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
            for(int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i].name == "Camera Left")
                {
					cameras[i].cullingMask = (1 << LayerMask.NameToLayer("Left"));
					cameras [i].stereoTargetEye = StereoTargetEyeMask.Left;
				}

                if (cameras[i].name == "Camera Right")
                {
					cameras[i].cullingMask = (1 << LayerMask.NameToLayer("Right"));
					cameras[i].stereoTargetEye = StereoTargetEyeMask.Right;
                }
            }
            hasUpdated = true;
        }
	}
}
