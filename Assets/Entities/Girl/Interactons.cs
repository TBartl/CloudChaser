using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactons : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Insert bullshit interaction here :'(");
        }

        if (other.tag == "Flower")
        {
            print("Insert interaction here :D");
        }
    }
}