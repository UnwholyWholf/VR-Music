using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    Transform myTrans;

    public float rotateRate = 1f;

	// Use this for initialization
	void Start () {
        myTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        myTrans.Rotate(Vector3.up, rotateRate);
	}
}
