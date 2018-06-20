using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Platform_mover : MonoBehaviour {
	public GameObject eeg_holder;		// to find the raw data. Not needed.
	public float eegCheckFrequency = 0.1f; //how often we check the speed from eeg.
			

	public float eegSpeedAdjusted = 1.0f; // the starting speed. Tells the actual movement speed in the scene.
	public float maxSpeed = 10.0f;		//the actual max speed you can move at.

	public float maxHeight = 50;			//how high you can fly, does not take in account the acceleration, actual will be lower. Values 1-30 are fine
	public float TrueMaxHeight = 20.0f;	// Actual upper limit. The other is used for speed calculations.
	public float minHeight = 3.29f;		//start height. Check from the scene.
	public float MoveStartTime = 30.0f;	//when will the test begin. Could put a counter here. Seconds.
	public float MoveEndTime = 660.0f;	//when will the test end. Test ends here. Maybe indicate with something. Seconds.
	public float TestEndTime = 665.0f; // when test will really end, white screen.
	
	public float UpCheckTick = 5.0f;	// how of ofhen the uptick is counted. Seconds.
	public float UpCheckTreshold = 5.0f;  // treshold to increase the speed. Not relative, tested that same as tick works well.
	public float UpSpeedIncrease = 3.2f;	// and how much it changes.	 Upspeed is good to be slightly higher than down seepd.
	public float UpBuffer = 1.5f;		//the speed buffer that resists the downward movement, multiplied maxspeed. 
		
	public float DownCheckTick = 5.0f;  	//How often downward tick is calculated. Rarer than uptick.
	public float DownCheckTreshold = 3.0f;	// and it's treshold. Needs testting, depending on test results.
	public float DownSpeedIncrease = 3.0f;  // keep it about the same as upSpeedIncrease
	public float DownBuffer = 1.0f;		//speed buffer that resist the upwardmovement, multiplied max speed. 1.0 = no effect) 


	public float BeginBoostStrength = 5.0f; //how much the beginning boost adds to uptick speed increases.
	public float BeginBoostLength = 60.0f; //length of beginning boost.
	


	public GameObject EnergyBubble;		//find the bubble object.


	public float WhiteCheckTick = 15.0f;
	public float WhiteCheckTreshold = 10.0f;

	public float BlackCheckTick = 10.0f;
	public float BlackCheckTreshold = 5.0f;
	

	public float WhiteOpacityMax = 1.7f;
	public float WhiteOpacityMin = 0.0f;
	public float opacityChange = 0.05f;

	public float WhiteOpacity = 0.0f;
	public float WhiteAdjustment = 0.0f; //added to the eeg feed of bubble channel to adjust the zero point.

	float goingBlackTimer = 0.0f;
	float goingBlackNode = 0.0f;
	float goingWhiteTimer = 0.0f;
	float goingWhiteNode = 0.0f;

	public GameObject SparkleSystem;
	public float SparkleTreshold = 0.6f;

	float writeTimer = 0.0f;
	public string path = @"c:\temp\MyTest.txt";

	public int AdaptationOn = 0; // 1 for true- runs with real data, 0 for false - runs with sin curve simulation. 




	// Internal variables.

	public float eegSpeedRaw = 0.0f;	//used in speed calculations
	float eeg2;
	float eeg3;
	float goingUpTimer = 0.0f;
	float goingUpNode = 0.0f;
	float goingDownTimer = 0.0f;
	float goingDownNode = 0.0f;
	bool movementDirection = true;
	float heightMultiplier;
	float beginCountTimer = 0.0f;
	float countTimer = 0.0f;
	float timer = 90.0f;
	string testType = "not defined yet";
	bool headerWritten = false;

	DateTime saveTimeNow;
	string path1;
	string path2;
	string headerToWrite;
	string StateToWrite;
	string simulationToWrite = " simulated data";
	string SaveFileName = "testname.txt";
	//GameObject findMe;



	void Start (){

				headerWritten = false;
				WhiteOpacity = 0.0f;
	
				if (PlayerPrefs.HasKey ("AdaptationOnStored")) {
						AdaptationOn = PlayerPrefs.GetInt ("AdaptationOnStored");		
						Debug.Log (AdaptationOn + "Adaptation from save");
				}

		
				if (PlayerPrefs.HasKey ("SceneHeightStored")) {
						maxHeight = PlayerPrefs.GetFloat ("SceneHeightStored");		
						Debug.Log (maxHeight + "max Height from save");
				}

				if (PlayerPrefs.HasKey ("RiseStrengthtStored")) {
						UpCheckTreshold = PlayerPrefs.GetFloat ("RiseStrengthtStored");		
						Debug.Log (UpCheckTreshold + "UpCheckTreshold from save");
				}

				if (PlayerPrefs.HasKey ("FallStrengthtStored")) {
						DownCheckTreshold = PlayerPrefs.GetFloat ("FallStrengthtStored");	
						Debug.Log (DownCheckTreshold + "DownCheckTreshold from save");
				}

				if (PlayerPrefs.HasKey ("WhiteStrengthtStored")) {
						WhiteCheckTreshold = PlayerPrefs.GetFloat ("WhiteStrengthtStored");	
						Debug.Log (WhiteCheckTreshold + "WhiteCheckTreshold from save");
				}

				if (PlayerPrefs.HasKey ("BlackStrengthtStored")) {
						BlackCheckTreshold = PlayerPrefs.GetFloat ("BlackStrengthtStored");	
						Debug.Log (BlackCheckTreshold + "BlackCheckTreshold from save");
				}

				if (PlayerPrefs.HasKey ("WhiteAdjustmentStored")) {
						WhiteAdjustment = PlayerPrefs.GetFloat ("WhiteAdjustmentStored");	
						Debug.Log (WhiteAdjustment + "WhiteAdjustement from save");
				}

				if (PlayerPrefs.HasKey ("SaveFileNameStored")) {
						SaveFileName = PlayerPrefs.GetString ("SaveFileNameStored");		
						Debug.Log (SaveFileName + "SaveFileName from save");
				}
		
				path1 = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Desktop) + "/RelaWorldData";
				path2 = path1 + "/" + SaveFileName;
	
				if (AdaptationOn == 1) {
						simulationToWrite = " ";
				}

				if (AdaptationOn == 0) {   


						// VARIABLE RESET FOR SIMULATION STARTS. SIMULATION IS NOT EFFECTED BY ENTRY VARIABLES (too much variance)
						// CHECK AND FIX THESE FOR THE SIMULATION TO WORK WELL.
			
						eegSpeedAdjusted = 1.0f; // the starting speed. Tells the actual movement speed in the scene.
						maxSpeed = 5.0f;		//the actual max speed you can move at.
			
						maxHeight = 70;			//how high you can fly, does not take in account the acceleration, actual will be lower. Values 1-30 are fine
						TrueMaxHeight = 20.0f;	// Actual upper limit. The other is used for speed calculations.
						minHeight = 3.29f;		//start height. Check from the scene.
						MoveStartTime = 30.0f;	//when will the test begin. Could put a counter here. Seconds.
						MoveEndTime = 660.0f;	//when will the test end. Test ends here. Maybe indicate with something. Seconds.
						TestEndTime = 665.0f; // when test will really end, white screen.
			
						UpCheckTick = 5.0f;	// how of ofhen the uptick is counted. Seconds.
						UpCheckTreshold = 3.0f;  // treshold to increase the speed. Not relative, tested that same as tick works well.
						UpSpeedIncrease = 3.4f;	// and how much it changes.	 Upspeed is good to be slightly higher than down seepd.
						UpBuffer = 1.0f;		//the speed buffer that resists the downward movement, multiplied maxspeed. 
			
						DownCheckTick = 5.0f;  	//How often downward tick is calculated. Rarer than uptick.
						DownCheckTreshold = 5.0f;	// and it's treshold. Needs testing, depending on test results.
						DownSpeedIncrease = 3.3f;  // keep it about the same as upSpeedIncrease
						DownBuffer = 1.0f;		//speed buffer that resist the upwardmovement, multiplied max speed. 1.0 = no effect) 
			
			
						BeginBoostStrength = 5.0f; //how much the beginning boost adds to uptick speed increases.
						BeginBoostLength = 60.0f; //length of beginning boost.

						WhiteCheckTick = 20.0f;
						WhiteCheckTreshold = 70.0f;
			
						BlackCheckTick = 5.0f;
						BlackCheckTreshold = 1.0f;
			
			
						WhiteOpacityMax = 0.8f;
						WhiteOpacityMin = 0.0f;
						opacityChange = 0.03f;
			
						WhiteOpacity = 0.0f; //starting value
						WhiteAdjustment = 0.0f;
						//*/
						// SIMULATION VARIABLE RESET ENDS


				}
		}

void Update (){  	//here we have our simulated sin-curve-data generation.


		// The random generators to be used when adaptation is off. Might need default value adjustments. Check at lab. Reset the variables to defaults.
		timer += Time.deltaTime;
		if (AdaptationOn == 0) {
						eeg2 = ((Mathf.Sin (timer * Mathf.PI * 2 / 120)) * 0.8f + UnityEngine.Random.Range (-0.2f, 0.2f));
						eeg3 = ((Mathf.Sin (timer * Mathf.PI * 2 / 120)) * 0.8f + UnityEngine.Random.Range (-0.2f, 0.2f));
		
						Debug.Log (eeg2);
						Debug.Log (eeg3);
				}


		//calculating limits. 
		heightMultiplier = (maxHeight - minHeight) / (MoveEndTime - MoveStartTime) /maxSpeed;  
		// idea of this was that if we use different scenes with different flying heights and durations, 
		// all of them would feel relatively same so that movement speed would scale so that max heights could be reached in all.
		// now that we have only one scene, this just works as a adjustment multiplier.


		//calculating when the beginning boost will end.
		beginCountTimer += Time.deltaTime;
		if (beginCountTimer > BeginBoostLength + MoveStartTime) {
			BeginBoostStrength = 0.0f;	
			}

		if (GameObject.Find ("BallSceneIndentifier") != null) {
			testType = "Body Scan";
		} else { testType = "Focused Attention"; } 
		

		if (Input.GetKeyDown ("l")) {
			if (AdaptationOn == 1) AdaptationOn = 0; else AdaptationOn = 1;
			
		}
		

	}


	// Update is called once per frame
	void FixedUpdate () {


				// HEIGHT MOVEMENT STARTS HERE
				// all is calculated on fixed upadate, if that is not machine-independent, things have to change.
				//fixed the count to be independet from timers. Seems to work better like this. Rethink.

				if (AdaptationOn == 1) {	//Here we get the real data from EEG machine.

						eeg2 = eeg_data.eeg1;  	
						eeg3 = eeg_data.eeg2;

						Debug.Log (eeg2 + "EEG1");
						Debug.Log (eeg3 + "EEG2");
				}

				//Checking for going up!
				goingUpTimer += Time.deltaTime;
				if (eeg2 > 0) {		// we add the positve results of the meditation.
						goingUpNode += eeg2;
				}
				if (goingUpTimer > UpCheckTick) {		// we reset the positive results every five seconds.
						goingUpNode = 0.0f;
						goingUpTimer = 0.0f;
				}
			
	
		
				if (goingUpNode > UpCheckTreshold) {							// if during those five seconds, result is over the threshold
					
						eegSpeedRaw += UpSpeedIncrease + BeginBoostStrength; //}		// we increase the speed movement
						//Debug.Log(eegSpeedRaw + " GO UP");
						goingUpNode = 0.0f;		// and reset
						goingUpTimer = 0.0f;
				}


				//Checking for going down!
				goingDownTimer += Time.deltaTime;
				if (eeg2 < 0) {								// we add the negative results of the meditation.
						goingDownNode -= eeg2;
				}
				if (goingDownTimer > DownCheckTick) {		// we reset the negative results every five seconds.
						goingDownNode = 0.0f;
						goingDownTimer = 0.0f;
				}	
		
		
				if (goingDownNode > DownCheckTreshold) {		// if during those five seconds, result is over threshold
						eegSpeedRaw -= DownSpeedIncrease;			// we decrease the speed of the movement
						//	Debug.Log(eegSpeedRaw + " GO DOWN");
			
						goingDownNode = 0.0f;						// and reset
						goingDownTimer = 0.0f;
		
				}
				





				// DO NOT REMOVE. THEY ACTUALLY DO SIGNIFICANTLY GOOD STUFF! ACT AS SPEED LIMITERS. IMPORTANT :D
				// THIS IS PROBABPLY THE PLACE OF THE BUG. WHY? DO THESE CONDITIONS EVER HAPPEN?
				// checking speed buffers so that small distractions won't change the movement direction.
				// found that using multiplier instead of adding seems more intuitive, keep the numbers small).
		
				if (eegSpeedRaw > maxSpeed * UpBuffer) {
						eegSpeedRaw = maxSpeed * UpBuffer;
				}
		
				if (eegSpeedRaw < -maxSpeed * DownBuffer) {
						eegSpeedRaw = -maxSpeed * DownBuffer;
				}
				
				//TO MAKE THE BUFFER:
				//INTEGRATE THIS TO THE MOVEMENT CHECKS AND TEST THE LIMITS.	
				/*if (eegSpeedRaw < -0.9 * maxSpeed) {							// If we are moving fast down
				eegSpeedRaw += UpSpeedIncrease * 0.5 + BeginBoostStrength;	// it is harder to get back up 	
				} else {	
		 		*/	




				// Check that if movement slows down to zero, then we change the movement direction.
				if (eegSpeedRaw < 0) {
						movementDirection = false;
						//				Debug.Log(movementDirection);
				}
				if (eegSpeedRaw > 0) {
						movementDirection = true;
						//			Debug.Log(movementDirection);
				}



	

				// this is where we change the reference speed to be adjusted to work with the max height
				// related to the time of the meditation, 
				//so that at maximum performance, 
				//you'll reach the maximum height
				//at the end of the meditation. (does not take in account accelelration, so endresult is somewhat lower)
				//also checks that you'll never go above the max speed, but this seems to bug)

				//Debug.Log(eegSpeedRaw + "raw speed");
				eegSpeedAdjusted = Mathf.Abs (eegSpeedRaw) * heightMultiplier; // we do itseisarvo, and correction based on maxspeed and max distance travelled.
				if (eegSpeedAdjusted > maxSpeed) {
						eegSpeedAdjusted = maxSpeed;  // this doesn't really work. eegSpeedAdjusted is in the range of -2 - 2, limiter is in the eegSpeedRaw
				}
				//Debug.Log(eegSpeedAdjusted +" movement speed");



				//here we do the actual movement
				countTimer += Time.deltaTime;

				if (countTimer > MoveStartTime) {
					


						if ((gameObject.transform.position.y < TrueMaxHeight) && (movementDirection == true)) {
								transform.Translate (Vector3.up * eegSpeedAdjusted * Time.deltaTime);
						}

						if ((gameObject.transform.position.y > minHeight) && (movementDirection == false)) {
								transform.Translate (Vector3.down * eegSpeedAdjusted * Time.deltaTime);
						}
				}

				//	if (gameObject.transform.position.y > RealMaxHeight)
				//					gameObject.transform.position.y = RealMaxHeight;
				// HEIGHT MOVEMENT ENDS HERE


				// BUBBLE GLOW STARTS HERE
				//Checking for going up!

				//keeping it at zero in the beginning.
				if (countTimer < MoveStartTime) {
					WhiteOpacity = 0.0f;
			
				}

				goingWhiteTimer += Time.deltaTime;
				if (eeg3 + WhiteAdjustment > 0) {		// we add the positve results of the meditation.
						goingWhiteNode += eeg3;
				}
				if (goingWhiteTimer > WhiteCheckTick) {		// we reset the positive results every five seconds.
						goingWhiteNode = 0.0f;
						goingWhiteTimer = 0.0f;
				}
		
		
		
				if (goingWhiteNode > WhiteCheckTreshold) {			// if during those five seconds, result is over the threshold
						WhiteOpacity += opacityChange;									//add to transparency amount of change
						//		Debug.Log(eegSpeedRaw + " Whiter");
						goingWhiteNode = 0.0f;		// and reset
						goingWhiteTimer = 0.0f;
				}


				goingBlackTimer += Time.deltaTime;
				if (eeg3 + WhiteAdjustment < 0.0f) {		// we add the negative results of the meditation.
						goingBlackNode -= eeg3;
				}
				if (goingBlackTimer > BlackCheckTick) {		// we reset the negative results every five seconds.
						goingBlackNode = 0.0f;
						goingBlackTimer = 0.0f;
				}
		
		
		
				if (goingBlackNode > BlackCheckTreshold) {		// if during those five seconds, result is over the threshold
						WhiteOpacity -= opacityChange;
						//	Debug.Log(eegSpeedRaw + " Blacker");
			
						goingBlackNode = 0.0f;		// and reset
						goingBlackTimer = 0.0f;
				}
	

				if (WhiteOpacity > WhiteOpacityMax) {
						WhiteOpacity = WhiteOpacityMax;
				}

				if (WhiteOpacity < WhiteOpacityMin) {
						WhiteOpacity = WhiteOpacityMin;
				}

				//end fade
				if (countTimer > TestEndTime + 3) {
						WhiteOpacityMax = 3;
						WhiteOpacity = 3;
				}

				

				//if (AdaptationOn == 0){
				//		WhiteOpacity = 0;  // FORCING THE BUBBLE TO BE TRANSPARENT IN ADAPTATION, UGLY HACK
				//		}

		EnergyBubble.GetComponent<Renderer>().material.color = new Color (256, 256, 256, WhiteOpacity);


		//Sparkle my friend!

		if (WhiteOpacity / WhiteOpacityMax > SparkleTreshold)
			SparkleSystem.SetActive (true);
		if (WhiteOpacity / WhiteOpacityMax < SparkleTreshold)
			SparkleSystem.SetActive (false);

		// particle system ends



		//FILE WRITING HAPPENS HERE. ONCE PER SECOND.
			
			writeTimer += Time.deltaTime;
			if (writeTimer > 1) {
				writeTimer -= writeTimer;	
				
				
				if (headerWritten == false) {
					
					if (Directory.Exists (path1)) {
					} else {
						DirectoryInfo di = Directory.CreateDirectory (path1);
					}
					
					saveTimeNow = System.DateTime.Now;				// HERE goes the session header.
					saveTimeNow.ToString ("yyyyMMddHHmmss");
					
					headerToWrite = "Starting recording new test: " + testType + " " + saveTimeNow + simulationToWrite + Environment.NewLine;
					Debug.Log (headerToWrite);
					System.IO.File.AppendAllText (path2, headerToWrite);
					headerWritten = true;
					
				} else {  //HERE we write actual data
					
					saveTimeNow = System.DateTime.Now;
					saveTimeNow.ToString ("HHmmss");
					
					var HeightTemp = gameObject.transform.position.y.ToString ();
					var WhiteTemp = WhiteOpacity.ToString ();
					
					StateToWrite = testType + " " + saveTimeNow + " " + HeightTemp + " " + WhiteTemp + simulationToWrite + Environment.NewLine;
					Debug.Log (testType + path2 + StateToWrite);
					System.IO.File.AppendAllText (path2, StateToWrite);
				}
			} 
			
			/*'
		System.Environment.GetFolderPath( (System.Environment.SpecialFolder)values.GetValue( i ) ) 
		System.IO.File.WriteAllText("C:\temp\cachefile.txt", "This is text that goes into the text file");
		DateTime saveTimeNow = System.DateTime.Now;
		saveTimeNow.ToString ("yyyyMMddHHmmss");
		Debug.Log (saveTimeNow);
		saveTimeNow.Trim ();
		*/
			
			
	}
		
		
		
		
		
		
	}
	