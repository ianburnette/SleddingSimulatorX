using UnityEngine;
using System.Collections;

public class PlayerSwitcher : MonoBehaviour {

	public bool onSled, crashed, haveSled;

	public GameObject onSledPlayer, onFootPlayer, sledCamera, sledModel;
	public float switchCooldownTime;
	bool cantSwitch = false;
	public Vector3 onFootPlayerOffset, onSledPlayerOffset, sledCameraOffset;

	// Use this for initialization
	void Start () {
		sledCameraOffset = onSledPlayer.transform.GetChild (1).position;
	}
	
	// Update is called once per frame
	void Update () {
		CheckForSwitch ();
	}

	public void Crash(){
		//cantSwitch = true;
		onSled = false;
		haveSled = false;
		crashed = true;
		sledModel.SetActive (false);
	}

	public void PickupSled(){
		haveSled = true;
		crashed = false;
		sledCamera.GetComponent<Rigidbody> ().isKinematic = true;
		sledCamera.transform.position = onSledPlayer.transform.position + sledCameraOffset;
		sledCamera.GetComponent<SphereCollider> ().isTrigger = true;
		sledCamera.GetComponent<BoxCollider> ().isTrigger = true;
		sledModel.SetActive (true);
	}

	void CheckForSwitch(){
		if (Input.GetButtonDown ("Fire1") && !cantSwitch) {
			Switch ();
		}
	}

	void Switch(){
		if (onSled) {
			SetOnFootPlayerPosition (onSledPlayer.transform);
			onSledPlayer.SetActive (false);
			onFootPlayer.SetActive (true);
			onSled = false;
		} else if (crashed) {
			SetOnFootPlayerPosition (sledCamera.transform);
			sledCamera.SetActive (false);
			onFootPlayer.SetActive (true);
			haveSled = false;
		} else {
			onSledPlayer.SetActive (true);
			onFootPlayer.SetActive (false);
			sledCamera.SetActive(true);
			onSled = true;
			SetOnSledPlayerPosition (onFootPlayer.transform);
		}
		cantSwitch = true;
		Invoke ("ResetSwitchTime", switchCooldownTime);
	}

	void SetOnFootPlayerPosition(Transform locationToPlace){
		onFootPlayer.transform.position = locationToPlace.position + onFootPlayerOffset;
	}

	void SetOnSledPlayerPosition(Transform locationToPlace){
		onSledPlayer.transform.position = locationToPlace.position + onSledPlayerOffset;
	}

	void ResetSwitchTime(){
		cantSwitch = false;
	}
}
