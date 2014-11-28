using UnityEngine;
using System.Collections;
using Leap;


public class handsStatus{
	public Vector3 relativeVelocity;
	public Vector3 relativePos;
	public Vector3 meanVelocity;

	public handsStatus(){
		this.relativeVelocity = new Vector3 (0, 0, 0);
		this.relativePos = new Vector3 (0, 0, 0);
		this.meanVelocity = new Vector3 (0, 0, 0);
	}

	public handsStatus(Hand lhand, Hand rhand){
		this.relativePos = rhand.PalmPosition.ToUnity () - lhand.PalmPosition.ToUnity ();
		this.relativeVelocity = rhand.PalmVelocity.ToUnity () - lhand.PalmVelocity.ToUnity ();
		this.meanVelocity = (rhand.PalmVelocity.ToUnity () + lhand.PalmVelocity.ToUnity ())/2.0f;
	}

	public void updateStatus(Hand lhand, Hand rhand){
		if (lhand == null || rhand == null) {
			this.clearStatus();
			return;
			}

		this.relativePos = rhand.PalmPosition.ToUnity () - lhand.PalmPosition.ToUnity ();
		this.relativeVelocity = rhand.PalmVelocity.ToUnity () - lhand.PalmVelocity.ToUnity ();
		this.meanVelocity = (rhand.PalmVelocity.ToUnity () + lhand.PalmVelocity.ToUnity ())/2.0f;
	}

	public void clearStatus(){
		this.relativePos.Set (0, 0, 0);
		this.relativeVelocity.Set (0, 0, 0);
		this.meanVelocity.Set (0, 0, 0);
	}
}




public class MovementManager{
	public int status;

	public MovementManager(){
		status = 0; // nothin
	}

	public void updateStatus(int i){
		if (i < 0 || i > 3) {
						status = 0;
						return;
				} else {
						this.status = i;
				}
	}
}


public class moveTerrain : MonoBehaviour
{
	private Controller controller;
	public GameObject target;

	public float grablimit = 0.9f;
	public float movevelocity = 0.05f;
	private bool isGrabbed_l;
	private bool isGrabbed_r;

	private float grabstrength_l=0f;
	private float grabstrength_r=0f;
	private float pinchstrength_l=0f;
	private float pinchstrength_r=0f;
	private Vector3 velocity;
	private Vector3 stablevelocity;
	private const float baseHeight = 400f;
	private const float baseAngel = 400f;
	private Queue RhandVelocities;
	private Queue statusqueue;
	private int remembernum = 8;
	private bool actionFinished = true;

	public handsStatus initialStatus;
	public MovementManager mvMngr;
//	private handsStatus currentStatus;
	
		// Use this for initialization
		void Start ()
		{
			controller = new Controller ();
			this.isGrabbed_l = false;
			this.isGrabbed_r = false;
			this.RhandVelocities = new Queue ();
			this.stablevelocity = new Vector3(0,0,0);
			this.initialStatus = new handsStatus();
			this.mvMngr = new MovementManager ();
			this.statusqueue = new Queue ();
//			this.currentStatus = new handsStatus();
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		updateMouseAction ();
		Frame frame;
		try{
			frame = controller.Frame();}
		catch{
			return;		
		}

			Hand[] hands;
			hands = getHands (frame);
			Hand lhand = hands [0];
			Hand rhand = hands [1];
		// update hand status
			updateIsgrabbed (lhand, rhand);
			updateStrength (lhand, rhand);
		// One-hand movement(drag the map)
			updateDrag (lhand, rhand);
		// Two-hand movement (zoom, rotate and pitch)
		if (isGrabbed_l && isGrabbed_r) {
						updateZoom (lhand, rhand);
						updateRotate (lhand, rhand);
						updatePitch (lhand, rhand);
						update2hdStatus (lhand, rhand);
				}
		try{
			grabstrength_l = rhand.PalmVelocity.ToUnity ().magnitude;
		}
		catch{
			return;		
		}
		}


//		void OnGUI(){
//		// info of LEFT hand
//			GUI.TextArea (new Rect (10, 10, 50, 50), this.isGrabbed_l.ToString ());
//			GUI.TextArea (new Rect (100, 10, 50, 50), this.grabstrength_l.ToString());
//			GUI.TextArea (new Rect (190, 10, 50, 50), this.pinchstrength_l.ToString());
//		try{
//			GUI.TextArea (new Rect (300, 10, 50, 50), this.mvMngr.status.ToString());}
//		catch{
//			return;}
//		// info of RIGHT hand
//			GUI.TextArea (new Rect (500, 10, 50, 50), this.isGrabbed_r.ToString ());
//			GUI.TextArea (new Rect (590, 10, 50, 50), this.grabstrength_r.ToString());
//			GUI.TextArea (new Rect (680, 10, 50, 50), this.pinchstrength_r.ToString());
//		}

	private void updateMouseAction(){
		updateMouseZoom ();
		updateMouseDrag ();
		updateMousePitch ();

		updateMouseRotate ();
		updateKeyDrag ();
		updateKeyPitch ();
	}
	private void updateMouseRotate(){
		if (!(Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand))) {
			return;
		}
		float speed = 2f;
		float vh = Input.GetAxis("Horizontal");
		gameObject.transform.RotateAround(gameObject.transform.position, 
		                                  Vector3.up,
		                                  vh* speed);
	}
	private void updateKeyPitch(){
		if (!(Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand))) {
			return;
		}
		float speed = 2f;
		float vv = Input.GetAxis ("Vertical");
		gameObject.transform.RotateAround(gameObject.transform.position, 
		                                  gameObject.transform.right,
		                                  -vv* speed);
	}
	private void updateKeyDrag(){
		if ((!Input.anyKey) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand)) {
			return;
				}
		float speed = 30f;
		float vx = Input.GetAxis("Horizontal");
		float vz = Input.GetAxis("Vertical");
		Vector3 velocity = new Vector3(vx*speed,0,vz*speed);
		Quaternion quat =  Quaternion.FromToRotation(Vector3.forward ,new Vector3(transform.forward.x,0,transform.forward.z));
		velocity = quat * velocity;
		
		gameObject.transform.position += velocity*gameObject.transform.position.y/baseHeight;
	}
	private void updateMouseZoom(){
		float k = 60f;
		float velocity = k * Input.GetAxis("Mouse ScrollWheel");
		gameObject.transform.Translate (Vector3.forward * velocity * gameObject.transform.position.y/baseHeight);
	}
	private void updateMouseDrag(){
		if (!Input.GetMouseButton(0)) {
		
			return;
				}
		// the left mouse down
		float speed = 1500f;
		float vz = -Input.GetAxis("Mouse Y")*Time.deltaTime;
		float vx = -Input.GetAxis("Mouse X")*Time.deltaTime;
//		Debug.Log("vx, vz " + vx*speed +" " +vz*speed);	
		Vector3 velocity = new Vector3(vx*speed,0,vz*speed);
		

		Quaternion quat =  Quaternion.FromToRotation(Vector3.forward ,new Vector3(transform.forward.x,0,transform.forward.z));
		velocity = quat * velocity;

		gameObject.transform.position += velocity*gameObject.transform.position.y/baseHeight;
	}

	private void updateMousePitch(){
		if (!Input.GetMouseButton(1)) {
			return;
				}
		// the right mouse down
		// move the mouse vertically to change the angle
		float speed = 4f;
		float vv = Input.GetAxis ("Mouse Y");
		gameObject.transform.RotateAround(gameObject.transform.position, 
		                                  gameObject.transform.right,
		                                  vv* speed);
	}


	private void update2hdStatus(Hand lhand, Hand rhand){
		while (statusqueue.Count > 40) {
			statusqueue.Dequeue();
				}
		if (lhand == null || rhand == null) {
			statusqueue.Clear();
			return;
				}
		if (lhand.PalmVelocity.ToUnity().magnitude < 40 && lhand.PalmVelocity.ToUnity().magnitude < 40) {
			mvMngr.updateStatus(0);
			statusqueue.Enqueue(0);
		}
		object[] statuses = statusqueue.ToArray ();
		bool allzero = true;
		int tag = 0;
		for (int i = 0; i< Mathf.Min(statuses.Length, 10) ; i++) {
			if ((int)statuses[i] != 0) {
				allzero = false;
				tag = (int)statuses[i];
				break;
//				mvMngr.updateStatus((int)statuses[i]);
//				statusqueue.Enqueue((int)statuses[i]);
			}	
		}
		if (allzero) {
						statusqueue.Clear ();
				} else {
						mvMngr.updateStatus (tag);
						statusqueue.Enqueue (tag);
				}
//		return;

//		statusqueue.Clear ();	

		if (lhand != null) {
			Vector3 newrelPos = rhand.PalmPosition.ToUnity () - lhand.PalmPosition.ToUnity (); 
			Vector3 deltapos = newrelPos-this.initialStatus.relativePos;
			float theta = deltapos.magnitude/newrelPos.magnitude;
//			float theta = ((newrelPos-this.initialStatus.relativePos)).z/newrelPos.magnitude;
			Vector3 relevantVelocity = rhand.PalmVelocity.ToUnity() - lhand.PalmVelocity.ToUnity();
			float vy = rhand.PalmVelocity.ToUnity().y;

			if (Mathf.Abs(vy)>300f) {
				mvMngr.updateStatus(3);
			}
			else if (Mathf.Abs(relevantVelocity.x) > 300 || (Mathf.Abs(relevantVelocity.x) > 160 && mvMngr.status == 1)){
				mvMngr.updateStatus(1);
			}
			else if ((Mathf.Abs(theta) > 0.04 || mvMngr.status == 2&&(Mathf.Abs(relevantVelocity.x)<5f*Mathf.Abs(relevantVelocity.z) && Mathf.Abs(theta) > 0.008f)) && 
			         newrelPos.magnitude > 200f) {
				mvMngr.updateStatus(2) ;
//				Debug.Log(theta);
			}
			else{
				mvMngr.updateStatus(0);
			}
//			print(newrelPos.magnitude);

			statusqueue.Enqueue(mvMngr.status);

		}

		this.initialStatus.updateStatus (lhand, rhand);
	}


	private void updateZoom(Hand lhand, Hand rhand){
		if (mvMngr.status != 1  || lhand == null) {
			return;
				}
		mvMngr.updateStatus (1);
		statusqueue.Enqueue (1);
		Vector3 relevantVelocity;
		float velocity = 0f;
		// give values
		relevantVelocity = rhand.PalmVelocity.ToUnity() - lhand.PalmVelocity.ToUnity();
		if (relevantVelocity.x > 0) {
			velocity = relevantVelocity.magnitude;
			if (transform.position.y<100) {
				return;
			}
		}
		else {
			velocity = -relevantVelocity.magnitude;
		}
		
		// move along the local z axis
		gameObject.transform.Translate (Vector3.forward * velocity * 0.05f * gameObject.transform.position.y/baseHeight);
		
	}


	private void updateRotate(Hand lhand, Hand rhand){
		if ( mvMngr.status != 2 || lhand == null) {
			return;
			}
		mvMngr.updateStatus (2);
		statusqueue.Enqueue (2);
		try{
			Vector3 newrelPos = rhand.PalmPosition.ToUnity () - lhand.PalmPosition.ToUnity (); 
			Vector3 deltapos = newrelPos-this.initialStatus.relativePos;
			float theta = deltapos.magnitude/newrelPos.magnitude;
			float tag = newrelPos.x* deltapos.z - newrelPos.z*deltapos.x;
			if (tag<0) {
				theta = -theta;
			}
			Vector3 unit = gameObject.transform.forward.normalized;
			Vector3 point = gameObject.transform.position 
						+ unit * Mathf.Abs(transform.position.y)/Mathf.Abs(unit.y);

			const float k = 80f;
			gameObject.transform.RotateAround(point, Vector3.up, k*theta);
		}
		catch{
			return;
		}
	}


	private void updatePitch(Hand lhand, Hand rhand){
		if (mvMngr.status != 3 || lhand == null) {
			return;
				}
		mvMngr.updateStatus (3);
		statusqueue.Enqueue (3);
		try{
			gameObject.transform.RotateAround(gameObject.transform.position, 
			                                  gameObject.transform.right, 
			                                  -rhand.PalmVelocity.ToUnity().y / baseAngel);

		}
		catch{
			return;		
		}
	}
	

	private void updateDrag (Hand lhand, Hand rhand){
		if (lhand != null) {
			this.velocity = Vector3.zero;
			this.stablevelocity = Vector3.zero;
			return;
		}
		mvMngr.updateStatus (0);
		statusqueue.Enqueue (0);

		if (isGrabbed_r) {
			Vector3 palmvelocity;
			try{
				palmvelocity = rhand.PalmVelocity.ToUnity();
			}
			catch{
				return;
			}
			Vector3 velocity = - this.movevelocity * palmvelocity;
			this.velocity = new Vector3(velocity.x,0,velocity.z);

//			Debug.Log("old  "+ this.velocity.x + " "+ this.velocity.z);
			Quaternion quat =  Quaternion.FromToRotation(Vector3.forward ,new Vector3(transform.forward.x,0,transform.forward.z));
			this.velocity = quat * this.velocity;
//			Debug.Log("new  "+ this.velocity.x + " "+ this.velocity.z);

			RhandVelocities.Enqueue (this.velocity);
			if(RhandVelocities.Count > this.remembernum){
				RhandVelocities.Dequeue();
			}

			gameObject.transform.position += this.velocity*gameObject.transform.position.y/baseHeight;
//			gameObject.transform.Translate(new Vector3(velocity.x, ,velocity.z));
			this.stablevelocity.Set(0,0,0);
			foreach (var v in RhandVelocities) {
				stablevelocity += (Vector3)v;
			}
			stablevelocity /= RhandVelocities.Count;
			this.velocity = stablevelocity;
		}
		else {
			this.velocity /= 1.05f;
			gameObject.transform.position += this.velocity*gameObject.transform.position.y/baseHeight;
		}
	}


	private void updateIsgrabbed(Hand lhand, Hand rhand){
		if (isGrabbed(rhand)) {
			this.isGrabbed_r= true;
				}
		else {
			this.isGrabbed_r = false;
				}

		if (isGrabbed(lhand)) {
			this.isGrabbed_l= true;
		}
		else {
			this.isGrabbed_l = false;
		}
	}


	private void updateStrength(Hand lhand, Hand rhand){
		try{
			this.grabstrength_r = rhand.GrabStrength;
			this.pinchstrength_r = rhand.PinchStrength;
			this.grabstrength_l = lhand.GrabStrength;
			this.pinchstrength_l = lhand.PinchStrength;
		}
		catch{
			return;		
		}
	}


	private bool isGrabbed(Hand hand){
		if (hand == null) {
			return false;
				}
		return (hand.GrabStrength > this.grablimit);
		
	}


	private Hand[] getHands(Frame frame){
		if (frame.Hands.IsEmpty) {
			Hand[] emptyhands = new Hand[2]{null, null};
			return emptyhands;
		}
		Hand lhand;
		Hand rhand;
		Hand[] hands;
		lhand = frame.Hands.Leftmost;
		rhand = frame.Hands.Rightmost;
		if (!(lhand.IsLeft && rhand.IsRight)) {
			if (lhand.IsLeft) {
				hands = new Hand[2]{lhand, null};
			}
			else {
				hands = new Hand[2]{null, rhand};
			}
				}
		else {
			hands = new Hand[2]{lhand, rhand};
				}
		return hands;	
	}
	
}

