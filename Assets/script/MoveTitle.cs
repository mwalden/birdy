using UnityEngine;
using System.Collections;

public class MoveTitle : MonoBehaviour {

	public float endx = 0f;
	public float endy = 0f;
	public float dx = .001f;
	public float dy = .001f;

	public float currentX;
	public float currentY;

	private bool atX;
	private bool atY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		currentX = getPosition(transform.position.x,endx,dx,atX); 
//		currentY = getPosition(transform.position.y,endy,dy,atY);
//		if (currentX == endx)
//			atX = true;
//		if (currentY == endy)
//			atY = true;
//		transform.position = new Vector2 (currentX, currentY);
		transform.position = Vector2.Lerp(transform.position, new Vector2(endx,endy), Time.deltaTime);


	}

	float getPosition(float current, float destination, float delta,bool atValue){

		if (atValue == true)
			return current;	
		//gotta minus it
		float newValue = current;
//
//		if (current == destination) {
//			return newValue;
//		}
		if (current > destination) {
			newValue = current-delta;
		}
		if (current < destination) {
			newValue = current+delta;
		}
		Debug.Log (newValue);
		return newValue;
	}
}
