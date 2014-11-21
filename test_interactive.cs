using UnityEngine;
using System.Collections;

public class test_interactive : MonoBehaviour
{
		
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

	public void PrintCurrentValue(){
		if (UIProgressBar.current != null)
			Debug.Log(Mathf.RoundToInt(UIProgressBar.current.value*100f) + "%");
	}
}

