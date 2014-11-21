using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class scale : MonoBehaviour 
{

    // Use this for initialization
    public float scalef=1.0f,high=10;
    public float emSize=5.0f;
    public float emis=25.0f;
    private void adaptSmoke(float s)
    {
        /*
        var obj=GameObject.Find(a);
        
            var pe=obj.GetComponent("Ellipsoid Particle Emitter");
        print(obj);
        print(pe);

        pe.particleEmitter.maxSize = 17.5*s;
        pe.particleEmitter.maxEnergy=5*s;
        pe.particleEmitter.maxEmission=25*s;
        */
        ParticleEmitter[] psarr = this.GetComponentsInChildren<ParticleEmitter>();
        for (int i = 0; i < psarr.Length; i++ )
        {
            ParticleEmitter p=psarr[i];
            p.maxSize = emSize*s;
            p.minSize=emSize*0.75f*s;
            p.minEnergy = emSize*s;
            p.maxEnergy = emSize*s;

            //p.maxEnergy=5.0f*s;
            p.maxEmission=emis*s;
            p.minEmission=emis*s;
        }
    }
    void Start () {

    }
    void OnMouseDown()
    {
        print("MouseDown");
        RTSCameraCon.setTarget(transform);
    }

    // Update is called once per frame
    void adapt (float s) 
    {
        //RTSCamera c = (RTSCamera)gameObject.GetComponent<RTSCamera>();
        float tmp_scale=s;
        transform.localScale=(new Vector3(1.0f,1.0f,1.0f))*tmp_scale*scalef;
        transform.position=new Vector3(transform.position.x,tmp_scale*scalef*high,transform.position.z);
        //adaptSmoke(tmp_scale*scalef);
	}
    
	void Update () 
    {
        //RTSCamera c = (RTSCamera)gameObject.GetComponent<RTSCamera>();
        float tmp_scale = RTSCameraCon.k_zoom;
        transform.localScale=(new Vector3(1.0f,1.0f,1.0f))*tmp_scale*scalef;
        transform.position=new Vector3(transform.position.x,tmp_scale*scalef*high,transform.position.z);
        adaptSmoke(tmp_scale*scalef);
    }
    void OnTap(Gesture sender)
    {
        RTSCameraCon.setTarget(transform);
    }

}
