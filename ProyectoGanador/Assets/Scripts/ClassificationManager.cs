using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassificationManager : MonoBehaviour
{
    public static ClassificationManager instanceCM;
    GameManager man;
    List<StudentInfo> people;
    int posJugador;

    [SerializeField]
    int posIni;

    private void Awake()
    {
        instanceCM = this; 
    }
    // Start is called before the first frame update
    public void startCM()
    {
        posJugador = posIni - 1;
        people = new List<StudentInfo>();
        man = GameManager.instance;
        List<Transform> children =  man.getSpawnList();

        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].childCount == 1)  //CAMBIARLO CUANDO HAYA MAS OBJETOS EN UN SPAWN
            {
                people.Add(children[i].GetChild(0).gameObject.GetComponent<StudentInfo>());
            }
        }

        man.Mezcla(people);

        people[posJugador].setUpper(true);

        log();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void npcGolpeado(int iD)
    {
        people[posJugador].setUpper(false);

        if (iD == people[posJugador].getID())
        {
            subirPuesto();
        }
        else bajarPuesto();

        people[posJugador].setUpper(true);

        log();
    }

    private void subirPuesto()
    {
        if (posJugador >= 0)
            posJugador--;
    }

    private void bajarPuesto()
    {
        if (posJugador < people.Count - 1)
            posJugador++;
    }

    private void log()
    {

        Debug.Log(posJugador);
        for (int i = 0; i < people.Count; i++)
        {          
           Debug.Log("ID: " + people[i].getID() + " Upper: " + people[i].getUpper());
        }
    }
}
