/*
Copyright © Jonathan Curtis, PortaPixel 2020
SpikeTraps Created by Jonathan C.
*/


using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTraps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
	{
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        player.Dead();
    }
}
