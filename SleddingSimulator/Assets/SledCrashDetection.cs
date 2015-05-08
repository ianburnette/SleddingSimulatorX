using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SledCrashDetection : MonoBehaviour {

	public LayerMask groundMask;
	public PlayerSwitcher switchScript;

	public PhysicMaterial sledMat, playerMat;

	public float maxDifference, minMag, spinSpeed, upSpeed;

	public bool canControl;

	public Text velocityText;
	public Rigidbody sledCam;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		velocityText.text = "Current Magnitude: " + rb.velocity.magnitude + " and difference is " + Quaternion.Angle(Quaternion.LookRotation(rb.velocity), transform.rotation);
		//velocityText.text = "rotation is " + transform.rotation.eulerAngles + " and move rotation is " + Quaternion.LookRotation(rb.velocity).eulerAngles;// + rb.velocity.magnitude + " and difference is " + Quaternion.Angle(Quaternion.LookRotation(rb.velocity), transform.rotation);
		if (canControl) {
			CheckToTip();
		}
	}

	void CheckToTip(){
		if (Physics.Raycast(transform.position, transform.up, 1f, groundMask)){
			//Crash();
		}
		if (Quaternion.Angle(Quaternion.LookRotation(rb.velocity), transform.rotation) > maxDifference){
			if (GetComponent<Rigidbody>().velocity.magnitude > minMag){
				Crash();
			}
		}
	}

	public void PickupSled(){
		GetComponent<SphereCollider> ().enabled = false;
		GetComponent<BoxCollider> ().sharedMaterial = sledMat;
	}

	void Crash(){
		switchScript.Crash ();
		canControl = false;
		rb.AddTorque (new Vector3 (1, 1, 1) * spinSpeed * rb.velocity.magnitude, ForceMode.Impulse);
		rb.AddForce (Vector3.up * upSpeed, ForceMode.Impulse);
		sledCam.isKinematic = false;
		sledCam.GetComponent<SphereCollider> ().isTrigger = false;
		sledCam.GetComponent<BoxCollider> ().isTrigger = false;
		sledCam.AddTorque (new Vector3 (-1, 1, -1) * spinSpeed * rb.velocity.magnitude, ForceMode.Impulse);
		sledCam.AddForce (Vector3.up * upSpeed, ForceMode.Impulse);
		GetComponent<SphereCollider> ().enabled = true;
		GetComponent<BoxCollider> ().sharedMaterial = playerMat;
	}
}
