using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class publicvar : MonoBehaviour {
	public GameObject originairplane1;
	public GameObject originairplane2;

	public static float lengthmesh_17 = 2296.26f;
	// Use this for initialization
	public static string ip_addr = "127.0.0.1";
	public static string host ="map.dronevery.com:8000";// "127.0.0.1:8000";
	public static string username = "admin";
	//Only in debug MODE
	public static string passwd = "qiaochu";
	public static int basei = 27397;//109630;
	public static int basej = 12285;//48780;
	public static float lengthmesh = 2296.26f*Mathf.Pow(2,17-zoom);//2304.68f;
    public static int loadwidth = 5;
	public static int mergenum = 1;
	public static float longitude = 121.0727906227f; //dashushan
	public static float latitude = 41.1472260440f;
	public static int zoom = 14;
	public static bool isloadfromLonLat = false;
	public static int SizeX = 20;
	public static int SizeZ = 20;
	public static int maxTileX = 8;
	public static int maxTileY = 8;
	public static int heightmapres = 33;
	public static float maxHeight = 2000;


	public Dictionary<string, Terrain> terrains = new Dictionary<string, Terrain> ();
	public Dictionary<string, airobj> airs= new Dictionary<string, airobj >();
//	public HeightmapLoader heightsloader = new HeightmapLoader();


	void Start(){
		
	}
}
