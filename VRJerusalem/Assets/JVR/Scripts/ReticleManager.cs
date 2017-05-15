using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// No longer used in our app
// Was used for rectile management and to constain recticle to screen

public class ReticleManager : MonoBehaviour {

    [SerializeField]
    private float maxReticleAngle;

	// Use this for initialization
	void Start ()
    {
        if (maxReticleAngle == 0.0f)
            maxReticleAngle = 30.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        Debug.Log(transform.localEulerAngles.y);
		if(Mathf.Abs(transform.localEulerAngles.y) >= maxReticleAngle)
        {
            Debug.Log("Blah");
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                maxReticleAngle * Mathf.Sign(transform.localEulerAngles.y),
                transform.localEulerAngles.z
                );
            Debug.Log(transform.localEulerAngles);
            Debug.Log(transform.rotation);
        }
        */
	}
}
