using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.InicializarPeople();
        GameManager.instance.initializeWalkingPeople();
        ClassificationManager.instanceCM.startCM();
    }
}
