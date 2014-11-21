using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Globalization;

public class EventsHandler: MonoBehaviour{
		
//	public static Dictionary<string,airobj> aircluster= new Dictionary<string,airobj>();
//	private Dictionary<int[], Terrain> terrains = publicvar.terrains;
	private Dictionary<string, airobj> airs = publicvar.airs;
	private AirManager airmanager;

	public GameObject originairplane;





	void Start(){
		// register the events	
		connect.connected += this.OnConnected;
		connect.report += this.OnReport;
		connect.receiveGamedata += this.OnReceiveGamedata;

		airmanager = new AirManager{
			originair = originairplane,
			airs = this.airs
		};

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