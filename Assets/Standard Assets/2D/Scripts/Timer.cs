﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public Collider2D coll;
	// private Rigidbody2D rb;
	private GameObject player;
	private Rigidbody2D rb;
	public const float tempoTroca = 15;  
	float timeLeft = tempoTroca;
	bool gost = false;

	// Use this for initialization

	void AchaPlayer(){
		if(player == null){
			player = GameObject.FindWithTag ("Player");
			
			if(player != null){
				rb = player.GetComponent<Rigidbody2D>();
				timeLeft = tempoTroca;		
			}
		}
	}

	void Start () {
		AchaPlayer();
				
		Debug.Log("start");
	}

	void Update()
	{
		AchaPlayer();	
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0 && player != null)
		{
			if (!gost) {
				Debug.Log ("gost");
				rb.mass = 0.65f;
				gost = true;
				timeLeft = tempoTroca;
				coll.enabled = false;

			} else {
				Debug.Log ("human");
				rb.mass = 1f;
				gost = false;
				coll.enabled = true;			
				timeLeft = tempoTroca;

			}
		}
	}
}	
