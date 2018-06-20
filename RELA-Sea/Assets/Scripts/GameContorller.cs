using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;




public class GameContorller : MonoBehaviour {


  public Transform BallGlowing;
	public Transform BallNormal;
	public GameObject LocationObject;
	public GameObject LocationObject2;
	public GameObject LocationObject3;
	public GameObject LocationObject4;
	public GameObject LocationObject5;

	public float BallDistance;
	public GameObject BallHolderObject;
	GameObject BallToMove;
	GameObject BallToMove1;
	GameObject BallToMove2;
	GameObject BallToMove3;
	GameObject BallToMove4;
	GameObject BallToMove5;
	GameObject BallToMove6;
	GameObject BallToMove7;

	public float MeditationEndTime = 630.0f;
	float endTimer = 0.0f;
	float suffleTimer = 0.0f;
	public float suffleFrequency = 30.0f;

	/*private UnitySerialPort unitySerialPort;

	[DllImport ("inpoutx64")]
	private static extern short Out32(short port, short message);
*/
	

	// Use this for initialization
	void Start () {
	
		
				/*unitySerialPort = UnitySerialPort.Instance;*/


				/* NEW WAY OF CREATING BALLS AND SUFFLING THEM 

		int chosenLocation = UnityEngine.Random.Range (1, 5);

		//Create normal balls, 


		for (int i=0; i<5; i++) {

			
			float x = LocationObject.transform.position.x;
			float y = LocationObject.transform.position.y;
			float z = LocationObject.transform.position.z;



			var ballToInstantiate = Instantiate(BallNormal, new Vector3 (x,y,z), Quaternion.identity);
			ballToInstantiate.name = "SphereNormal(Clone)" + i;
				}
		*/



/*  OLD WAY OF GENERATING THE BALLS AND THEY DO NOT MOVE. NEW WAY ABOVE. */
				int chosenLocation = UnityEngine.Random.Range (1, 5);
				int chosenCounter = 1;

	
				float x = LocationObject.transform.position.x;
				float y = LocationObject.transform.position.y;
				float z = LocationObject.transform.position.z;

				//create balls
				for (int i=0; i<5; i++) {	
						x += BallDistance;
						if (chosenCounter == chosenLocation) { 		
								Instantiate (BallGlowing, new Vector3 (x, y, z), Quaternion.identity);

								var ballToInstantiate = Instantiate (BallNormal, new Vector3 (x, y, z), Quaternion.identity);
								ballToInstantiate.name = "SphereNormal(Clone)" + i;
			
		
								chosenCounter++;
						} else {
								var ballToInstantiate = Instantiate (BallNormal, new Vector3 (x, y, z), Quaternion.identity);
								ballToInstantiate.name = "SphereNormal(Clone)" + i;

								chosenCounter++;


						}

				}


				// UUGLY HACK DON'T DO IT LIKE THIS:

				if (GameObject.Find ("SphereSpecial(Clone)") != null) {
						BallToMove = GameObject.Find ("SphereSpecial(Clone)");
						BallToMove.transform.parent = BallHolderObject.transform;
				}

				/*if (GameObject.Find ("SphereNormal(Clone)") != null) {
			BallToMove1 = GameObject.Find ("SphereNormal(Clone)");
			BallToMove1.transform.parent = BallHolderObject.transform;
		}*/

				if (GameObject.Find ("SphereNormal(Clone)0") != null) {
						var BallToMove2 = GameObject.Find ("SphereNormal(Clone)0");
						BallToMove2.transform.parent = BallHolderObject.transform;
				}

				if (GameObject.Find ("SphereNormal(Clone)1") != null) {
						var BallToMove3 = GameObject.Find ("SphereNormal(Clone)1");
						BallToMove3.transform.parent = BallHolderObject.transform;
				}
				if (GameObject.Find ("SphereNormal(Clone)2") != null) {
						var BallToMove4 = GameObject.Find ("SphereNormal(Clone)2");
						BallToMove4.transform.parent = BallHolderObject.transform;
				}
				if (GameObject.Find ("SphereNormal(Clone)3") != null) {
						var BallToMove5 = GameObject.Find ("SphereNormal(Clone)3");
						BallToMove5.transform.parent = BallHolderObject.transform;
				}
				if (GameObject.Find ("SphereNormal(Clone)4") != null) {
						var BallToMove6 = GameObject.Find ("SphereNormal(Clone)4");
						BallToMove6.transform.parent = BallHolderObject.transform;
				}
				/*if (GameObject.Find ("SphereNormal(Clone)5") != null) {
			BallToMove7 = GameObject.Find ("SphereNormal(Clone)5");
			BallToMove7.transform.parent = BallHolderObject.transform;
		}	 */


//*/
		
// SEND START SIGNAL TO SERUAL PORT (COM7 set in GUI)
				/*			try {

						unitySerialPort.OpenSerialPort (); 
						for (int j=0; j<256; j++) {
								string serialmessage = j.ToString ();
								unitySerialPort.SendSerialDataAsLine (serialmessage);
						}
				} catch (Exception ex2) {
						// Failed to send serial data
						Debug.Log ("failed to send to serial port");				
				}	

//AND TRY TO SEND IT TO PARALLER PORT
	
				for (short j=0; j<16; j++) {
						
					Out32 (888, j);  //tries to write to LPT1 (0x378 = 888);
					
						
				}
	}
*/

		}


		
		
	public void StartBalls() {
		Application.LoadLevel (1);
	}

	public void StartBody() {
		Application.LoadLevel (2);
	}



	
	// Update is called once per frame
	void Update () {


		//here we move the glowing ball around.
		suffleTimer += Time.deltaTime;
		if ((suffleTimer > suffleFrequency) && (endTimer > 31)){   //31 is higher than the movement starting time in platform mover script. Ugly.
			suffleTimer = 0.0f;	
			int suffleLocation = UnityEngine.Random.Range (1, 5);
			BallToMove = GameObject.Find ("SphereSpecial(Clone)");

			if (suffleLocation == 1 ){
				BallToMove2 = GameObject.Find ("SphereNormal(Clone)0");
				BallToMove.transform.position = BallToMove2.transform.position;
			}

			if (suffleLocation == 2) {
				BallToMove2 = GameObject.Find ("SphereNormal(Clone)1");
				BallToMove.transform.position = BallToMove2.transform.position;
			}

			if (suffleLocation == 3) {
				BallToMove2 = GameObject.Find ("SphereNormal(Clone)2");
				BallToMove.transform.position = BallToMove2.transform.position;
			}

			if (suffleLocation == 4){
				BallToMove2 = GameObject.Find ("SphereNormal(Clone)3");
				BallToMove.transform.position = BallToMove2.transform.position;
			}

			if (suffleLocation == 5){
				BallToMove2 = GameObject.Find ("SphereNormal(Clone)4");
				BallToMove.transform.position = BallToMove2.transform.position;
			}
			

		}



		endTimer += Time.deltaTime;

		if (endTimer > MeditationEndTime){
			
			Application.LoadLevel (5);
			
		}


		if (endTimer > MeditationEndTime-1)
			{
				//TRY TO SEND IT TO PARALLER PORT
			/*
					for (short j=0; j<256; j++)
					{
						Out32 (888, j);  //tries to write to LPT1 (0x378 = 888);
					}

			
			try{
				unitySerialPort.OpenSerialPort(); 
				for (int j=0; j<256; j++)
				{
					string serialmessage = j.ToString();
					unitySerialPort.SendSerialDataAsLine(serialmessage);
				}
				}
				catch (Exception ex5)
				{
					Debug.Log("Failed To send to serial ");
					
				}*/
			}



	



		if (Input.GetKeyDown ("f1")) {
				Application.LoadLevel (0);
					
		}

		if (Input.GetKeyDown ("f2")) {
			Application.LoadLevel (1);
			
		}

		if (Input.GetKeyDown ("f3")) {
			Application.LoadLevel (2);
			
		}

		if (Input.GetKeyDown ("f4")) {
			Application.LoadLevel (3);
			
		}

		if (Input.GetKeyDown ("f5")) {
			Application.LoadLevel (4);
			
		}

		if (Input.GetKeyDown ("f6")) {
			Application.LoadLevel (6);
			
		}
	
	}
}
