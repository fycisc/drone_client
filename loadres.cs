//using UnityEngine;
//using System.Collections;
//using System;
//using System.IO;
//
//public class loadres: MonoBehaviour {
//	/*
//    IEnumerator Start() {
//        WWW www = new WWW(url);
//        yield return www;
//        renderer.material.mainTexture = www.texture;
//    }*/
//	public int loadwidth = publicvar.loadwidth;
//    public bool setuped = false;
//	public GameObject camera;
//	public Transform target;
//
//	private int basei ;
//	private int basej ;
//
//
//	IEnumerator StartLoad()
//	{
//		if (target == null) {
//						setuped = false;			
//				} else {
//						if (publicvar.isloadfromLonLat) {
//								int[] center = map.lnglatToXY (publicvar.longitude, publicvar.latitude, publicvar.zoom);
//								basei = center [0];
//								basej = center [1];
//								Debug.Log (basei);
//								Debug.Log (basej);
//								publicvar.basei = basei;
//								publicvar.basej = basej;
//								Debug.Log (publicvar.basei);
//								Debug.Log (publicvar.basej);
//						} else {
//								basei = publicvar.basei;
//								basej = publicvar.basej;
//						}
//						camera.SetActive (false);
//
//						Debug.Log ("载入地图数据...请稍后");
//
//						Debug.Log (loadwidth);
//
//			for (int i = basei - loadwidth; i <= basei + loadwidth; i+=1)
//			for (int j = basej - loadwidth; j <= basej + loadwidth; j+=1){
//									Debug.Log("Loop: " + i+ ", "+j);
//									yield return TerrainWithTexture (i, j);
//								}
//						game0gui.gamelog ("加载完毕");
//						StartCoroutine (UpdateMap ());
//						camera.SetActive (true);
//				}
//	}
//
//    void Start()
//    {
//        //renderer.material.mainTexture = (Texture)Resources.Load("map/merge_109660_49020_10", typeof(Texture2D));
////        if(auth.online && !setuped)
//		if (!setuped)
//        {
//			setuped = true;
//            StartCoroutine(StartLoad()) ;
//        }
//
//    }
//
//	void Update(){
//		if (!setuped) {
//			setuped = true;
//			StartCoroutine(StartLoad());
//		}
//	}
//
//	int TerrainWithTexture(int i,int j)
//	{
////		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
//		TerrainData terraindata = new TerrainData ();
//		GameObject terrainobject = Terrain.CreateTerrainGameObject (terraindata);
//		Terrain terrain = terrainobject.GetComponent<Terrain> ();
//		// some settings of the terrain
//		terrain.name = String.Format ("{0},{1}", i, j);
////		terrain.terrainData.detailWidth = publicvar.lengthmesh;
////		terrain.terrainData.detailHeight = publicvar.lengthmesh;
//
//		terrain.transform.position = new Vector3( 
//				(i - basei)*publicvar.lengthmesh ,0,-( j - basej )*publicvar.lengthmesh
//				);
//		StartCoroutine(loadmap(i,j,terrain.renderer));
////		terrain.transform.localScale = new Vector3(publicvar.lengthmesh/10,1,publicvar.lengthmesh/10); 
//		terrain.transform.rotation = Quaternion.Euler( 0 ,180,0); 
//		terrain.transform.parent = transform;
//		return 0;
//	}
//
//
//    string mapurl(int i,int j)
//    {	
//		return string.Format ("http://{0}/merge/{1}/merge_{1}_{2}_10.jpg", publicvar.ip_addr, i, j);
//	}
//    string mappath(int i,int j)
//    {
//        return Application.dataPath+string.Format("/map/{1}/merge_{0}_{1}_10.jpg",i,j);
//    }
//	string remotemapurl(int x, int y, int zoom){
//		return string.Format ("https://api.tiles.mapbox.com/v3/examples.map-qfyrx5r8/{0}/{1}/{2}.jpg",zoom, x, y);
//	}
//
//
//	IEnumerator loadmap(int i , int j,Renderer renderer)
//    {
//        string path = mappath(i,j);
//        FileInfo fi1 = new FileInfo(path);   
//        WWW www;
//        if (fi1.Exists)
//        {
//            www = new WWW("file://"+path);
//			yield return www;
//			if (www.error != null )
//			{
//				print("Error:"+ www.error);
//			}
//            else
//            {
//			    renderer.material.mainTexture = www.texture;
//				www.Dispose();
//				www =null;
//            }
//		}
//
//		else
//		{
//			string url = remotemapurl(i,j,publicvar.zoom);
//			www=new WWW(url);
//			yield return www;
//			Debug.Log(url);
//			if (www.error != null )
//			{
//				print("Error:"+ www.error);
//				yield return www;
//			}
//            else
//            {
//				Debug.Log("Texture Loaded. Total memory" + GC.GetTotalMemory(true));
//			    if (!Directory.Exists(Application.dataPath+"/map" ))//判断文件夹是否已经存在
//			    {
//			    	Directory.CreateDirectory(Application.dataPath+"/map");
//			    }
//
//				Texture2D tmp = new Texture2D(256,256);
//				www.LoadImageIntoTexture(tmp);
//				try{renderer.material.mainTexture = tmp;}
//				catch{
//					Debug.Log("MissingReferenceException");
//				}
//				www.Dispose();
//				www =null;
//				Debug.Log("Texture Released. Total memory" + GC.GetTotalMemory(true));
//            }
//		}
//
//	} 
//
//	IEnumerator UpdateMap(){	
//		int[] tileNum;
//		int newbasei;
//		int newbasej;
//		
//		while (true) {
//			//wait for a second
//			yield return new WaitForSeconds(1);
//
//			// clear remote tiles
//			tileNum = map.getNumTile(target.position.x,target.position.z,basei, basej, publicvar.lengthmesh, 1);
//			newbasei = tileNum[0];
//			newbasej = tileNum[1];
//
//			StartCoroutine(ClearRemoteTiles(transform, newbasei,newbasej, loadwidth));
//
//			//wait for a second 
//			yield return new WaitForSeconds(1);
//			// clear remote tiles
//			tileNum = map.getNumTile(target.position.x,target.position.z,basei, basej, publicvar.lengthmesh, 1);
//			newbasei = tileNum[0];
//			newbasej = tileNum[1];
//
//
//			StartCoroutine(FlushNewTiles(newbasei, newbasej,loadwidth));
//		}
//					
//	}
//
//	IEnumerator ClearRemoteTiles(Transform father, int newbasei, int newbasej, int width){
//		foreach (Transform child in father) {
//			int[] childNum = map.getNumTile(child.position.x,child.position.z,basei,basej,publicvar.lengthmesh, 1);
//			int numDeltaMax = Mathf.Max (Mathf.Abs(childNum[0]-newbasei), Mathf.Abs (childNum[1] -newbasej));
//			if (numDeltaMax> width){
//				Debug.Log ("i destroyed myself!"+child.name);
////				Texture2D.DestroyImmediate(child.renderer.material.mainTexture, true);
//				GameObject.DestroyImmediate(child.gameObject, true);
//			}		
//		}
//		yield return 0;
//	}
//
//	IEnumerator FlushNewTiles(int newbasei, int newbasej, int width){
//		for (int i = newbasei - loadwidth; i <= newbasei + loadwidth; i+=1)
//			for (int j = newbasej - loadwidth; j <= newbasej +	loadwidth; j+=1) {
//				GameObject existedPlane = GameObject.Find (String.Format ("{0},{1}", i, j));
//				if (existedPlane == null) {
//				yield return TerrainWithTexture (i, j);
//				}	
//				else{
//					yield return 0;
//				}
//			}
//
//
//	}
//
//}
