using UnityEngine;
using System.Collections;

public class LoadAudio : MonoBehaviour {

    public string audioSourceTag = "AudioSource";

    private AudioSource[] audioSources;
    private bool isPaused = false;

	// Use this for initialization
	void Start () {
        GameObject[] sources = GameObject.FindGameObjectsWithTag(audioSourceTag);
        audioSources = new AudioSource[sources.Length];
        for (int i = 0; i < sources.Length; i++)
            audioSources[i] = sources[i].GetComponent<AudioSource>();

        StartCoroutine(LoadSong(Variables.filepath));
	}


    IEnumerator LoadSong(string filepath)
    {
        WWW www = new WWW("file://" + filepath);

        yield return www;

        AudioClip clip = www.GetAudioClip(true);

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = clip;
            audioSource.mute = false;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7)) //Start
        {
            if (isPaused)
                foreach (AudioSource audioSource in audioSources)
                    audioSource.UnPause();
            else
                foreach (AudioSource audioSource in audioSources)
                    audioSource.Pause();

            isPaused = !isPaused;
        }
    }
}
