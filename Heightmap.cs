using UnityEngine;
using System;
using System.Collections;

public class Heightmap
{
	
//	Stream imageStreamSource = new FileStream("tulipfarm.tif", FileMode.Open, FileAccess.Read, FileShare.Read);
//	TiffBitmapDecoder decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
//	BitmapSource bitmapSource = decoder.Frames[0];
	public static void Main(){
				Texture2D heightmap = (Texture2D)Resources.Load ("/Users/feiyicheng/Downloads/GDEM/ASTGTM2_N31E117/ASTGTM2_N31E117_dem.tif");
				Color[] pixs = heightmap.GetPixels ();
				for (int i = 0; i < 10; i++) {
						Console.WriteLine (pixs [i]);
				}
		}

}

