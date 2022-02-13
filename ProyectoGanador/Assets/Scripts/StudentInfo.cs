using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentInfo : MonoBehaviour
{
    [SerializeField]
    string description;

    [SerializeField]
    int iD;
    bool upper = false;

    int lab = 0;
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
    public int getLab()
    {
        return lab;
    }

    public bool getUpper()
    {
        return upper;
    }

    public void SetLab(int i)
    {
        lab = i;
    }

    public string getDescription() { return description; }
}
