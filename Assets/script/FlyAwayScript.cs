using UnityEngine;
using System.Collections;

public class FlyAwayScript : MonoBehaviour {
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		Debug.Log ("askldfjals;dkfjals;dkfjal;ksdjfaklsdf");
		Messenger.AddListener("triggerGameOver",onTriggerGameOver);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("askldfjals;dkfjals;dkfjal;ksdjfaklsdf");
	}
	
	void onFlyAwayComplete(){
		Messenger.Broadcast ("onGameOver");
	}
	
	void onTriggerGameOver(){

		animator.SetTrigger ("flyAway");
	}
}
