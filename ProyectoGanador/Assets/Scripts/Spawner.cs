using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject person;
    Vector3 pos;
    Quaternion rot;
    [SerializeField]
    bool rotar = false;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPrefab(GameObject t)
    {
        person = t;
        pos = t.transform.position;
        rot = t.transform.rotation;
        if (rotar) rot.eulerAngles += new Vector3(0, 180, 0);
        createChild();
    }

    public void createChild()
    {
        if (person == null) return;
        if (this.transform.childCount == 6)     //CAMBIARLO CUANDO HAYA MAS OBJETOS EN UN SPAWN
        {
            this.transform.GetChild(5).position = this.GetComponentInParent<Transform>().position;
            this.transform.GetChild(5).rotation = rot;
            this.transform.GetChild(5).gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.transform.GetChild(5).gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            this.transform.GetChild(5).gameObject.GetComponent<Animator>().enabled = true;
            this.transform.GetChild(5).gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            Instantiate(person, this.transform, false);
        }
    }
}
