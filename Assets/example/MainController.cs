using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public MusicController musicController;
	public CriAtomSource atomSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onStartButtonTap(){
		musicController.play ();
	}


	public void playSound(){
		atomSource.Play ();
	}

}
