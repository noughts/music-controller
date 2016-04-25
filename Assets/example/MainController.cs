using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public MusicController musicController;
	public CriAtomSource atomSource;

	// Use this for initialization
	void Start () {
		StartCoroutine (checkLatencyLoop ());
	}

	IEnumerator checkLatencyLoop(){
		CriAtomExLatencyEstimator.InitializeModule ();
		while(true){
			CriAtomExLatencyEstimator.EstimatorInfo info = CriAtomExLatencyEstimator.GetCurrentInfo ();
			print (info.status);
			print (info.estimated_latency);
			if( info.status.Equals (CriAtomExLatencyEstimator.Status.Done) ){
				break;
			}
			yield return null;
		}
		CriAtomExLatencyEstimator.FinalizeModule ();
		print ("遅延推定完了!");
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
