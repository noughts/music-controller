using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour {

	public MusicController musicController;

	// Use this for initialization
	void Start () {
		musicController.onBeatChangedEvent.AddListener (onBeatChanged);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onBeatChanged(){
		float rnd = Random.Range (-1f, 1f);
		this.transform.localPosition = new Vector3 (rnd,rnd,rnd);
	}
}
