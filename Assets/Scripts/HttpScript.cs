﻿using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;
using System;
public class HttpScript : MonoBehaviour {

    //static string URL = "http://52.78.35.138:8080";
    //static string data;

    //public static string GetDia(string s)
    //{
    //    data = s;
    //    if (s == null)
    //    {
    //        CGoogleplayGameServiceManager.Login();
    //        data = CGoogleplayGameServiceManager.GetNameGPGS();
    //    }
    //    // Create a request using a URL that can receive a post. 
    //    WebRequest request = WebRequest.Create(URL);
    //    // Set the Method property of the request to POST.
    //    request.Method = "POST";
    //    // Create POST data and convert it to a byte array.
    //    string postData = string.Format("email={0}", data);
    //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
    //    // Set the ContentType property of the WebRequest.
    //    request.ContentType = "application/x-www-form-urlencoded";
    //    // Set the ContentLength property of the WebRequest.
    //    request.ContentLength = byteArray.Length;
    //    // Get the request stream.
    //    Stream dataStream = request.GetRequestStream();
    //    // Write the data to the request stream.
    //    dataStream.Write(byteArray, 0, byteArray.Length);
    //    // Close the Stream object.
    //    dataStream.Close();
    //    // Get the response.
    //    WebResponse response = request.GetResponse();
    //    // Display the status.
    //    Debug.LogError(((HttpWebResponse)response).StatusDescription);
    //    // Get the stream containing content returned by the server.
    //    dataStream = response.GetResponseStream();
    //    // Open the stream using a StreamReader for easy access.
    //    StreamReader reader = new StreamReader(dataStream);
    //    // Read the content.
    //    string responseFromServer = reader.ReadToEnd();
    //    // Display the content.
    //    Debug.LogError(responseFromServer);
    //    // Clean up the streams.
    //    reader.Close();
    //    dataStream.Close();
    //    response.Close();
    //    if (responseFromServer == "None")
    //    {
    //        return "0";
    //    }
    //    return responseFromServer;
    //}

    // Use this for initialization
    void Start()
    {
    }
       
	
	// Update is called once per frame
	void Update () {
	
	}
}
