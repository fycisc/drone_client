#pragma strict

var box:GameObject;

private var forceHead:Transform;
private var forceLeftAirfoil:Transform;
private var forceRightAirfoil:Transform;
private var forceLeftTailAirfoil:Transform;
private var forceRightTailAirfoil:Transform;

private var thisTransform:Transform;
private var thisRigidbody:Rigidbody;

private var _speed:float = 250.0f;

function Start () 
{
box = GameObject.Find("Plane");

forceHead = transform.Find("ForceHead");

forceLeftAirfoil = transform.Find("ForceLeftAirfoil");
forceRightAirfoil = transform.Find("ForceRightAirfoil");

forceLeftTailAirfoil = transform.Find("ForceLeftTailAirfoil");
forceRightTailAirfoil = transform.Find("ForceRightTailAirfoil");

thisTransform = this.transform;
thisRigidbody = this.rigidbody;
}

function FixedUpdate () 
{
thisRigidbody.AddForceAtPosition(thisTransform.forward * this._speed, forceHead.position);

thisRigidbody.AddForceAtPosition(thisTransform.up * 11.0f, forceLeftAirfoil.position);
thisRigidbody.AddForceAtPosition(thisTransform.up * 11.0f, forceRightAirfoil.position);

thisRigidbody.AddForceAtPosition(thisTransform.up * 5.0f, forceLeftTailAirfoil.position);
thisRigidbody.AddForceAtPosition(thisTransform.up * 5.0f, forceRightTailAirfoil.position);

if(Input.GetKey(KeyCode.W))
{
// 俯冲
thisRigidbody.AddForceAtPosition(thisTransform.up * 5.0f, forceLeftTailAirfoil.position);
thisRigidbody.AddForceAtPosition(thisTransform.up * 5.0f, forceRightTailAirfoil.position);
}
else if(Input.GetKey(KeyCode.S))
{
// 爬升
thisRigidbody.AddForceAtPosition(thisTransform.up * -5.0f, forceLeftTailAirfoil.position);
thisRigidbody.AddForceAtPosition(thisTransform.up * -5.0f, forceRightTailAirfoil.position);
}
else if(Input.GetKey(KeyCode.A))
{
// 左翻滚
thisRigidbody.AddForceAtPosition(thisTransform.up * -5.0f, forceLeftTailAirfoil.position);
thisRigidbody.AddForceAtPosition(thisTransform.up * 5.0f, forceRightTailAirfoil.position);
}
else if(Input.GetKey(KeyCode.D))
{
// 右翻滚
thisRigidbody.AddForceAtPosition(thisTransform.up * 5.0f, forceLeftTailAirfoil.position);
thisRigidbody.AddForceAtPosition(thisTransform.up * -5.0f, forceRightTailAirfoil.position);
}
}