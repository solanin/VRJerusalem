using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class DialougeManager : MonoBehaviour {
	// Vars
	private const float _RATE = 44100.0f;
	private const int MAX_LINE_CHAR = 25;
	private static  AudioSource audioSource;
	public static List<float> subtitleTiming = new List<float> ();
	public static List<string> subtitleContent = new List<string> ();
	private static string displaySubtitle = "";
	private static int nextSubtitle = 0;
	private static TextMesh subtitleL;
	private static TextMesh subtitleR;

	// Singleton
	public static DialougeManager instance { get; private set; }

	void Awake() 
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		//DontDestroyOnLoad (gameObject);
	}


	void Start() {
		gameObject.AddComponent<AudioSource> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		subtitleL = GameObject.Find ("SubtitleL").GetComponent<TextMesh> ();
		subtitleR = GameObject.Find ("SubtitleR").GetComponent<TextMesh> ();
	}

	public static bool isPlaying() {
		return audioSource.isPlaying;
	}

	public static void BeginDialouge (int i) {
		subtitleTiming = new List<float> ();
		subtitleContent = new List<string> ();
		nextSubtitle = 0;

		// Parse
		string[] fileLines = Data.SUBTITLES[i].text.Split('\n');
		for (int ent = 0; ent < fileLines.Length; ent++) {
			string[] splitTemp = fileLines [ent].Split ('|');
			subtitleTiming.Add (float.Parse(CleanTimeString(splitTemp [0])));
			subtitleContent.Add (splitTemp [1]);
		}

		// Set
		displaySubtitle = FormatString(subtitleContent[0]);

		// Play Audio
		audioSource.clip = Data.AUDIO_FULL [i];
		audioSource.Play ();
	}

	public static void BeginDialouge (TextAsset text, AudioClip sound) {
		subtitleTiming = new List<float> ();
		subtitleContent = new List<string> ();
		nextSubtitle = 0;

		// Parse
		string[] fileLines = text.text.Split('\n');
		for (int ent = 0; ent < fileLines.Length; ent++) {
			string[] splitTemp = fileLines [ent].Split ('|');
			subtitleTiming.Add (float.Parse(CleanTimeString(splitTemp [0])));
			subtitleContent.Add (splitTemp [1]);
		}

		// Set
		displaySubtitle = FormatString(subtitleContent[0]);

		// Play Audio
		audioSource.clip = sound;
		audioSource.Play ();
	}

	private static string CleanTimeString(string timeString) {
		Regex digetOnly = new Regex (@"[^\d+(\.\d+)*$]");
		return digetOnly.Replace (timeString, "");
	}

	void Update() {
		if (nextSubtitle > 0 && audioSource.isPlaying) {
			subtitleL.text = displaySubtitle;
			subtitleR.text = displaySubtitle;
		} else if (!audioSource.isPlaying) {
			subtitleL.text = "";
			subtitleR.text = "";
		}

		if (nextSubtitle < subtitleContent.Count) {
			if (audioSource.timeSamples/_RATE > subtitleTiming [nextSubtitle]) {
				displaySubtitle = FormatString(subtitleContent [nextSubtitle]);
				nextSubtitle++;
			}
		}
	}

	static string FormatString (string text) {
		int charCount = 0;
		string[] words = text.Split(" "[0]); //Split the string into seperate words
		string result = "";

		for (var index = 0; index < words.Length; index++) {

			string word = words [index].Trim();

			if (index == 0) {
				result = words [0];
			} 
			else if (index > 0 ) {
				charCount += word.Length + 1; //+1, because we assume, that there will be a space after every word
				if (charCount <= MAX_LINE_CHAR) {
					result += " " + word;
				}
				else {
					charCount = 0;
					result += "\n " + word;
				}
			}
		}

		return result;
	}
}
