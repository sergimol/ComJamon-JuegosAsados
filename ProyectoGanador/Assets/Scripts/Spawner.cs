using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject person;
    float t = 0;
    public bool time = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time)
        {
            t += Time.deltaTime;
            if (t > 2)
            {
                createChild();
                t = 0;
                time = false;
            }
        }
    }

    public void setPrefab(Transform t)
    {
        person = t.gameObject;
        createChild();
    }

    public void createChild()
    {
        if (person == null) return;
        if (this.transform.childCount == 1)     //CAMBIARLO CUANDO HAYA MAS OBJETOS EN UN SPAWN
        {
            this.transform.GetChild(0).position = this.transform.position;
            this.transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
            this.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
        else
            Instantiate(person, this.transform.position, this.transform.rotation, this.transform);
    }
}
