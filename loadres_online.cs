using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class loadres_online: MonoBehaviour {
	/*
    IEnumerator Start() {
        WWW www = new WWW(url);
        yield return www;
        renderer.material.mainTexture = www.texture;
    }*/
	public int loadwidth = 20;
	public bool setuped = false;
	public GameObject camera;

	private int basei;
	private int basej;

	IEnumerator StartLoad()
	{
		//StartCoroutine(loadmap(109660,49020,renderer));
		camera.SetActive(false);
		//        while(!auth.online);
		//            yield return 0;
		//		game0gui.gamelog("载入地图数据...请稍后");
		Debug.Log("载入地图数据...请稍后");
		loadwidth = publicvar.loadwidth;
		print(loadwidth);

		int[] center = map.lnglatToXY (publicvar.longitude, publicvar.latitude, publicvar.zoom);
		basei = center[0];
		basej = center[1];
		for (int i = basei - loadwidth;i < basei + loadwidth;i+=1)
			for (int j = basej - loadwidth;j < basej +	loadwidth;j+=1)
				yield return PlaneWithTexture(i,j);
		game0gui.gamelog("加载完毕");
		camera.SetActive(true);
	}
	
	void Update()
	{
		//renderer.material.mainTexture = (Texture)Resources.Load("map/merge_109660_49020_10", typeof(Texture2D));
		//        if(auth.online && !setuped)
		if (!setuped)
		{
			setuped = true;
			StartCoroutine(StartLoad()) ;
		}
	}
	
	int PlaneWithTexture(int i,int j)
	{
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.position = new Vector3( 
		                                       (i - basei)*publicvar.lengthmesh ,0,-( j - basej )*publicvar.lengthmesh
		                                       );
		StartCoroutine(loadmap(i,j,plane.renderer));
		plane.transform.localScale = new Vector3(publicvar.lengthmesh/10,1,publicvar.lengthmesh/10); 
		plane.transform.rotation = Quaternion.Euler(0, 180, 0); 
		plane.transform.parent = transform;
		return 0;
	}

	string remotemapurl(int x, int y, int zoom){
		return string.Format ("https://api.tiles.mapbox.com/v3/examples.map-qfyrx5r8/{0}/{1}/{2}.jpg",zoom, x, y);
	}
	string mappath(int i,int j)
	{
		return Application.dataPath+string.Format("/map/{1}/merge_{0}_{1}_10.jpg",i,j);
	}
	
	IEnumerator loadmap(int i , int j,Renderer renderer)
	{
		string path = mappath(i,j);
		FileInfo fi1 = new FileInfo(path);   
		WWW www;
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
			}
		}
		
		else
		{
			string url = remotemapurl(i,j,publicvar.zoom);
			www=new WWW(url);
			Debug.Log(url);
			yield return www;
			if (www.error != null )
			{
				print("Error:"+ www.error);
				yield return www;
			}
			else
			{
				if (!Directory.Exists(Application.dataPath+"/map" ))//判断文件夹是否已经存在
				{
					Directory.CreateDirectory(Application.dataPath+"/map");
				}
				//game0gui.gamelog("Cache"+Application.dataPath);
				/*
			using (FileStream sw = fi1.OpenWrite()) 
			{
				sw.Write(www.bytes,0,www.bytes.Length);
			}*/
				renderer.material.mainTexture = www.texture;
			}
		}
		
	} 
}