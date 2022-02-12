using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHit : MonoBehaviour
{
    private SphereCollider batCol;
    // Start is called before the first frame update
    void Start()
    {
        batCol = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(Camera.main.transform.forward*8, ForceMode.Impulse);

            Debug.Log("Mona china golpeada");
            //batCol.enabled = false;
        }
    }
}
