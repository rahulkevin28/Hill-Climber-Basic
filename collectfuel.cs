using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectfuel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Change FuelController to Fuel
            Fuel.instance.FillFuel();
            Destroy(gameObject);
        }
    }
}

