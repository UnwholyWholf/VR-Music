using UnityEngine;
using System.Collections;

public class DJGuyAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Animation djAnim = gameObject.GetComponentInChildren<Animation>();
        string currAnim = "celebration2";
        djAnim.wrapMode = WrapMode.Loop;
        djAnim.CrossFade(currAnim);
        djAnim[currAnim].time = Random.Range(0f, 10f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
