using UnityEngine;
using System.Collections;

public class ImageData : MonoBehaviour {

	// Image data
	//public readonly static string[] TITLES = new string[] { "Jericho road, near the Samaritan's Inn", "Plain of the Jordan--southeast from the ruins of ancient Jericho", "Moses's outlook over the Valley of Shittim", "Ruth gleaning in the Fields of Boaz", "A Samaritan woman at Jacob's well, Palestine", "On the Road to Jericho--the Parable of the Good Samaritan", "Women grinding at the mill--Palestine", "Samaritan High Priest and Pentateuch roll--Shechem, Palestine", "A Christian girl of Nazareth, Palestine"};
	//public readonly static int[] IMAGE_NUM = new int[] { 1800, 596, 1856, 1875, 603, 536, 606, 605, 620};
	public readonly static string[] TITLES = new string[] { "Jericho road, near the Samaritan's Inn", "Plain of the Jordan--southeast from the ruins of ancient Jericho", "On the Road to Jericho--the Parable of the Good Samaritan", "A Christian girl of Nazareth, Palestine"};
	public readonly static int[] IMAGE_NUM = new int[] { 1800, 596, 536, 620 };

	public readonly static Sprite[] IMAGE_L = new Sprite[IMAGE_NUM.Length];
	public readonly static Sprite[] IMAGE_R = new Sprite[IMAGE_NUM.Length];

	public readonly static AudioClip[] AUDIO_FULL = new AudioClip[IMAGE_NUM.Length];

	// Singleton
	/*public static ImageData instance = null;

	void Awake() 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		Init();
	}*/

	void Start() {
		for (int i = 0; i < IMAGE_NUM.Length; i++) {
			IMAGE_L [i] = Resources.Load<Sprite> ("Images/" + IMAGE_NUM [i] + "/" + IMAGE_NUM [i] + "_L");
			IMAGE_R [i] = Resources.Load<Sprite> ("Images/" + IMAGE_NUM [i] + "/" + IMAGE_NUM [i] + "_R");
			AUDIO_FULL [i] = Resources.Load<AudioClip> ("Audio/" + IMAGE_NUM [i]);
		}
	}
}
