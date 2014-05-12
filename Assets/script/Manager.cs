using UnityEngine;
using System.Collections;
namespace AssemblyCSharp{
	public enum GameState{
		START,GAMEOVER
	};
	
	public class Manager : MonoBehaviour {
		public Direction birdDirection;
		public bool headTurned;
		public bool handMoving = false;
		public GUIText gameOver;
		public GUIText restart;
		public GameState gameState = GameState.START;
		public bool turnOffBird;

		// Use this for initialization
		void Start () {
			Messenger<Direction>.AddListener ("birdDirection", onBirdChangeDirection);
			Messenger<bool>.AddListener ("handMoving", onHandMoving);
			Messenger<bool>.AddListener ("headTurned", onHeadTurned);
			Messenger.AddListener ("onGameOver", onGameOver);
			Messenger<GameState>.AddListener ("onGameState", onGameState);
		}
		
		// Update is called once per frame
		void Update () {
			if (gameState == GameState.GAMEOVER){
				if (Input.GetKey("space")){
					gameOver.enabled = false;
					restart.enabled = false;
					Application.LoadLevel("start screen");
				}
			}

			if (gameState == GameState.START){

				if (handMoving == true && (birdDirection == Direction.LEFT || (birdDirection == Direction.RIGHT && headTurned))) {
					if (turnOffBird == true)
						return;
					Messenger.Broadcast ("triggerGameOver");
				}
			}
		}
		void onHeadTurned(bool headTurned){
			this.headTurned = headTurned;
		}
		void onHandMoving(bool handMoving){
			this.handMoving = handMoving;
		}
		void onBirdChangeDirection(Direction direction){
			birdDirection = direction;
		}
		void onGameState(GameState gameState){
			this.gameState = gameState;
		}
		void onGameOver(){
			//Time.timeScale = 0.0f;
			if (gameOver == null || restart == null) {
				Debug.Log("sihts null yo");
			}

			Messenger<GameState>.Broadcast ("onGameState", GameState.GAMEOVER);

			gameOver.text= "The bird saw you!";
			restart.text = "Press space to start again";
		}

		void Awake () {			
			DontDestroyOnLoad (gameOver);
			DontDestroyOnLoad (restart);
		}


	}
}
