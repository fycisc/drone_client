using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class publicvar : MonoBehaviour {
	public GameObject originairplane1;
	public GameObject originairplane2;

	// Use this for initialization
	public static string ip_addr = "127.0.0.1";
	public static string username = "admin";
	//Only in debug MODE
	public static string passwd = "qiaochu";
	public static int basei = 109650;//109630;
	public static int basej = 49050;//48780;
	public static float lengthmesh = 2304.68f;//2304.68f;
    public static int loadwidth = 5;
	public static int mergenum = 1;
	public static float longitude = 117.2554888244f; //USTC West Campus
	public static float latitude = 31.8390572361f;
	public static int zoom = 17;
	public static bool isloadfromLonLat = false;
	public static int SizeX = 20;
	public static int SizeZ = 20;
	public static int maxTileX = 1;
	public static int maxTileY = 1;
	public static int heightmapres = 65;
	public static float maxHeight = 2000;


	public Dictionary<string, Terrain> terrains = new Dictionary<string, Terrain> ();
	public Dictionary<string, airobj> airs= new Dictionary<string, airobj >();
//	public HeightmapLoader heightsloader = new HeightmapLoader();


	void Start(){
		
	}


	
}
