using UnityEngine;
using System.Collections;

public class AudienceAnimation : MonoBehaviour {

    // Use this for initialization
    private string[] anims = {"idle", "applause", "applause2", "celebration", "celebration2", "celebration3"};

    void Start() {

        Animation[] audienceAnims = gameObject.GetComponentsInChildren<Animation>();
        foreach (Animation anim in audienceAnims)
        {
            string currAnim = anims[Random.Range(0, 5)];
            anim.wrapMode = WrapMode.Loop;
            anim.CrossFade(currAnim);
            anim[currAnim].time = Random.Range(0f, 5f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
