using UnityEngine;
using System.Collections;
namespace AssemblyCSharp{
public class HandScript : MonoBehaviour {
	public float speed = 5f;
	private GameState gameState;
	private Animator anim;
	// Use this for initialization
	void Start () {
		Messenger<GameState>.AddListener ("onGameState", onGameState);
		anim = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GameState.GAMEOVER)
			return;
		if (Input.GetKey ("space")) {
			Vector3 moveDir = Vector3.right;
			transform.position += moveDir * speed * Time.deltaTime;
			Messenger<bool>.Broadcast ("handMoving", true);
			anim.SetTrigger("walk");
		} else {
			Messenger<bool>.Broadcast("handMoving",false);
			anim.SetTrigger("idle");
		}
	}

	
	void onGameState(GameState gameState){
		this.gameState = gameState;
	}

}
}
