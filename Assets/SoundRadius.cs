using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadius : MonoBehaviour
{

    public bool canBeHeard = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            canBeHeard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            canBeHeard = false;
        }
    }
}
