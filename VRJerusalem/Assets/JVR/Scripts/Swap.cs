using UnityEngine;
using System.Collections;

// Handles user input
// Attached to Left and Right image objects

public class Swap : MonoBehaviour {

	private int current;
	private GameObject leftImg;
	private GameObject rightImg;

    // Use this for initialization
    void Start () {
	    //Find objs
		leftImg = GameObject.Find("LeftImg");
		rightImg = GameObject.Find("RightImg");
	    // Set the scene
		SwapImages (0);
		current = 1;
    }
	
	// Update is called once per frame
	void Update () {k
		// Swap image on clic
        if (Input.GetMouseButtonDown(0))
        {
            SwapImages();
        }
	}

	// Change to next image
	void SwapImages()
	{
		if (current < Data.IMAGE_NUM.Length)
		{
			SwapImages (current);
		}

		current++;
		if (current >= Data.IMAGE_NUM.Length) {
			current = 0;
		}
	}
	
	// Change to a given image
	void SwapImages(int i)
	{
		leftImg.GetComponent<SpriteRenderer>().sprite = Data.IMAGE_L[i];
		rightImg.GetComponent<SpriteRenderer>().sprite = Data.IMAGE_R[i];

		DialougeManager.BeginDialouge(i);
	}
}
