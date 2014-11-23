using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Globalization;

public class EventsHandler: MonoBehaviour{
		
//	public static Dictionary<string,airobj> aircluster= new Dictionary<string,airobj>();
//	private Dictionary<int[], Terrain> terrains = publicvar.terrains;
	private Dictionary<string, airobj> airs;
	private AirManager airmanager;
	private publicvar publicv;
	private HeightmapLoader heightsloader;
	public GameObject originairplane;
	
	void Start(){
		// register the events	
		connect.connected += this.OnConnected;
		connect.report += this.OnReport;
		connect.receiveGamedata += this.OnReceiveGamedata;

		publicv = gameObject.AddComponent<publicvar> ();
		publicv.originairplane1 = this.originairplane;

		airmanager = gameObject.AddComponent<AirManager> ();
		airmanager.originair = this.originairplane;
		airmanager.airs = publicv.airs;

		heightsloader = gameObject.AddComponent<HeightmapLoader> ();
		heightsloader.terrainstoload = new Queue ();

		StartCoroutine (Startloadheightmap());
	}

	IEnumerator Startloadheightmap(){
		yield return new WaitForSeconds (10);
		heightsloader.Startload ();
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