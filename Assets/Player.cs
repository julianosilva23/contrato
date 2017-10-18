using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[System.Serializable]

	public class PlayerStats{
		public int Helth = 100;	
	}

	public PlayerStats playerStats = new PlayerStats ();

	public int fallBoundary = -20;

	void Update(){
		if (transform.position.y <= fallBoundary) {
			DamagePlayer (99999);
		}
	}

	public void DamagePlayer (int damage){
		playerStats.Helth -= damage;
		if (playerStats.Helth <= 0) {
			GameMaster.KillPlayer (this);
		}
	}
}
