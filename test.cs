using UnityEngine;
using System.Collections;
using System;
public class test
{

	public static int[] lnglatToXY(float longitude, float latitude, int zoom){
		float lat_rad;
		float n;
		int xtile;
		int ytile;
		lat_rad = (float) latitude * Mathf.PI / 180.0f;
		n = Mathf.Pow(2, zoom);
		xtile =(int) Mathf.Floor((longitude + 180.0f) / 360.0f * n);
		ytile =(int) Mathf.Floor((1.0f - Mathf.Log(Mathf.Tan(lat_rad) + (1.0f / Mathf.Cos(lat_rad))) / Mathf.PI) / 2.0f * n);
		int[] answer = new int[2];
		answer [0] = xtile; answer [1] = ytile;
		return answer; 
	}
	
	public static float[] XYToLonLat(int xtile, int ytile, int zoom){
		float lon_deg;
		float lat_deg;
		float lat_rad;
		float n;
		n = Mathf.Pow (2.0f, zoom);
		lon_deg = (xtile * 360.0f) / n - 180.0f;
		lat_rad = Mathf.Atan (Convert.ToSingle( System.Math.Sinh(Mathf.PI * (1.0f - 2.0f * ytile / n))));
		lat_deg = Mathf.Rad2Deg * lat_rad;
		return new float[] {lon_deg, lat_deg};	
	}


}

