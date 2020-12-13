using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float springModifier;
    [SerializeField] float springBase;
    private float playerSpeed;


    public void OnTriggerEnter(Collider other)
    {
        playerSpeed = Player.GetComponent<Rigidbody>().velocity.magnitude;
        Player.GetComponent<Rigidbody>().velocity = transform.up * springBase + transform.up * playerSpeed * springModifier;

    }

}
