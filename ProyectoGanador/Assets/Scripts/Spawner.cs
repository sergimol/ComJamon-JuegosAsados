using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject person;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPrefab(Transform t)
    {
        person = t.gameObject;
        createChild();
    }

    public void createChild()
    {
        if (person == null) return;
        if (this.transform.childCount == 1)    //CAMBIARLO CUANDO HAYA MAS OBJETOS EN UN SPAWN
            Destroy(this.transform.GetChild(0).gameObject);
        Instantiate(person, this.transform.position, this.transform.rotation, this.transform);
    }
}
