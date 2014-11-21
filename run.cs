using UnityEngine;
using System.Collections;
public class run : MonoBehaviour {
    private float theta = 0;
    public float omgea = 0.1f;
    public float r0 = 6;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        theta += omgea;
        float r = r0+r0/2*Mathf.Cos(theta/10);
        transform.position= new Vector3 ( Mathf.Cos(theta)*r ,0, Mathf.Sin(theta)*r);
        transform.rotation=Quaternion.Euler(0,180-theta*Mathf.Rad2Deg,0)*Quaternion.Euler(0,0,-30);
	
	}
}
