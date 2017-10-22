using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[System.Serializable]

	public class PlayerStats{
		public int Health = 100;	
	}

	public PlayerStats stats = new PlayerStats ();

	public int fallBoundary = -20;

	[SerializeField]

	void Start(){
	}

	void Update(){
		if (transform.position.y <= fallBoundary) {
			DamagePlayer (99999);
		}
	}

	public void DamagePlayer (int damage){
		stats.Health -= damage;
		if (stats.Health <= 0) {
			GameMaster.KillPlayer (this);
		}
	}
}
