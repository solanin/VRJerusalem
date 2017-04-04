﻿using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {

	private int current;
	private GameObject leftImg;
	private GameObject rightImg;
	private AudioSource	audioSource;

    // Use this for initialization
    void Start () {
		leftImg = GameObject.Find("LeftImg");
		rightImg = GameObject.Find("RightImg");
		audioSource = GameObject.Find("Stereograph").GetComponent<AudioSource>();

		SwapImages (0);
		current = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SwapImages();
        }
	}

	void SwapImages()
	{
		if (current < ImageData.IMAGE_NUM.Length)
		{
			SwapImages (current);
		}

		current++;
		if (current >= ImageData.IMAGE_NUM.Length) {
			current = 0;
		}
	}

	void SwapImages(int i)
	{
		leftImg.GetComponent<SpriteRenderer>().sprite = ImageData.IMAGE_L[i];
		rightImg.GetComponent<SpriteRenderer>().sprite = ImageData.IMAGE_R[i];

		audioSource.clip = ImageData.AUDIO_FULL[i];
		audioSource.Play ();
	}
}
