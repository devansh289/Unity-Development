using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	private float speed = 12f;	
	private int score = 0;
	private int randNumber;

	Color[] primaryColor = new Color[5];
	Color[] secondaryColor = new Color[5];
	Color[] ballsColor = new Color[2];

	public Collider2D upPlayerCollider;
	public Collider2D downPlayerCollider;

 	public Collider2D upBallCollider;
	public Collider2D downBallCollider;

	public Renderer topPlayerRender;
	public Renderer bottomPlayerRender;

	public Renderer topBall;
	public Renderer bottomBall;

	public Renderer animateCircle;

	public Rigidbody2D upBallBody;
	public Rigidbody2D downBallBody;

    public AudioClip coinSoundEffect;
    AudioSource audioSource;

	public Text scoreText;


	private bool animationState = true;
	public Animator anim;
	public Animator animParticle;

	// Use this for initialization
	void Start () {
		
		//Debug.Log("Game Started");
		

		//Add colours here
		primaryColor[0] = new Color(1f,0f,0f);
		secondaryColor[0] = new Color(1f,1f,0f);

		primaryColor[1] = new Color(0f,0f,1f);
		secondaryColor[1] = new Color(1f,1f,1f);

		primaryColor[2] = new Color(0.196f, 0.886f, 0.9451f);
		secondaryColor[2] = new Color(1f,0.91f,0.588f);

		primaryColor[3] = new Color(0.55f,0.071f,0.984f);
		secondaryColor[3] = new Color(0.196f, 0.886f, 0.9451f);

		primaryColor[4] = new Color(0.55f,0.071f,0.984f);
		secondaryColor[4] = new Color(1f,0.91f,0.588f);

		Random.InitState(System.DateTime.Now.Millisecond);
		
		//CHANGE THIS WHEN ADDING COLORS
		randNumber = Random.Range((int)0,(int)3);

		topPlayerRender.material.color = primaryColor[randNumber];
		bottomPlayerRender.material.color = secondaryColor[randNumber];

		ballsColor[0] = primaryColor[randNumber];
		ballsColor[1] = secondaryColor[randNumber];

		topBall.material.color = primaryColor[randNumber];
		bottomBall.material.color = secondaryColor[randNumber];

		//upBallBody.gravityScale = gravityVal;
		downBallBody.velocity = new Vector2(0f,0f);
		upBallBody.velocity = new Vector2(0f,-speed);

		anim = GetComponent<Animator>();	
		audioSource = GetComponent<AudioSource>();
		//animParticle = GetComponent<Animator>();	

		StopSounds();
	}

	void Update () {
		if (Input.touchCount > 0 &&  animationState && !anim.GetCurrentAnimatorStateInfo(0).IsName("rot2")) {
			anim.Play("rot1");
			animationState = false;
		} else if (Input.touchCount > 0 &&  !animationState && !anim.GetCurrentAnimatorStateInfo(0).IsName("rot1")) {
			anim.Play("rot2");
			animationState = true;
		}

	}


	void FixedUpdate() {
	randNumber = Random.Range((int)0, (int)2);

		if (upPlayerCollider.IsTouching(upBallCollider)){
			
			upBallCollided();
			checkCollider(topPlayerRender, topBall);
			changeBallColor();
     	} else if (downPlayerCollider.IsTouching(downBallCollider)){
			
			downBallCollided();
			checkCollider(bottomPlayerRender, bottomBall);
			changeBallColor();
		} else if (upPlayerCollider.IsTouching(downBallCollider)){
			
			downBallCollided();
			checkCollider(topPlayerRender, bottomBall);
			changeBallColor();
		} else if (downPlayerCollider.IsTouching(upBallCollider)){
			
			upBallCollided();
			checkCollider(bottomPlayerRender,topBall);
			changeBallColor();
		}
	}			

	
	void upBallCollided(){
		upBallBody.velocity = Vector2.zero;
		upBallBody.angularVelocity = 0f;
		upBallBody.position = new Vector2(0f,11.02f);
		upBallBody.rotation = 0f;
		
	}

	void downBallCollided(){
		downBallBody.velocity = Vector2.zero;
		downBallBody.angularVelocity = 0f;
		downBallBody.position = new Vector2(0f,-11.1f);
		downBallBody.rotation = 0f;

	}

	void upBallInitiate() {
		upBallBody.velocity = new Vector2(0,-speed);
	}

	void downBallInitiate(){
		
		downBallBody.velocity = new Vector2(0,speed);
	}

	void checkCollider(Renderer player, Renderer ball) {
		
		Color playerColor = player.material.color;
		Color ballColor = ball.material.color;
		
		if (playerColor.Equals(ballColor)) {
			animateCircle.material.color = playerColor;		
			score += 1;
			scoreText.text = score.ToString();
			animParticle.Play("particlePoint");
			PlaySounds();
			audioSource.PlayOneShot(coinSoundEffect, 1F);

			if (randNumber == 0) {
				downBallInitiate();
			} else if (randNumber == 1){
				upBallInitiate();
			}
		} else if(!playerColor.Equals(ballColor)) {

			PlayerPrefs.SetInt("ballFlipCurrentUserScore", score);
			SceneManager.LoadScene("Retry");

		}

	}
	
	int checkWhichBall() {
		int a = Random.Range((int)0,(int)2);
		Debug.Log(a);
		return a;
	}

	void changeBallColor(){
		topBall.material.color = ballsColor[Random.Range((int)0, (int)2)];
		bottomBall.material.color = ballsColor[Random.Range((int)0,(int)2)];
	}

	void StopSounds()
	{
    	audioSource.enabled = false;
	}

	void PlaySounds(){
		audioSource.enabled = true;
	}
}

