using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_control : MonoBehaviour {
	public Camera CameraFacing;
	private Vector3 originalScale;

	float countTimer;
	public float fade_time;
	public float part_duration;


	public Image leg_feet_l;
	public float leg_feet_l_reveal;
	bool leg_feet_l_revealed = false;
	
	public Image leg_lower_l;
	public float leg_lower_l_reveal;
	bool leg_lower_l_revealed = false;

	public Image leg_knee_l;
	public float leg_knee_l_reveal;
	bool leg_knee_l_revealed = false;

	public Image leg_upper_l;
	public float leg_upper_l_reveal;
	bool leg_upper_l_revealed = false;


	public Image leg_feet_r;
	public float leg_feet_r_reveal;
	bool leg_feet_r_revealed = false;

	public Image leg_lower_r;
	public float leg_lower_r_reveal;
	bool leg_lower_r_revealed = false;
	
	public Image leg_knee_r;
	public float leg_knee_r_reveal;
	bool leg_knee_r_revealed = false;
	
	public Image leg_upper_r;
	public float leg_upper_r_reveal;
	bool leg_upper_r_revealed = false;


	public Image arm_hand_l;
	public float arm_hand_l_reveal;
	bool arm_hand_l_revealed = false;
	
	public Image arm_low_l;
	public float arm_low_l_reveal;
	bool arm_low_l_revealed = false;
	
	public Image arm_elbow_l;
	public float arm_elbow_l_reveal;
	bool arm_elbow_l_revealed = false;

	public Image arm_up_l;
	public float arm_up_l_reveal;
	bool arm_up_l_revealed = false;
	
	public Image arm_hand_r;
	public float arm_hand_r_reveal;
	bool arm_hand_r_revealed = false;
	
	public Image arm_low_r;
	public float arm_low_r_reveal;
	bool arm_low_r_revealed = false;
	
	public Image arm_elbow_r;
	public float arm_elbow_r_reveal;
	bool arm_elbow_r_revealed = false;
	
	public Image arm_up_r;
	public float arm_up_r_reveal;
	bool arm_up_r_revealed = false;


	public Image pelvis;
	public float pelvis_reveal;
	bool pelvis_revealed = false;

	public Image abadomen;
	public float abadomen_reveal;
	bool abadomen_revealed = false;

	public Image chest;
	public float chest_reveal;
	bool chest_revealed = false;

	public Image shoulder_l;
	public float shouder_l_reveal;
	bool shoulder_l_revealed = false;

	public Image shoulder_r;
	public float shoulder_r_reveal;
	bool shoulder_r_revealed = false;

	public Image neck;
	public float neck_reveal;
	bool neck_revealed = false;

	public Image head;
	public float head_reveal;
	bool head_revealed = false;

	public Image crown;
	public float crown_reveal;
	bool crown_revealed = false;

	public Image fullbody;
	public float fullbody_reveal;
	bool fullbody_revealed = false;




	void ShowImage (Image imageShown, float revealTime, bool shownAlready) {
	if ((countTimer > revealTime) && (countTimer < revealTime + fade_time)) {
		imageShown.gameObject.SetActive(true);
		if (shownAlready == false) {
			imageShown.CrossFadeAlpha(0.0f, 0.0f, false); 
			shownAlready = true;
		}
		imageShown.CrossFadeAlpha(1.0f, fade_time, false); 
		}
	if (countTimer > (revealTime + fade_time + part_duration)) {
		imageShown.CrossFadeAlpha(0.0f, fade_time, false); 
		}		
	}


 

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;

		fullbody.gameObject.SetActive(false);
		leg_feet_l.gameObject.SetActive(false);
		leg_feet_r.gameObject.SetActive(false);
		leg_lower_l.gameObject.SetActive(false);
		leg_lower_r.gameObject.SetActive(false);
		leg_knee_l.gameObject.SetActive(false);
		leg_knee_r.gameObject.SetActive(false);
		leg_upper_l.gameObject.SetActive(false);
		leg_upper_r.gameObject.SetActive(false);

		arm_hand_l.gameObject.SetActive(false);
		arm_low_l.gameObject.SetActive(false);
		arm_elbow_l.gameObject.SetActive(false);
		arm_up_l.gameObject.SetActive(false);
		shoulder_l.gameObject.SetActive(false);
		arm_hand_r.gameObject.SetActive(false);
		arm_low_r.gameObject.SetActive(false);
		arm_elbow_r.gameObject.SetActive(false);
		arm_up_r.gameObject.SetActive(false);
		shoulder_r.gameObject.SetActive(false);

		abadomen.gameObject.SetActive(false);
		chest.gameObject.SetActive(false);
		neck.gameObject.SetActive(false);
		head.gameObject.SetActive(false);
		crown.gameObject.SetActive(false);
		pelvis.gameObject.SetActive (false);


		countTimer = 0.0f;
		fade_time = 2.0f;
	}


	// Update is called once per frame
	void Update () {


		countTimer += Time.deltaTime;


		//Here we showturn the canvas to look at the camera,
		RaycastHit hit;
		float distance;
		if (Physics.Raycast (new Ray (CameraFacing.transform.position,
		                              CameraFacing.transform.rotation * Vector3.forward),
		                     out hit)) {
			distance = hit.distance;
		} else {
			distance = CameraFacing.farClipPlane * 0.95f;
		}
		transform.position = CameraFacing.transform.position +
						CameraFacing.transform.rotation * Vector3.forward * 1.0f; //distance;
		transform.LookAt (CameraFacing.transform.position);





		ShowImage(leg_feet_l, leg_feet_l_reveal, leg_feet_l_revealed);
		ShowImage(leg_feet_r, leg_feet_r_reveal, leg_feet_r_revealed);
		ShowImage(leg_lower_l, leg_lower_l_reveal, leg_lower_l_revealed);
		ShowImage(leg_lower_r, leg_lower_r_reveal, leg_lower_r_revealed);
		ShowImage(leg_knee_l, leg_knee_l_reveal, leg_knee_l_revealed);
		ShowImage(leg_knee_r, leg_knee_r_reveal, leg_knee_r_revealed);
		ShowImage(leg_upper_l, leg_upper_l_reveal, leg_upper_l_revealed);
		ShowImage(leg_upper_r, leg_upper_r_reveal, leg_upper_r_revealed);

		ShowImage(arm_hand_l, arm_hand_l_reveal, arm_hand_l_revealed);
		ShowImage(arm_hand_r, arm_hand_r_reveal, arm_hand_r_revealed);
		ShowImage(arm_low_l, arm_low_l_reveal, arm_low_l_revealed);
		ShowImage(arm_low_r, arm_low_r_reveal, arm_low_r_revealed);
		ShowImage(arm_elbow_l, arm_elbow_l_reveal, arm_elbow_l_revealed);
		ShowImage(arm_elbow_r, arm_elbow_r_reveal, arm_elbow_r_revealed);
		ShowImage(arm_up_l, arm_up_l_reveal, arm_up_l_revealed);
		ShowImage(arm_up_r, arm_up_r_reveal, arm_up_r_revealed);

		ShowImage(pelvis, pelvis_reveal, pelvis_revealed);
		ShowImage(abadomen, abadomen_reveal, abadomen_revealed);
		ShowImage(chest, chest_reveal, chest_revealed);
		ShowImage(shoulder_l, shouder_l_reveal, shoulder_l_revealed);
		ShowImage(shoulder_r, shoulder_r_reveal, shoulder_r_revealed);
		ShowImage(neck, neck_reveal, neck_revealed);
		ShowImage(head, head_reveal, head_revealed);
		ShowImage(crown, crown_reveal, crown_revealed);
		ShowImage(fullbody, fullbody_reveal, fullbody_revealed);
		







		// Now we start handling the different parts of the body in the hud to be shown.




	/*

		// for full body
		if ((countTimer > body_reveal) && (countTimer < body_reveal + fade_time)) {
			fullbody.gameObject.SetActive(true);
			if (fullbody_revealed == false) {
					fullbody.CrossFadeAlpha(0.0f, 0.0f, false); 
					fullbody_revealed = true;
			}
			fullbody.CrossFadeAlpha(1.0f, fade_time, false); 
			}
		if (countTimer > (fullbody_reveal + fade_time + part_duration)) {
			fullbody.CrossFadeAlpha(0.0f, fade_time, false); 
	
		}
*/

	
	

	}
	}