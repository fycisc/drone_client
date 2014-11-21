using System;
using UnityEngine;
using System.Collections;


public class generateTerrain: MonoBehaviour{

	public long sizeToLoad = 1000;
	public Transform target;

	private static float MaxHeight = 100;
	private int count = 0;
	private int countMax = 100;
//	private GameObject[] terrainManger = new GameObject[countMax];

	void Start(){

	}

	void Update(){

	}



	private void OnImageryLoaded(Texture2D tex2d, Terrain targetTerrain){
		TerrainData terrainData = targetTerrain.terrainData;
		SplatPrototype splat = new SplatPrototype ();
		splat.texture = tex2d;
		splat.tileSize = new Vector2 (terrainData.size.x, terrainData.size.z);
		terrainData.splatPrototypes = new SplatPrototype[]{splat};
		targetTerrain.Flush ();
	}

	private void OnImageryRemoved(Terrain targetTerrain){
		targetTerrain.terrainData.splatPrototypes = new SplatPrototype[]{new SplatPrototype()};
		targetTerrain.Flush ();
	}

	IEnumerator loadTerrain(int i, int j){
		TerrainData terrainData = new TerrainData(){
			baseMapResolution = 512, heightmapResolution = 32,
			size = new Vector3(publicvar.SizeX, MaxHeight, publicvar.SizeZ)
		};
		GameObject terrainGO = Terrain.CreateTerrainGameObject(terrainData);
		terrainGO.transform.position = new Vector3 (i, 0, j);
		
		yield return 0;
	}

	

}


//terrainGOs[col, cow] = terrainGO;