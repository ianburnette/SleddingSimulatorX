using UnityEngine;
using System.Collections;

public class SledPickup : MonoBehaviour {

	public PlayerSwitcher switchScript;
	public Transform sledCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		if (coll.transform.tag == "Player") {
			GetComponent<SledCrashDetection>().PickupSled();
			switchScript.PickupSled();
			gameObject.SetActive(false);
			//sledCam.gameObject.SetActive(true);
		}
	}
}
