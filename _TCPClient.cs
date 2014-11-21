using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class _TCPClient : MonoBehaviour
{
	internal Boolean socketReady = false;

	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	String Host = "localhost";
	Int32 Port = 1026;

	private String theLine;


	void Start (){
		setupSocket ();

	}
	void Update (){
		theLine = readSocket ();
		if (theLine == "") {
				writeSocket ("line get!---:" + theLine);
		}
	}

	void OnDestroy(){
		closeSocket ();
	}
// --------------------------------------

	public void setupSocket(){
		try{
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}
		catch (Exception e){
			Debug.Log("Socket Setup error: " + e);
		}
	}

	public void writeSocket(string theLine){
	// theLine only contains contents 
		if (!socketReady)
			return;
		String foo = theLine + "\r\n";
		theWriter.Write(foo);
		theWriter.Flush();
	}

	public String readSocket() {
		if (!socketReady)
			return "";
		if (theStream.DataAvailable)
			return theReader.ReadLine();
		return "";
	}

	public void closeSocket() {
		if (!socketReady)
			return;
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		socketReady = false;
	}
}

