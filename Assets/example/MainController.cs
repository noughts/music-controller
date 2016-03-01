using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public MusicController musicController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onStartButtonTap(){
		musicController.gameObject.SetActive (true);
	}
}
