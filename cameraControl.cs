using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour
{
	public GameObject camera1;
	public GameObject camera2;
		// Use this for initialization
	void Start ()
		{
		camera1.SetActive (true);
		if (camera2 != null)
			camera2.SetActive (false);
		}
	
		// Update is called once per frame
		void Update ()
		{
		if (Input.GetKey (KeyCode.Alpha1)) {
			camera1.SetActive(true);
			camera1.SetActive(false);
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			camera1.SetActive(false);
			camera1.SetActive(true);
		}

	}

}