using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Globalization;

public class EventsHandler: MonoBehaviour{
	// THIS IS THE CORE OF THE ENTIRE GAME
		
//	public static Dictionary<string,airobj> aircluster= new Dictionary<string,airobj>();
//	private Dictionary<int[], Terrain> terrains = publicvar.terrains;
	private Dictionary<string, airobj> airs;
	private AirManager airmanager;
	private publicvar publicv;
	private HeightmapLoader heightmaploader;
	public GameObject originairplane;
	
	void Start(){
	// 1. register the events	

		connect.connected += this.OnConnected;
		connect.report += this.OnReport;
		connect.receiveGamedata += this.OnReceiveGamedata;

//		publicv = gameObject.AddComponent<publicvar> ();

	// 2. Initialize Global varibles
		try{
			publicv = GameObject.Find ("publicvar").GetComponent<publicvar> ();
		}catch (Exception ex){
			print("Gameobject named publicvar not found! \n  " +ex.Message);
		}

		if (publicv.originairplane1 == null) {
			publicv.originairplane1 = this.originairplane;	
			}

		int i =map.lnglatToXY(publicvar.longitude,publicvar.latitude,publicvar.zoom)[0];
		int j=  map.lnglatToXY(publicvar.longitude,publicvar.latitude,publicvar.zoom)[1];
		publicvar.basei = i;
		publicvar.basej = j;
		Debug.Log ("basei ,basej: " + i + " " + j);

	// 3. Initialize Air manager
//		airmanager = gameObject.AddComponent<AirManager> ();
		airmanager = GameObject.Find ("AirManager").GetComponent<AirManager> ();
		airmanager.originair = this.originairplane;
		airmanager.airs = publicv.airs;

	// 4. Initialize HeightmapLoader
//		heightsloader = gameObject.AddComponent<HeightmapLoader> ();
		heightmaploader = GameObject.Find ("HeightmapLoader").GetComponent<HeightmapLoader> ();

		StartCoroutine (Startloadheightmap());

//		createtestair ();

	}
//
//	public void createtestair(){
//
//		airobj airplane = new airobj(GameObject.Find("airplane"));
//	}

	IEnumerator Startloadheightmap(){
		yield return new WaitForSeconds (8);
		heightmaploader.Startload ();
		yield break;
	}
	
	void OnEnable(){
		// register the events	
		connect.connected += this.OnConnected;
		connect.report += this.OnReport;
		connect.receiveGamedata += this.OnReceiveGamedata;
	}

	void OnConnected(JsonData data){

	}

	void OnReport(JsonData data){
		
	}

	void OnReceiveGamedata(JsonData jd){
	
		airmanager.UpdateOrCreate (jd);
		

	}

	void OnDisable(){
		//unregister the events
		connect.connected -= this.OnConnected;
		connect.report -= this.OnReport;
		connect.receiveGamedata -= this.OnReceiveGamedata;
	}

	void OnDestroy(){
		//unregister the events
		connect.connected -= this.OnConnected;
		connect.report -= this.OnReport;	
		connect.receiveGamedata -= this.OnReceiveGamedata;
	}

}