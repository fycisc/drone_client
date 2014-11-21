using UnityEngine;
using System;
using System.Collections;

public class map //: MonoBehaviour
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


	// given 
	public static float[] getUnityPosfromLatlng(float lon, float lat, int zoom){
//		return new float[] {0,0,0};

		// get lon, lat length
		float lonlength = Mathf.Abs(XYToLonLat(publicvar.basei,publicvar.basej,zoom)[0]-XYToLonLat(publicvar.basei+publicvar.mergenum,publicvar.basej,zoom)[0]);
		float latlength = Mathf.Abs(XYToLonLat(publicvar.basei,publicvar.basej,zoom)[1]-XYToLonLat(publicvar.basei,publicvar.basej+publicvar.mergenum,zoom)[1]);

		// given lon lat
		// get tileNum
		int[] tileNum = lnglatToXY (lon, lat, zoom);

		// get tilelon tilelat of tile top-left
		float[] LonlatTopleft = XYToLonLat (tileNum [0], tileNum [1], zoom);

		// get delta of the lon, lat
		float[] deltaLonlat = new float[] {lon- LonlatTopleft[0],lat-LonlatTopleft[1]};

		// get x,z of topleft
		float[] posTopleft = new float[]{((tileNum [0] - publicvar.basei) / publicvar.mergenum - 0.5f) * publicvar.lengthmesh,
			(-(tileNum [1] - publicvar.basej) / publicvar.mergenum + 0.5f) * publicvar.lengthmesh,
		};

		// get x,z of the given lon,lat
		float[] position = new float[]{
			posTopleft[0]+publicvar.lengthmesh*deltaLonlat[0]/lonlength,
			posTopleft[1]+publicvar.lengthmesh*deltaLonlat[1]/latlength
		};
		return new float[]{
			position[0], 0, position[1]
		};
	
	}





	public static int[] getNumTile(float x, float z,int centerX, int centerY, float lengthMesh, int mergenum){
		int[] tileNum = new int[2];
		// tileX
		tileNum [0] = Mathf.FloorToInt(Mathf.RoundToInt (2.0f * x / lengthMesh) / 2 ) * mergenum + centerX;
		//tileY
		tileNum [1] = -Mathf.FloorToInt( Mathf.RoundToInt (2.0f * z / lengthMesh) / 2) * mergenum + centerY;
		return tileNum;
	}

}

