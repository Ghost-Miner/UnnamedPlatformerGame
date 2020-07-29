using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetFloat("x", gameObject.transform.position.x);
            PlayerPrefs.SetFloat("y", gameObject.transform.position.y);
        }
    }
}
