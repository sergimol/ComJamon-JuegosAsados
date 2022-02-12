using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollActivator : MonoBehaviour
{
    Animator anim;
    SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();

        col = this.gameObject.GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BatHit>() != null)
        {
            anim.enabled = false;

            col.enabled = false;    
        }
    }
}
