using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class AirManager : MonoBehaviour
{
	public Dictionary<string, airobj> airs = new Dictionary<string, airobj >();
	public GameObject originair;



	public airobj CreateAir(string name, GameObject originair, JsonData jd){
		return new airobj (name, originair, jd);
	}

	public int UpdateAir(JsonData jd){
		foreach (DictionaryEntry entry in jd) {
			string name = (string)entry.Key;
			JsonData data = (JsonData) entry.Value;
			airs[name].update(data);
			return 0;
		}
		return -1;

	}

	public int UpdateOrCreate(JsonData jd){
		foreach (DictionaryEntry entry  in jd) {
			string name = (string)entry.Key;
			JsonData data = (JsonData) entry.Value;
			if (airs.ContainsKey(name)) {
				UpdateAir(data);
			}		
			else{
				CreateAir(name, originair, data);
			}
		
			return 0;
		}
		return -1;
	}

	public void DestroyAir(string name){
		airs [name].destroy();
		airs [name] = null;
	}

	public int numAir(){
		return airs.Count;
	}

}

