using UnityEngine;
using System.Collections;

public class flying : MonoBehaviour {


	public float kcl = 18.0f;
	public float kcld = 0.1f;
	public float mass = 20000.0f;
	public float v0 = 100.0f;
	public float krotate = - 10.1f;
	public float winposition = -0.1f;

	public float kroll = 0.1f;


	public float kpitch = 10f;

	public float kf = 10f;

	public float power = 0.0f;

	public float maxengine = 240000.0f;

	public float kfup = 100f;
	float cl()
	{
		float pitch = -transform.eulerAngles.x;
		if (pitch >180)
			pitch = pitch - 360;
		if(pitch < -180)
			pitch = pitch + 360;


		if (pitch>30)
			return (80-pitch)*kcl*Mathf.Deg2Rad;
		return kcl*(pitch+5)*Mathf.Deg2Rad;

		if(pitch<-60)
			return 0;
	}
	/*
	Vector3 forward()
	{
		Vector3 f = new Vector3(0,0,1);
		return transform.rotation * f;
	}
	*/

	Vector3 ApplyL()
	{
		float  speedforward = Vector3.Dot(rigidbody.velocity,transform.forward);
		float f = cl ()*speedforward*speedforward;
		Vector3 L = new Vector3(0,f,0);

		rigidbody.AddRelativeForce(L);
		rigidbody.AddRelativeTorque(new Vector3(f,0,0)*winposition);
		print (new Vector3(f,0,0)*winposition);

		Vector3 fri_wing = new Vector3(0,0,-f*kcld);
		rigidbody.AddRelativeForce(fri_wing);


		return L;
	}

	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3(0,0,v0);

		rigidbody.mass = mass;

		rigidbody.inertiaTensor = new Vector3(10000,10000,10000);
	}

	void ApplyEngine()
	{
		rigidbody.AddRelativeForce(new Vector3(0,0,power*maxengine/100.0f));
	}

	Vector3 everysqr(Vector3 c)
	{
		return new Vector3(c.x*c.x,c.y*c.y,c.z*c.z);
	}

	void friction()
	{
		rigidbody.AddForce(-  everysqr(rigidbody.velocity)*kf);
	}


	void frictionUP()
	{
		float upspeed= Vector3.Dot (transform.up , rigidbody.velocity);

		print ("Up"+upspeed);

		float f = upspeed*upspeed * kfup;

		rigidbody.AddRelativeForce(0,-f,0);
	}
	
	// Update is called once per frame
	void pitch()
	{
		float flap = 0.0f;

		if(Input.GetKey(KeyCode.DownArrow))
		{
			flap = - kpitch;
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			flap = + kpitch;
		}
		rigidbody.AddRelativeTorque(flap,0,0);
	}

	void roll()
	{
		float f = cl ()*rigidbody.velocity.sqrMagnitude;
		float flap=0f;


		if(Input.GetKey(KeyCode.LeftArrow))
		{
			flap = + kroll;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			flap = - kroll;
		}
		rigidbody.AddRelativeTorque(0,0,flap*f);
	}
	void Update()
	{
		print("V:"+rigidbody.velocity);
		if (Input.GetKey(KeyCode.W ))
		{
			power += 1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			power -= 1f;
		}
		power = Mathf.Clamp(power,0,100.0f);

		pitch();
		roll ();

	}

	void FixedUpdate () {
	
		frictionUP();
		ApplyL();
		ApplyEngine();
		friction();	
	}
}
