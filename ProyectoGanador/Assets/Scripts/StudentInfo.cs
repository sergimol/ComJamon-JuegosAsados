using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentInfo : MonoBehaviour
{

    [SerializeField]
    int iD;
    bool upper = false;

    Animator anim;
    SphereCollider col;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();

        col = this.gameObject.GetComponent<SphereCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BatHit>() != null)
        {
            anim.enabled = false;
            col.enabled = false;

            ClassificationManager.instanceCM.npcGolpeado(iD);
            this.transform.GetComponentInParent<Spawner>().time = true;
        }
    }

    public void setUpper(bool b)
    {
        upper = b;
    }

    public int getID()
    {
        return iD;
    }

    public bool getUpper()
    {
        return upper;
    }
}
