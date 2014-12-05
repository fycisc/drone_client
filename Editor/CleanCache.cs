using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;


public class CleanCache : EditorWindow {

	[MenuItem("TerrainTools/CleanHeightmapCache/All")]
	static void CleanHeightmapCache_all(){
		string folderpath = Application.dataPath + "/Resources/heightmap/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach(DirectoryInfo childfolder in folder.GetDirectories()){
				if(Directory.Exists(childfolder.FullName)){
					foreach (FileInfo file in childfolder.GetFiles()) {
						file.Delete();
					}
				}
			}
		}
	}

	[MenuItem("TerrainTools/CleanHeightmapCache/10")]
	static void CleanHeightmapCache_10(){
		string folderpath = Application.dataPath + "/Resources/heightmap/10/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}

	[MenuItem("TerrainTools/CleanHeightmapCache/11")]
	static void CleanHeightmapCache_11(){
		string folderpath = Application.dataPath + "/Resources/heightmap/11/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanHeightmapCache/12")]
	static void CleanHeightmapCache_12(){
		string folderpath = Application.dataPath + "/Resources/heightmap/12/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanHeightmapCache/13")]
	static void CleanHeightmapCache_13(){
		string folderpath = Application.dataPath + "/Resources/heightmap/13/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanHeightmapCache/14")]
	static void CleanHeightmapCache_14(){
		string folderpath = Application.dataPath + "/Resources/heightmap/14/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanHeightmapCache/15")]
	static void CleanHeightmapCache_15(){
		string folderpath = Application.dataPath + "/Resources/heightmap/15/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanHeightmapCache/16")]
	static void CleanHeightmapCache_16(){
		string folderpath = Application.dataPath + "/Resources/heightmap/16/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanHeightmapCache/17")]
	static void CleanHeightmapCache_17(){
		string folderpath = Application.dataPath + "/Resources/heightmap/17/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}



	[MenuItem("TerrainTools/CleanTextureCache/All")]
	static void CleanTextureCache_all(){
		string folderpath = Application.dataPath + "/Resources/map/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach(DirectoryInfo childfolder in folder.GetDirectories()){
				if(Directory.Exists(childfolder.FullName)){
					foreach (FileInfo file in childfolder.GetFiles()) {
						file.Delete();
					}
				}
			}
		}
	}
	
	[MenuItem("TerrainTools/CleanTextureCache/10")]
	static void CleanTextureCache_10(){
		string folderpath = Application.dataPath + "/Resources/map/10/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/11")]
	static void CleanTextureCache_11(){
		string folderpath = Application.dataPath + "/Resources/map/11/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/12")]
	static void CleanTextureCache_12(){
		string folderpath = Application.dataPath + "/Resources/map/12/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/13")]
	static void CleanTextureCache_13(){
		string folderpath = Application.dataPath + "/Resources/map/13/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/14")]
	static void CleanTextureCache_14(){
		string folderpath = Application.dataPath + "/Resources/map/14/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/15")]
	static void CleanTextureCache_15(){
		string folderpath = Application.dataPath + "/Resources/map/15/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/16")]
	static void CleanTextureCache_16(){
		string folderpath = Application.dataPath + "/Resources/map/16/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
	[MenuItem("TerrainTools/CleanTextureCache/17")]
	static void CleanTextureCache_17(){
		string folderpath = Application.dataPath + "/Resources/map/17/";
		DirectoryInfo folder = new DirectoryInfo (folderpath);
		if (Directory.Exists(folderpath)) {
			foreach (FileInfo file in folder.GetFiles()) {
				file.Delete();
			}
		}
	}
}
