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
	private static GUIStyle substitleStyle = new GUIStyle();

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

		substitleStyle.fixedWidth = Screen.width / 1.5f;
		substitleStyle.wordWrap = true;
		substitleStyle.alignment = TextAnchor.MiddleCenter;
		substitleStyle.normal.textColor = Color.white;
		substitleStyle.fontSize = 18; //Mathf.FloorToInt (Screen.height * 0.0225f);
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
		displaySubtitle = subtitleContent[0];

		// Play Audio
		audioSource.clip = sound;
		audioSource.Play ();
	}

	private static string CleanTimeString(string timeString) {
		Regex digetOnly = new Regex (@"[^\d+(\.\d+)*$]");
		return digetOnly.Replace (timeString, "");
	}

	void OnGUI() {
		if (nextSubtitle > 0 && audioSource.isPlaying) {

			// Make GUI
			GUI.depth = -1001;
			Vector2 size = substitleStyle.CalcSize (new GUIContent ());
			GUI.contentColor = Color.black;

			GUI.Label (new Rect (Screen.width / 2 - size.x / 2 + 1, Screen.height / 1.25f - size.y + 1, size.x, size.y), displaySubtitle, substitleStyle);
			GUI.contentColor = Color.white;
			GUI.Label (new Rect (Screen.width / 2 - size.x / 2, Screen.height / 1.25f - size.y, size.x, size.y), displaySubtitle, substitleStyle);
		}

		if (nextSubtitle < subtitleContent.Count) {
			if (audioSource.timeSamples/_RATE > subtitleTiming [nextSubtitle]) {
				displaySubtitle = subtitleContent [nextSubtitle];
				nextSubtitle++;
			}
		}
	}

	public static bool isPlaying() {
		return audioSource.isPlaying;
	}
}
