using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ImageData : MonoBehaviour {

	// Image Data
	//public readonly static string[] TITLES = new string[] { "Jericho road, near the Samaritan's Inn", "Plain of the Jordan--southeast from the ruins of ancient Jericho", "Moses's outlook over the Valley of Shittim", "Ruth gleaning in the Fields of Boaz", "A Samaritan woman at Jacob's well, Palestine", "On the Road to Jericho--the Parable of the Good Samaritan", "Women grinding at the mill--Palestine", "Samaritan High Priest and Pentateuch roll--Shechem, Palestine", "A Christian girl of Nazareth, Palestine"};
	//public readonly static int[] IMAGE_NUM = new int[] { 1800, 596, 1856, 1875, 603, 536, 606, 605, 620};
	public readonly static string[] TITLES = new string[] { "Jericho road, near the Samaritan's Inn", "Plain of the Jordan--southeast from the ruins of ancient Jericho", "On the Road to Jericho--the Parable of the Good Samaritan", "A Christian girl of Nazareth, Palestine"};
	public readonly static int[] IMAGE_NUM = new int[] { 1800, 596, 536, 620 };
	public readonly static Sprite[] IMAGE_L = new Sprite[IMAGE_NUM.Length];
	public readonly static Sprite[] IMAGE_R = new Sprite[IMAGE_NUM.Length];
	public readonly static AudioClip[] AUDIO_FULL = new AudioClip[IMAGE_NUM.Length];
	public readonly static TextAsset[] SUBTITLES = new TextAsset[IMAGE_NUM.Length];

	// Vars
	private const float _RATE = 44100.0f;
	private static  AudioSource audioSource;
	public static List<float> subtitleTiming = new List<float> ();
	public static List<string> subtitleContent = new List<string> ();
	private static int nextSubtitle = 0;
	private static GUIStyle substitleStyle = new GUIStyle();

	// Singleton
	public static ImageData instance { get; private set; }

	void Awake() 
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	void Start() {
		//gameObject.AddComponent<AudioSource> ();
		audioSource = gameObject.GetComponent<AudioSource>();

		substitleStyle.fixedWidth = Screen.width / 1.5f;
		substitleStyle.wordWrap = true;
		substitleStyle.alignment = TextAnchor.MiddleCenter;
		substitleStyle.normal.textColor = Color.white;
		substitleStyle.fontSize = 18; //Mathf.FloorToInt (Screen.height * 0.0225f);
		
		for (int i = 0; i < IMAGE_NUM.Length; i++) {
			IMAGE_L [i] = Resources.Load<Sprite> ("Images/" + IMAGE_NUM [i] + "/" + IMAGE_NUM [i] + "_L");
			IMAGE_R [i] = Resources.Load<Sprite> ("Images/" + IMAGE_NUM [i] + "/" + IMAGE_NUM [i] + "_R");
			AUDIO_FULL [i] = Resources.Load<AudioClip> ("Audio/" + IMAGE_NUM [i]);
			SUBTITLES [i] = Resources.Load<TextAsset> ("Subtitles/" + IMAGE_NUM [i]);
		}
	}

	public static void BeginDialouge (int i) {
		subtitleTiming = new List<float> ();
		subtitleContent = new List<string> ();

		nextSubtitle = 0;

		// Parse
		string[] fileLines = SUBTITLES[i].text.Split('\n');
		for (int ent = 0; ent < fileLines.Length; ent++) {
			string[] splitTemp = fileLines [ent].Split ('|');
			subtitleTiming.Add (float.Parse(CleanTimeString(splitTemp [0])));
			subtitleContent.Add (splitTemp [1]);
		}
			
		// Play Audio
		audioSource.clip = AUDIO_FULL [i];
		audioSource.Play ();
	}

	private static string CleanTimeString(string timeString) {
		Regex digetOnly = new Regex (@"[^\d+(\.\d+)*$]");
		return digetOnly.Replace (timeString, "");
	}

	void OnGUI() {
		// check for <break/> or invalid next
		if (nextSubtitle >= 0) {
			
			// Make GUI
			GUI.depth = -1001;
			Vector2 size = substitleStyle.CalcSize (new GUIContent ());
			GUI.contentColor = Color.black;
	
			GUI.Label (new Rect (Screen.width / 2 - size.x / 2 + 1, Screen.height / 1.25f - size.y + 1, size.x, size.y), subtitleContent [nextSubtitle], substitleStyle);
			GUI.contentColor = Color.white;
			GUI.Label (new Rect (Screen.width / 2 - size.x / 2, Screen.height / 1.25f - size.y, size.x, size.y), subtitleContent [nextSubtitle], substitleStyle);
		}

		if (nextSubtitle < subtitleContent.Count - 1) {
			if (audioSource.timeSamples/_RATE > subtitleTiming [nextSubtitle]) {
				nextSubtitle++;
			}
		}
	}
}
