using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class TerrainManager : MonoBehaviour
{
	// a manager only manage the tiles around one aircraft

	public static  Dictionary<int[], Terrain> terrains = publicvar.terrains;
	public int maxTileX = 3; // tiles farther than this will be destroyed
	public int maxTileY = 3; // tiles farther than this will be destroyed
	public int basei = publicvar.basei;
	public int basej = publicvar.basej;
	public GameObject plane;
	private bool isUpdating = false;


//---------------------------------------------------------------------------------------

	public Terrain NewTerrainData(TerrainMetaData mdata, TextureData tdata, HeightmapMetaData hdata){
		TerrainData terraindata = new TerrainData ();
		GameObject terrainobject = Terrain.CreateTerrainGameObject (terraindata);
		Terrain terrain = terrainobject.GetComponent<Terrain> ();
		// set some paras of the terrainTile
		_InitTerrain(terrain.terrainData, mdata, hdata);
		// register to the manager
		terrains.Add(new int[]{tdata.i, tdata.j}, terrain );
		// start a coroutine to load the texture
		StartCoroutine(loadTexture(tdata, terrain.renderer));
		// start a coroutine to load the heightmap
		StartCoroutine (loadHeightmap (hdata, terrain.terrainData));

		//TODO
		float[] xz = getPos (tdata.i, tdata.j);
		terrain.transform.position = new Vector3 (xz[0],0,xz[1]);
		return terrain;
	}

	public void DestroyTerrain(int[] ij){
		Terrain terrain = terrains [ij];
		GameObject.DestroyImmediate (terrain.gameObject);
	}
	

	public void StartUpdate(){
		this.isUpdating = true;
		StartCoroutine (Updatemap ());
	}

	public void StopUpdate(){
		this.isUpdating = false;
	}

	public Terrain getTerrain(int i, int j){
		return terrains [new int[]{i,j}];
	}

	public Terrain getActiveTerrain(){
		int[] center = getCurrentTile (plane);
		return terrains [new int[]{center[0],center[1]}];
	}

	public int numTerrain(){
		return terrains.Count;
	}

	public TerrainManager(GameObject plane){
		this.plane = plane;
	}
//----------------------------------------------------------------------------------------

	IEnumerator Updatemap(){
		isUpdating = true;
		while (true) {
			//wait for a second
			yield return new WaitForSeconds(1);
			
			// clear remote tiles
			int[] center = getCurrentTile(plane);
			StartCoroutine(ClearRemoteTiles(center));
			
			//wait for a second 
			yield return new WaitForSeconds(1);
			// flush new tiles
			center = getCurrentTile(plane);
			StartCoroutine(FlushNewTiles(center));
			
			
			if (!isUpdating) break;
		}
		
	}

	IEnumerator ClearRemoteTiles(int[] center){
		foreach (KeyValuePair<int[], Terrain> entry in terrains) {
			int i = entry.Key[0];
			int j = entry.Key[1];
			if (Math.Abs(i-center[0]) > maxTileX || Math.Abs (j-center[1]) > maxTileY){
				DestroyTerrain(entry.Key);
			}
		}
		yield return 0;
	}

	IEnumerator FlushNewTiles(int[] center){
		for (int i = -maxTileX; i <= maxTileX; i++) 
			for (int j = -maxTileY; j <= maxTileY; j++)
			if (!terrains.ContainsKey(new int[]{i,j})) {
				TerrainMetaData mdata = new TerrainMetaData();
				TextureData tdata = new TextureData(i, j);
				HeightmapMetaData hdata = new HeightmapMetaData();
				yield return NewTerrainData(mdata, tdata, hdata);
			}

		yield return 0;
	}


	private void _InitTerrain(TerrainData tdata, TerrainMetaData mdata, HeightmapMetaData hdata){
//		tdata.detailWidth = mdata.width;
//		tdata.detailHeight = mdata.length;
//		tdata.heightmapWidth = hdata.width;
//		tdata.heightmapHeight = hdata.length;
	}

	IEnumerator loadHeightmap(HeightmapMetaData hdata, TerrainData terraindata){
		// TODO
		yield return 0;
	}

	IEnumerator loadTexture(TextureData tdata, Renderer renderer){
		int i = tdata.i;
		int j = tdata.j;
		string path = mappath(i,j);
		FileInfo fi1 = new FileInfo(path);   
		WWW www;
		// file exists. Load from local cache.
		if (fi1.Exists)
		{
			www = new WWW("file://"+path);
			yield return www;
			if (www.error != null )
			{
				print("Error:"+ www.error);
			}
			else
			{
				renderer.material.mainTexture = www.texture;
				www.Dispose();
				www =null;
			}
		}

		// file not exits. load from Internet
		else{
			string url = remotemapurl(i,j,publicvar.zoom);
			www=new WWW(url);
			yield return www;
			Debug.Log(url);
			if (www.error != null ){
				print("Error:"+ www.error);
				yield return www;
			}
			else{
				Debug.Log("Texture Loaded. Total memory" + GC.GetTotalMemory(true));
				if (!Directory.Exists(Application.dataPath+"/map" )){ //判断文件夹是否已经存在
					Directory.CreateDirectory(Application.dataPath+"/map");
				}
				
				Texture2D tmp = new Texture2D(256,256);
				www.LoadImageIntoTexture(tmp);
				try{renderer.material.mainTexture = tmp;}
				catch{
					Debug.Log("MissingReferenceException");
				}
				www.Dispose();
				www =null;
				Debug.Log("Texture Released. Total memory" + GC.GetTotalMemory(true));
			}
		}
	}

	protected string remotemapurl(int i, int j, int zoom){
		return string.Format ("https://api.tiles.mapbox.com/v3/examples.map-qfyrx5r8/{0}/{1}/{2}.jpg",zoom, i, j);
	}
	protected string mappath(int i,int j){
		return "hehe";
	}

	protected float[] getPos(int i, int j){
		float[] xy = new float[]{0f,0f};
		xy [0] = (i - basei) * publicvar.lengthmesh;
		xy [1] = -(j - basej) * publicvar.lengthmesh;
		return xy;
	}

	protected int[] getCurrentTile(GameObject aircraft){
		int[] ij = new int[]{0, 0};
		float x = aircraft.transform.position.x ;
		float z = aircraft.transform.position.z ;
		ij [0] = (int)((x + publicvar.lengthmesh / 2) / publicvar.lengthmesh);
		ij [1] = (int)((z + publicvar.lengthmesh / 2) / publicvar.lengthmesh);
		ij [0] -= basei;
		ij [1] = basej - ij [1];
		return ij;
	}

}

