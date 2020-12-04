using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// credit: https://blog.silentkraken.com/2010/04/06/audiomanager/

public class SoundManager : MonoBehaviour {

	// Random pitch adjustment range.
	public float LowPitchRange = .90f;
	public float HighPitchRange = 1.1f;

	// Singleton instance.
	public static SoundManager Instance = null;

	// Initialize the singleton instance.
	private void Awake() {
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null) {
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this) {
			Destroy(gameObject);
		}
	}

	public AudioSource Play(AudioClip clip, Transform emitter) {
		return Play(clip, emitter, 1f, 1f);
	}

	public AudioSource Play(AudioClip clip, Transform emitter, float volume) {
		return Play(clip, emitter, volume, 1f);
	}

	/// <summary>
	/// Plays a sound by creating an empty game object with an AudioSource
	/// and attaching it to the given transform (so it moves with the transform). Destroys it after it finished playing.
	/// </summary>
	/// <param name="clip"></param>
	/// <param name="emitter"></param>
	/// <param name="volume"></param>
	/// <param name="pitch"></param>
	/// <returns></returns>
	public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch) {
		//Create an empty game object
		GameObject go = new GameObject("Audio: " + clip.name);
		go.transform.position = emitter.position;
		go.transform.parent = emitter;

		//Create the source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play();
		Destroy(go, clip.length);
		return source;
	}

	public AudioSource Play(AudioClip clip, Vector3 point) {
		return Play(clip, point, 1f, 1f);
	}

	public AudioSource Play(AudioClip clip, Vector3 point, float volume) {
		return Play(clip, point, volume, 1f);
	}

	/// <summary>
	/// Plays a sound at the given point in space by creating an empty game object with an AudioSource
	/// in that place and destroys it after it finished playing.
	/// </summary>
	/// <param name="clip"></param>
	/// <param name="point"></param>
	/// <param name="volume"></param>
	/// <param name="pitch"></param>
	/// <returns></returns>
	public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch) {
		//Create an empty game object
		GameObject go = new GameObject("Audio: " + clip.name);
		go.transform.position = point;

		//Create the source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play();
		Destroy(go, clip.length);
		return source;
	}

	// Play a random clip from an array, and randomize the pitch slightly.
	public void RandomSoundEffect(AudioClip[] clips, Vector3 point) {
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
		this.Play(clips[randomIndex], point, 0.5f, randomPitch);
	}

}