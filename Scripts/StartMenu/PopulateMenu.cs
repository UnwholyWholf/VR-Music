using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets;

public class PopulateMenu : MonoBehaviour {

    public int menuSpacing = 50;
    public int menuStartHeight = 250;
    public int fontSize = 30;
    public int width = 500;
    public int maxSongs = 8;

	// Use this for initialization
	void Start () {
        string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/../Music");

        int musicIndex = 0;

        for (int i = 0; i < files.Length; i++)
        {
            string filepath = files[i].Replace('\\', '/');
            string[] f = filepath.Split(new char[] { '/' });
            string filename = f[f.Length - 1];
            if (filename.Length < 4 || !filename.Substring(filename.Length - 4).Equals(".wav"))
                continue;

            GameObject newGO = new GameObject("MusicMenuOption" + musicIndex);
            newGO.transform.SetParent(this.transform);

            MusicMenuOption myText = newGO.AddComponent<MusicMenuOption>();
            myText.filepath = filepath;
            myText.text = filename;
            myText.color = Color.black;

            RectTransform trans = newGO.GetComponent<RectTransform>();
            trans.localPosition = new Vector3(0, (musicIndex * -menuSpacing) + menuStartHeight, -1);
            trans.localScale = new Vector3(1, 1, 1);
            trans.sizeDelta = new Vector2(width, menuSpacing);

            BoxCollider bc = newGO.AddComponent<BoxCollider>();
            bc.size = new Vector3(width, menuSpacing, 1);

            myText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            myText.fontSize = fontSize;
            myText.alignment = TextAnchor.MiddleCenter;
            myText.raycastTarget = true;

            musicIndex++;

            if (musicIndex >= maxSongs)
                break;
        }

    }

    // Update is called once per frame
    void Update () {
	    
	}
}
