using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class testScrollrec : MonoBehaviour
{

//	private ScrollRect srec = GameObject.Find("pitch").GetComponent<ScrollRect>();
	private Image pitchimage ;

		// Use this for initialization
		void Start ()
		{
			pitchimage = GameObject.Find("pitchimage").GetComponent<Image>();
		}
	
		// Update is called once per frame
		void Update ()
		{
		if (Input.GetKey(KeyCode.W)) {
			pitchimage.transform.position += new Vector3(0,1,0);
				}
		else if (Input.GetKey(KeyCode.S)) {
			pitchimage.transform.position += new Vector3(0,-1,0);
		}	
		}
}

