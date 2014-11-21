using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class publicvar : MonoBehaviour {

	// Use this for initialization
	public static string ip_addr = "222.195.92.141";
	public static string username = "admin";
	//Only in debug MODE
	public static string passwd = "qiaochu";
	public static int basei = 26969;//109630;
	public static int basej = 12419;//48780;
	public static float lengthmesh = 23.0468f;//2304.68f;
    public static int loadwidth = 5;
	public static int mergenum = 1;
	public static float longitude = 117.2554888244f; //USTC West Campus
	public static float latitude = 31.8390572361f;
	public static int zoom = 17;
	public static bool isloadfromLonLat = false;

	public static int SizeX = 20;
	public static int SizeZ = 20;
	public static Dictionary<int[], Terrain> terrains = new Dictionary<int[], Terrain> ();
	public static Dictionary<string, airobj> airs = new Dictionary<string, airobj >();

}
