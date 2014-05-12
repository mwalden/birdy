using UnityEngine;
using System.Collections;
using System.Timers;

namespace AssemblyCSharp{
	public enum BirdState{
		STANDING,
		WALKING,
		HOPPING,
		TURNING,
		FLYING_AWAY
	};


	public class birdscript : MonoBehaviour {
		public float speed =5f;
		public float hopForce;
		private bool flipIt ;
		private float flipped = -1f;

		public float leftBounds = -5;
		public float rightBounds = 5;
		private float action;

		private Vector2 birdPosition;

		private Animator animator;
		private bool gameOver = false;
		private Vector2 gameOverDestination = new Vector2 (10, 20);

		public BirdState birdState;
		public Direction birdDirection;
		public bool headTurned;

		public float ROTATE = 180f;


		// Use this for initialization
		void Start () {
			hopForce = 100f;
			Debug.Log ("hey is this working?");
			birdState = BirdState.STANDING;
			birdDirection = Direction.LEFT;
			animator = GetComponent<Animator> ();
			InvokeRepeating("onBirdyEvent", 1.5f , 1.5f);
			birdPosition = transform.position;
			Messenger.AddListener("triggerGameOver",onTriggerGameOver);

		}

		// Update is called once per frame
		void Update () {

			//get bird position.
			birdPosition = transform.position;
			//get random number to determine action 
			action = Random.Range(-10,10);
			//let the manager know what direction the bird is facing
			Messenger<Direction>.Broadcast("birdDirection",birdDirection);
			Messenger<bool>.Broadcast("headTurned",headTurned);

			if (birdState == BirdState.FLYING_AWAY){
				flyAway();
				return;
			}
			//handle bird action
			if (birdState == BirdState.TURNING) {
				turnBird();
				return;
			}
			if (birdState == BirdState.WALKING) {
				moveBird();
				return;
			}
			if (birdState == BirdState.HOPPING) {
				birdyHop();
				return;
			}

		}

		void onBirdyEvent(){

			if (birdState == BirdState.STANDING) {
				if (action < -5){
					animator.SetTrigger ("turnhead");
					headTurned = true;
					Debug.Log("TURNING HEAD!!!!!!!!!!!!!!!!!!!!!! : " + headTurned); 
				}
				if (action > 5 && (birdPosition.x < rightBounds - 1 && birdPosition.x > leftBounds + 1 ))
					birdState = BirdState.HOPPING;
				else if (action <=5  && action > 0){
					birdState = BirdState.TURNING;
				}else
					birdState = BirdState.WALKING;
			} else {
				birdState = BirdState.STANDING;
			}
		}
		void moveBird(){

			Vector3 moveDir = Vector3.zero;

			moveDir.x = flipped;

			transform.position += moveDir * speed * Time.deltaTime;

			if (transform.position.x < leftBounds && flipIt == false) {
				flipIt = true;
			}
			
			if (transform.position.x > rightBounds && flipIt == false) {
				flipIt = true;
			}
			if (flipIt == true)
				turnBird ();


		}
		void flyAway(){
			if (transform.position.x > gameOverDestination.x && transform.position.y > gameOverDestination.y) {
				if (gameOver == false){
					Messenger.Broadcast ("onGameOver");
					gameOver = true;
				}
				return;
			}

			rigidbody2D.AddForce(new Vector2(10f,10f));
		}
		void turnBird(){
			birdDirection = birdDirection == Direction.LEFT ? Direction.RIGHT : Direction.LEFT;

			transform.Rotate(0,ROTATE,0);

			flipIt = false;			
			flipped *= -1;
			if (birdState == BirdState.TURNING)
				birdState = BirdState.STANDING;
		}
		void birdyHop(){
			rigidbody2D.AddForce(new Vector2(hopForce*flipped,hopForce));
			birdState = BirdState.STANDING;
		}

		void headTurnComplete(){
			headTurned = false;
		}


		void onTriggerGameOver(){
			//gameOver = true;
			CancelInvoke ("onBirdyEvent");
			birdState = BirdState.FLYING_AWAY;

			//transform.rotation = new Quaternion (0, 180f, 0, 0);
			rigidbody2D.gravityScale = 0;
		}

		void Awake () {

			DontDestroyOnLoad (transform.root.gameObject);
		}

	}
}
