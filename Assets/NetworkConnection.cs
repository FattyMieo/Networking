using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkConnection : NetworkBehaviour
{
	private static NetworkConnection _instance;
	public static NetworkConnection instance
	{
		get
		{
			return _instance;
		}
	}

	public List<PlayerController> players;

	void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}
}
