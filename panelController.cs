using UnityEngine;
using System.Collections;

public class panelController : MonoBehaviour
{
	public float x=-300;
	public float y=200;
	public float w=500;
	public float h=300;
	public GameObject panelpref;
//	public UISprite.AnchorPoint leftAnchorPoint;
//	public UISprite.AnchorPoint bottomAnchorPoint;
	public Transform startPoint;

		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

	public void OnMouseDown(){
		GameObject panelClone;
		panelClone = GameObject.Find (panelpref.name +"(Clone)");
		if (panelClone != null) {
			Destroy(panelClone);
			return;
		}
		
		GameObject panelInstance;
		panelInstance = Instantiate (panelpref) as GameObject;
		Debug.Log (panelInstance.name);
		UITexture texture = panelInstance.GetComponent<UITexture> ();
//		texture.SetRect(x,y,w,h);
		
//		GameObject panel_obj = panelInstance.gameObject;
		//		Destroy (panelInstance);
	}
	
}

