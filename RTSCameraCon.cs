using UnityEngine;
using System.Collections;

public class RTSCameraCon : MonoBehaviour {

	// Use this for initialization
	public float tarx=0,tary=0,tarz=0;
	public float distance = 500.0f,zoomspeed = 0.5f;
	public float distancemini = 20;
	public float distancemax = 100000;
	public float xSpeed = 25.0f;
	public float ySpeed = 12.0f;
	public float dis = 0;
	public float yMaxLimit = -20;
	public float yMinLimit = 80;

	private static Transform target;
	private static bool lockon = false;

	private float fai = 0 , theta = 0;
	public static float k_zoom = 1.0f;

	public static void setTarget(Transform tar)
	{
		target = tar;
		lockon = true;
	}

	void Start () {
		theta = 60;
		dis = distance;
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		k_zoom+=k_zoom*Input.GetAxis("Mouse ScrollWheel")*zoomspeed*0.1f;
		theta += Input.GetAxis("Mouse ScrollWheel") * ySpeed * 0.1f;
		theta = ClampAngle(theta, yMinLimit+30, yMaxLimit);
		adapt_distance(k_zoom);
		adapt_angle();
	}
	void follow_position()
	{
		tarx = target.position.x;
		tary = target.position.y;
		tarz = target.position.z;
	}

	void adapt_distance(float k_zoom)
	{
		dis=k_zoom*distance;

		if(dis<distancemini )
		{
			dis=distancemini;
			k_zoom=dis/distance;
		}
		if(dis>distancemax)
		{
			dis=distancemax;
			k_zoom=dis/distance;
		}
	}

	void adapt_angle()
	{
		if(!lockon)
		{
			if(Input.GetMouseButton(0))
			{
				float posex=Input.GetAxis("Mouse X") * xSpeed * 0.2f*k_zoom;
				float posey=Input.GetAxis("Mouse Y") * ySpeed * 0.2f/Mathf.Sin(theta*Mathf.Deg2Rad)*k_zoom;
				Vector3 pose = new Vector3(-posex,0,-posey);
				Vector3 tmp=Quaternion.Euler(0, fai, 0)* pose;
				tarx+=tmp.x;
				tary+=tmp.y;
				tarz+=tmp.z;
				//lockon=false;
				//print("Tar x "+tarx+"tary "+tary+" tarz "+tarz);
			}
		}
		else 
		{
			follow_position();
		}

		if(Input.GetMouseButton(3)||Input.GetKey(KeyCode.LeftAlt))
		{
			fai += Input.GetAxis("Mouse X") * xSpeed * 0.2f;
			theta -= Input.GetAxis("Mouse Y") * ySpeed * 0.2f;
			theta = ClampAngle(theta, yMinLimit, yMaxLimit);
		}
		if(Input.GetKey(KeyCode.Escape))
		{
			lockon = false;
		}
		var rotation = Quaternion.Euler(theta, fai, 0);

		var position = rotation *new Vector3(0.0f, 0.0f, -dis) +new  Vector3(tarx,tary,tarz);

		transform.rotation = rotation;
		transform.position = position;
	}


	static float ClampAngle (float angle,float min,float max)
	{
		if (angle < -360)
		angle += 360;
		if (angle > 360)
		angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
