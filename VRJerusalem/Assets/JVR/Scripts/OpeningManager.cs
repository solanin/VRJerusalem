using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager : MonoBehaviour {

	private AudioClip AudioIntro;
	private TextAsset SubtitleIntro;
	private bool isPlaying;

	// Use this for initialization
	void Start () {
		AudioIntro = Resources.Load<AudioClip> ("Audio/Audio/Intro");
		SubtitleIntro = Resources.Load<TextAsset> ("Subtitles/Intro");
		isPlaying = false;
	}

	void Update() {
		// Start on press of screen
		if (Input.GetMouseButtonDown(0) && !isPlaying)
		{
			isPlaying = true;
			DialougeManager.BeginDialouge(SubtitleIntro, AudioIntro);
		} else if (Input.GetMouseButtonDown(0) && isPlaying) {
			Application.LoadLevel ("main");
		}

		// Move on when done
		if (!DialougeManager.isPlaying() && isPlaying) {
			Application.LoadLevel ("main");
		}
	}
}
