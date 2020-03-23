using UnityEngine;
using System.Collections;
using Assets;

public class LoadAudioOnFocus : MonoBehaviour {

    private RaycastMenuSelect selector;
    private AudioSource[] audioSources;
    private RaycastText lastHit = null;

    public bool stopPlayingIfNoFocus = true;
    public string audioSourceTag = "AudioSource";

    // Use this for initialization
    void Start () {
         selector = GameObject.Find("CenterEyeAnchor").GetComponent<RaycastMenuSelect>();
        GameObject[] sources = GameObject.FindGameObjectsWithTag(audioSourceTag);
        audioSources = new AudioSource[sources.Length];
        for (int i = 0; i < sources.Length; i++)
            audioSources[i] = sources[i].GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastText hit = selector.currentOption;

        if (lastHit != null && hit == lastHit)
            return;

        lastHit = hit;

        if (hit is MusicMenuOption)
        {
            string filepath = (hit as MusicMenuOption).filepath;
            StopAllCoroutines();
            StartCoroutine(LoadSong(filepath));
        }
        else if (stopPlayingIfNoFocus)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = true;
            }
        }
            
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
}
