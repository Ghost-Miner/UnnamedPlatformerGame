/*
Copyright © Jonathan Curtis, PortaPixel 2020
Water Created by Jonathan C.
*/


using UnityEngine;

public class Water : MonoBehaviour
{
    public float playerBuoyancy;
	public GameObject player;

	float startSpeed;

	private void Start()
	{
		startSpeed = player.GetComponent<Movement>().speed;
		//startJumpHeight = player.GetComponent<Movement>().jumpForce;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		player.GetComponent<Movement>().speed -= playerBuoyancy;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		player.GetComponent<Movement>().speed = startSpeed;
	}
}
