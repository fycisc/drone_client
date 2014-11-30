using UnityEngine;
using System.Collections;

public class resize : MonoBehaviour {

	// Use this for initialization
	public GameObject camera;
	public float k=0.01f;

	void Start () {
		float height = camera.transform.position.y;
		transform.localScale = new Vector3(k*height,k*height,k*height);

	}
	
	// Update is called once per frame
	void Update () {
		float height = camera.transform.position.y;
		transform.localScale = new Vector3(k*height,k*height,k*height);
	}
}
