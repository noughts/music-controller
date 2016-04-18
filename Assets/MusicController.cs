using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(LowLatencyAudioSource))]
public class MusicController : MonoBehaviour {

	public readonly UnityEvent onBeatChangedEvent = new UnityEvent();// ビートが変わったタイミングのイベント
	public readonly UnityEvent onMiddleOfBeatEvent = new UnityEvent();// ビートの中心に来たタイミングのイベント

	[Tooltip("音楽のテンポを調べて入力してください")]
	public int tempo = 120;

	[Tooltip("AudioSource.timeを使うのが正確そうだが、エディタ上でステップ実行する際に進まないので、デバッグ用にタイマーを使ったモードに変更します。")]
	public bool useSystemTimer = false;


	float _prevFramePositionAtBeat;// 前のフレームの、ビート内での位置パーセンテージ 0.0~1.0
	float _secOfBeat;
	float _currentPositionAtBeat;
	LowLatencyAudioSource audioSource;

	private int _beatCount;
	public int beatCount { get { return _beatCount; } }
	private float startTime;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<LowLatencyAudioSource>();
	}


	// Update is called once per frame
	void Update () {
		if( audioSource.isPlaying == false ){
			return;
		}
		_currentPositionAtBeat = (getCurrentTime () % _secOfBeat) / _secOfBeat;

		// ビートが変わった判定
//		print (_prevFramePositionAtBeat + " => " + _currentPositionAtBeat);
		if( _currentPositionAtBeat < _prevFramePositionAtBeat ){
//			print ("beat changed!!!!!!");
			_beatCount++;
			onBeatChangedEvent.Invoke ();
		}

		// ビート中心の判定
		if( _prevFramePositionAtBeat < 0.5 && _currentPositionAtBeat >= 0.5 ){
			onMiddleOfBeatEvent.Invoke ();
		}

		_prevFramePositionAtBeat = _currentPositionAtBeat;
	}


	#region Public

	public void play(){
		startTime = Time.time;
		_beatCount = 0;
		_secOfBeat = 60f / tempo;
		_prevFramePositionAtBeat = 9999;// スタート直後にonBeatChangedEventが発生するように設定
		audioSource.Play ();
	}

	/// 1beatの長さを秒で返す
	public float getDurationOf1BeatInSeconds(){
		return 1000f / (tempo / 60f) / 1000f;
	}


	/// ちょうどビートのうえかどうか
	public bool checkIfJustAtBeat(){
		return _currentPositionAtBeat == 0;
	}

	/// ビートが始まったばかりかどうか
	public bool checkIfNearStartOfBeat(){
		if( checkIfJustAtBeat() ){
			return false;
		}
		return _currentPositionAtBeat < 0.3;
	}

	/// ビートが終わりに近いかどうか
	public bool checkIfNearEndOfBeat(){
		if( checkIfJustAtBeat() ){
			return false;
		}
		return _currentPositionAtBeat > 0.7;
	}

	/// ビートの近くかどうかを返す
	public bool checkIfNearBeat(){
		if( checkIfJustAtBeat() ){
			return true;
		}
		if( checkIfNearStartOfBeat () ){
			return true;
		}
		if( checkIfNearEndOfBeat () ){
			return true;
		}
		return false;
	}

	/// ビート内での位置パーセンテージ 0.0~1.0 を返す
	public float getCurrentPositionAtBeat(){
		return _currentPositionAtBeat;
	}

	public float getCurrentTime(){
		if( useSystemTimer ){
			return Time.time - startTime;
		} else {
			return audioSource.time;
		}

	}

	#endregion
}
