using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded;
    public LayerMask groundLayer;
    private int groundContacts = 0; // licznik kontaktów

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            groundContacts++;
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                groundContacts = 0;
                isGrounded = false;
            }
        }
    }
}
