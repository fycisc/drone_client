using UnityEngine;
using System.Collections;


public class main_menu : MonoBehaviour {


	public GUISkin mySkin;
	private Rect windowRect0 = new Rect (Screen.width-500, 10, 480, 490);
	private Rect windowRect1 = new Rect (50, 10, 400, 450);

	private bool ToggleBTN = false;
	private bool ConnectServer = false;
	private bool Settings = false;
	private bool Help = false;
	private bool About = false;
	private bool  Window1 = true;
	private float bul=1f;
	private float HorizSliderValue = 0.5f;
	private float VertSliderValue = 0.5f;
	public static float opacity=1.0f;
	public static bool roboGui=true;

	//private string ip_addr = "127.0.0.1";
	// Use this for initialization
	void Start () {

	}
	void ConnectServerWindow(int windowID)
	{
		GUILayout.BeginVertical();
		GUILayout.Space(2);
		GUILayout.Label("连接到服务器");

		GUILayout.Space(2);

		GUILayout.BeginHorizontal();
		GUILayout.Label("IP地址", "ShortLabel");
		publicvar.ip_addr = GUILayout.TextField(publicvar.ip_addr);
		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		GUILayout.BeginHorizontal();
		GUILayout.Label("用户名", "ShortLabel");
		publicvar.username = GUILayout.TextField(publicvar.username);
		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		GUILayout.BeginHorizontal();
		GUILayout.Label("密码", "ShortLabel");

		publicvar.passwd = GUILayout.PasswordField (publicvar.passwd, "*"[0], 25);
		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		if (GUILayout.Button("连接","Button") )
		{
			ConnectServer = ! ConnectServer;
			Application.LoadLevel("game0");
		}
		GUILayout.EndVertical();
		GUI.DragWindow ();
	}

	void MyWindow1 (int windowID) 
	{

		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Olivia 控制台","ShortLabel");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();


		GUILayout.Space(15);
		GUILayout.Label("版本号：内部测试0.1");

		GUILayout.Label("", "Divider");

		if (GUILayout.Button("连接到服务器","Button") )
			ConnectServer = ! ConnectServer;

		if (GUILayout.Button("设置","Button") )
			Settings = ! Settings;

		if (GUILayout.Button("关于","Button") )
			About = ! About;

		if (GUILayout.Button("退出","Button") )
			Application.Quit();


		GUILayout.Label("", "Divider");
		GUILayout.Box("This is a textbox\n this can be used for big texts");

		GUILayout.EndVertical();
		GUILayout.Space(10);
		GUILayout.EndHorizontal();

		GUI.DragWindow ();
	}


	// Update is called once per frame
	private Rect connectRect = new Rect (Screen.width-500, 10, 400, 300);
	void OnGUI () {
		GUI.skin = mySkin;
		
		if (ConnectServer)
		{
			windowRect0 = GUI.Window (0, connectRect, ConnectServerWindow, "");   
		}

		if (Window1)
			windowRect1 = GUI.Window (1, windowRect1, MyWindow1, "");   

	}



}
