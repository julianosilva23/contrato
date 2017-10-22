using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {


	private GameObject player;
	private int vida;
	public int DanoInimigo;

	
	[System.Serializable]

	public class InimigoStats{
		public int Helth = 100;	
	}

	public InimigoStats inimigostats = new InimigoStats ();

	[SerializeField]

	public void DamageInimigo (int damage){
		inimigostats.Helth -= damage;
		if (inimigostats.Helth <= 0) {
			GameMaster.KillInimigo (this);
		}
	}

	void AchaPlayer(){
		if(player == null){
			player = GameObject.FindWithTag ("Player");
		}
	}

	void Start(){

		AchaPlayer();

		
	}

	void Update(){
		AchaPlayer();
	}

	//mata o inimigo se o player atacar 3x na cabeça
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag("pesplayer")){
			DamageInimigo(35);
		}else if (collider.CompareTag("corpoplayer")){
			if(DanoInimigo < 0){DanoInimigo = 45;}
			player.GetComponent<Player>().DamagePlayer(DanoInimigo);
		}

	}

}
