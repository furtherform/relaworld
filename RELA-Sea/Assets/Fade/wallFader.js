#pragma strict
//This script will fade the camera to black when you lean into an object
//tagged with "Fade". The object has to have a collider attached to it

//This script goes on only one of the fade boxes

var OVRCameraController : Transform;

function Start () {

}

function Update () {

}

function OnTriggerEnter (other : Collider) {
	
	if (other.tag == "Fade") {
		OVRCameraController.GetComponent(fadeInOut).fade = true;
	}
	
}

function OnTriggerExit (other : Collider) {
	
	if (other.tag == "Fade") {
		OVRCameraController.GetComponent(fadeInOut).fade = false;
	}
	
}