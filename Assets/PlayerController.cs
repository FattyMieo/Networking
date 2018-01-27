using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	//Dev
	private Rigidbody2D rb;
	public SpriteRenderer sprite;

	[SyncVar]
	public int id;

	public float speed = 10.0f;
	public float jumpForce = 10.0f;

	// Use this for initialization
	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isLocalPlayer) return;

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		//float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		//transform.Translate(x, y, 0.0f);

		rb.velocity = new Vector2(x, rb.velocity.y);

		if(Input.GetKeyDown(KeyCode.W))
			rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
	}

	public override void OnStartLocalPlayer()
	{
		this.id = Network.connections.Length - 1;

		if(this.id < 0) this.id = 0;

		RpcSpawn();

		List<PlayerController> players = NetworkConnection.instance.players;
		for(int i = 0; i < players.Count; i++)
		{
			switch(i)
			{
				case 0:
					players[i].sprite.color = Color.red;
					break;
				case 1:
					players[i].sprite.color = Color.blue;
					break;
				case 2:
					players[i].sprite.color = Color.yellow;
					break;
				case 3:
					players[i].sprite.color = Color.green;
					break;
			}
		}
	}


	void OnPlayerConnected(NetworkPlayer player)
	{
		
	}

	[ClientRpc]
	void RpcSpawn()
	{
		NetworkConnection.instance.players.Add(this);

		List<PlayerController> players = NetworkConnection.instance.players;
		switch(this.id)
		{
			case 0:
				players[this.id].sprite.color = Color.red;
				break;
			case 1:
				players[this.id].sprite.color = Color.blue;
				break;
			case 2:
				players[this.id].sprite.color = Color.yellow;
				break;
			case 3:
				players[this.id].sprite.color = Color.green;
				break;
		}
	}
}
