using UnityEngine;
using System.Collections;

/* This script helps purge the memory when the app/game is not fluent enough
 */

public class MemoryController : MonoBehaviour
{
	public int count;
		// Use this for initialization
		void Start ()
		{
		count = 0;
	
		}
	
		// Update is called once per frame
		void Update ()
		{
//		Debug.Log (System.GC.GetTotalMemory(false));
		if (Time.deltaTime > 0.1f || Time.frameCount % 120 == null) {
			Resources.UnloadUnusedAssets();
			count++;
//			Debug.Log("num called: --- "  + count);
			}
			else if (Time.frameCount%120 == 60) {
				System.GC.Collect();
				}			
		}
}

