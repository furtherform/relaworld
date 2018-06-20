#pragma strict

var blackoutCubeL : Transform;
var blackoutCubeR : Transform;

var fade : boolean;

var changeLevelFade : boolean;

var levelToLoad : String;

private var fadeToDarkTimer : float;
private var fadeTolightTimer : float;

private var startColor : Color;
private var endColor : Color;

private var changingLevel : boolean;


function Start () {
	
	startColor = Color(0, 0, 0, 0);
	endColor = Color(0, 0, 0, 1);
	
	fadeToDarkTimer = 0;
	fadeTolightTimer = 0;
	fade = false;
	changingLevel = false;
}

function Update () {
	
	if (fade == true || changeLevelFade == true) {
		
		if (blackoutCubeL.GetComponent.<Renderer>().material.color == startColor) {
			
			fadeTolightTimer = 0;
			
		}
		
		fadeToDarkTimer += Time.deltaTime;
		blackoutCubeL.GetComponent.<Renderer>().material.color = Color.Lerp(startColor, endColor, fadeToDarkTimer);
		blackoutCubeR.GetComponent.<Renderer>().material.color = Color.Lerp(startColor, endColor, fadeToDarkTimer);
		
		if (blackoutCubeL.GetComponent.<Renderer>().material.color == endColor && changingLevel == false) {
			
			if (changeLevelFade == true) {
				changingLevel = true;
				Application.LoadLevel(levelToLoad);
				
			}
			
		}
		
		
	} else {
		
		if (blackoutCubeL.GetComponent.<Renderer>().material.color == endColor) {
			
			fadeToDarkTimer = 0;
			
		}
		
		fadeTolightTimer += Time.deltaTime;
		blackoutCubeL.GetComponent.<Renderer>().material.color = Color.Lerp(endColor, startColor, fadeTolightTimer);
		blackoutCubeR.GetComponent.<Renderer>().material.color = Color.Lerp(endColor, startColor, fadeTolightTimer);
		
	}
	
}