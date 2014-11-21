var tarx=0;
var tary=0;
var tarz=0;
var distance = 500.0;
var zoomspeed=0.5;
var distancemini=20;
var distancemax=100000;

var xSpeed = 25.0;
var ySpeed = 12.0;

var yMinLimit = -20;
var yMaxLimit = 80;
var plane:GameObject;
private var fai = 0.0;
private var theta = 0.0;
private var lockon=false;
public static var k_zoom=1.0;
public var dis=0;
public var target:Transform;
public var g0:GameObject;
public var g1:GameObject;
@script AddComponentMenu("Camera-Control/Mouse Orbit")

function Start () {
    var angles = transform.eulerAngles;
    theta = 60;
	dis=distance;
	// Make the rigid body not change rotation
   	if (rigidbody)
		rigidbody.freezeRotation = true;
}

function LateUpdate ()
{
	k_zoom+=k_zoom*Input.GetAxis("Mouse ScrollWheel")*zoomspeed*0.1;
    //plane.SendMessage("adapt",k_zoom);
	dis=k_zoom*distance;
    if(dis>30000)
    {
        g0.active=true;
        g1.active=false;
    }
    else
    {
        g0.active=false;
        g1.active=true;
    }
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
	if(Input.GetMouseButton(0))
	{
		var posex=Input.GetAxis("Mouse X") * xSpeed * 0.2*k_zoom;
		var posey=Input.GetAxis("Mouse Y") * ySpeed * 0.2/Mathf.Sin(theta*Mathf.Deg2Rad)*k_zoom;
        var tmp=Quaternion.Euler(0, fai, 0)*Vector3(-posex,0,-posey);
        tarx+=tmp.x;
        tary+=tmp.y;
        tarz+=tmp.z;
        lockon=false;
        //print("Tar x "+tarx+"tary "+tary+" tarz "+tarz);
	}
	else
    if(Input.GetMouseButton(3)||Input.GetKey(KeyCode.Q))
	{
		fai += Input.GetAxis("Mouse X") * xSpeed * 0.2;
		theta -= Input.GetAxis("Mouse Y") * ySpeed * 0.2;
		theta = ClampAngle(theta, yMinLimit, yMaxLimit);
	}
	var rotation = Quaternion.Euler(theta, fai, 0);

	var position = rotation * Vector3(0.0, 0.0, -dis) + Vector3(tarx,tary,tarz);

	transform.rotation = rotation;
	transform.position = position;
}
function setTargetx(x)
{
    tarx=x;
}
function setTargety(y)
{
    tary=y;
}
function setTargetz(z)
{
    tarz=z;
}
static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}
