using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int randonNumber = UnityEngine.Random.Range(0, 4);
        switch(randonNumber)
        {
            case 0:
                transform.position = new Vector3(10, 0, 0);
                break;
            case 1:
                transform.position = new Vector3(10, 0, 11);
                break;
            case 2:
                transform.position = new Vector3(11, 0, -13);
                break;
            case 3:
                transform.position = new Vector3(-10, 0, 10);
                break;
        }
    }
}
