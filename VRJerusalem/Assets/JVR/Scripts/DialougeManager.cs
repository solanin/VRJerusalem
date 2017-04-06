using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DialougeManager : MonoBehaviour {
	// Vars
	private const float _RATE = 44100.0f;
	private static  AudioSource audioSource;
	public static List<float> subtitleTiming = new List<float> ();
	public static List<string> subtitleContent = new List<string> ();
	private static string displaySubtitle = "";
	private static int nextSubtitle = 0;
	private static TextMesh subtitle;

	// Singleton
	public static DialougeManager instance { get; private set; }

	void Awake() 
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}


	void Start() {
		gameObject.AddComponent<AudioSource> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		subtitle = GameObject.Find ("Subtitle").GetComponent<TextMesh> ();
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
		displaySubtitle = subtitleContent[0];

		// Play Audio
		audioSource.clip = Data.AUDIO_FULL [i];
		audioSource.Play ();
	}

	private static string CleanTimeString(string timeString) {
		Regex digetOnly = new Regex (@"[^\d+(\.\d+)*$]");
		return digetOnly.Replace (timeString, "");
	}

	void Update() {
		if (nextSubtitle > 0 && audioSource.isPlaying) {
			subtitle.text = displaySubtitle;
		} else if (!audioSource.isPlaying) {
			subtitle.text = "";
		}

		if (nextSubtitle < subtitleContent.Count) {
			if (audioSource.timeSamples/_RATE > subtitleTiming [nextSubtitle]) {
				displaySubtitle = subtitleContent [nextSubtitle];
				nextSubtitle++;
			}
		}
	}
}
