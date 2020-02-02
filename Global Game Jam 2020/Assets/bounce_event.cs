using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce_event : MonoBehaviour
{
    public AudioSource bounce_event_collision;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bounce_event_collision.Play();
        }
    }
}
