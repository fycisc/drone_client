using UnityEngine;
using System.Collections;
public class game0gui : MonoBehaviour {

	public GUISkin mySkin;
	private int left = 0;
	private int on =  Screen.height - Screen.height/2;
	private int width = Screen.width /4;	
	private int height = Screen.height /2;	
	//private Rect windowRect1 = new Rect (50, Screen.height-400, 500, 360);

	private bool ToggleBTN = false;
	private bool Window0 = true;
	private float bul=1f;
	private float HorizSliderValue = 0.5f;
	private float VertSliderValue = 0.5f;
	public static float opacity=1.0f;
	public static bool roboGui=true;
	public static string log = "";
	public static string log_raw = "";
	Vector2 Sc;
	// Use this for initialization
	public static void gamelog(string a)
	{
		log_raw+=(a+"\n");
		log = "<color=blue><size=15>"+log_raw+"</size></color>";
	}
	
	void logWindow(int windowID)
	{

		GUILayout.BeginVertical();
		GUILayout.Space(2);
		GUILayout.Label("日志");
		GUILayout.Space(2);
		
		Sc = GUILayout.BeginScrollView(Sc,GUILayout.Width(width*3/5),GUILayout.Height(height/2));  
        GUILayout.Box(log);  
        GUILayout.EndScrollView();    

		GUILayout.EndVertical();
	}
	public Texture2D img;
	void OnGUI () 
	{
		Rect windowRect1 = new Rect (left, on, width, height);
		GUI.skin = mySkin;
		//if (Window0)
		//	windowRect1 = GUI.Window (0, windowRect1, logWindow, "");   
		GUI.skin.box.normal.background = img;
		 GUI.Box(windowRect1, log);
	}


}
