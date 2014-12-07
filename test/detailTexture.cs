using UnityEngine;
using System.Collections;

public class detailTexture : MonoBehaviour
{
	public Terrain terrain= null;
	private TerrainData tdata;
	private string testexture1 = "/Users/feiyicheng/Desktop/drones/oliviaclient/Assets/textures/vlM8iGxAJXSpIyS.png";
	private string testexture2 = "/Users/feiyicheng/Desktop/drones/oliviaclient/Assets/textures/sideTruck.png";

		// Use this for initialization
		void Start ()
		{
		tdata = terrain.terrainData;
		tdata.size = new Vector3 (2000, 1000, 2000);
		float[,] heights = new float[33, 33];
		for (int i = 0; i < 33; i++) {
			for (int j = 0; j < 33; j++) {
				heights[i,j] = 0.25f;
			}
				}
		tdata.SetHeights(0,0,heights);
		Debug.DrawLine (new Vector3 (0, 1000, 0), new Vector3 (0, 0, 0), Color.red);
		}
	
		// Update is called once per frame
		void Update ()
		{
		Debug.DrawLine (Vector3.zero, new Vector3(0,500,0), Color.red);
		}
}

