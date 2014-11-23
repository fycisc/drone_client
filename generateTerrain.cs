using UnityEngine;
using System.Collections;

public class generateTerrain : MonoBehaviour
{
	public int  heightmapres = 33;
	public int sizex = 200;
	public int sizez = 200;
	public int detailres = 1024;

		// Use this for initialization
	IEnumerator Start ()
	{
		TerrainData tdata = new TerrainData ();
		tdata.heightmapResolution = heightmapres;
		Debug.Log (tdata.heightmapScale); //= new Vector3 (sizex, 200, sizez);

		WWW www1 = new WWW ("http://api.tiles.mapbox.com/v2/examples.map-qfyrx5r8/116.3,39.9,15/256x256.png");
		yield return www1;

		var splats = new SplatPrototype[1];
		splats [0] = new SplatPrototype ();
		splats [0].texture = new Texture2D(256,256);
		www1.LoadImageIntoTexture (splats [0].texture);
		splats [0].tileOffset = new Vector2 (0, 0);
		splats [0].tileSize = new Vector2 (sizex, sizez);

		float[,] heights = new float[heightmapres,heightmapres];
		for (int i = 0; i < heightmapres; i++) {
			for (int j = 0; j < heightmapres; j++) {
				//heights[i,j] = 0.1f* Random.Range(0,1f);
				heights[i,j] = 1f;
			}
		}
		tdata.SetHeights (0, 0, heights);

		tdata.splatPrototypes = splats;
		tdata.size = new Vector3 (sizex, 200, sizez);

		GameObject newterrainobject = Terrain.CreateTerrainGameObject (tdata);
		Terrain newterrain = newterrainobject.GetComponent<Terrain> ();
		newterrain.transform.position = new Vector3 (0, 0, 0);

	}
		// Update is called once per frame
		void Update ()
		{
	
		}
}

