using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassificationManager : MonoBehaviour
{
    public static ClassificationManager instanceCM;
    GameManager man;
    List<StudentInfo> people;
    int posJugador;

    [SerializeField]
    int posIni;
    [SerializeField]
    GameObject TV;

    bool win = false;

    private void Awake()
    {
        instanceCM = this;
    }
    // Start is called before the first frame update
    public void startCM()
    {
        //Debug.Log(posJugador + " " + posIni);
        posJugador = posIni - 1;
        people = new List<StudentInfo>();
        man = GameManager.instance;
        List<Transform> children = man.getSpawnList();

        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].childCount == 6)        //CAMBIARLO CUANDO HAYA MAS OBJETOS EN UN SPAWN
            {
                people.Add(children[i].GetChild(5).gameObject.GetComponent<StudentInfo>());
            }
        }

        man.Mezcla(people);

        people[posJugador].setUpper(true);

        ActTV();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void npcGolpeado(int iD)
    {
        if (win)
            return;

        people[posJugador].setUpper(false);

        if (iD == people[posJugador].getID())
        {
            subirPuesto();
        }
        else bajarPuesto();

        if (posJugador != -1)
            people[posJugador].setUpper(true);
        else win = true;

        ActTV();
    }

    private void ActTV()
    {
        int i = 0;
        bool up = false, down = false;

        while (!up && !down && i < 4)
        {
            i++;
            if (posJugador + 1 - i < 0)
            {
                up = true;
            }
            if (posJugador + i + 1 > people.Count)
            {
                down = true;
            }
        }

        int ini = 0;
        if (!up && !down) ini = posJugador - 3;
        else if (up) ini = 0;
        else if (down) ini = people.Count - 8;
        int auxJ = 0;
        if (posJugador == -1)
        {
            Transform line = TV.transform.GetChild(0).GetChild(2).GetChild(auxJ);
            line = TV.transform.GetChild(0).GetChild(2).GetChild(auxJ);
            auxJ++;
            line.GetChild(1).GetComponent<Image>().color = Color.green;
            line.GetChild(2).GetComponent<TextMeshProUGUI>().text = (ini + auxJ).ToString();
            line.GetChild(3).GetComponent<TextMeshProUGUI>().text = "ID " + people[ini + auxJ].getID().ToString();
            line.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Lab " + people[ini + auxJ].getLab().ToString();
            ini = 0;
            posJugador = 0;
        }
        for (int j = auxJ; j < 9; j++)
        {
            Transform line = TV.transform.GetChild(0).GetChild(2).GetChild(j);
            line.GetChild(1).GetComponent<Image>().color = Color.cyan;
            line.GetChild(2).GetComponent<TextMeshProUGUI>().text = (ini + j + 1).ToString();
            bool misMuertos = false;
            if (ini + j > posJugador)
            {
                j--;
                misMuertos = true;
            }
            line.GetChild(3).GetComponent<TextMeshProUGUI>().text = "ID " + people[ini + j].getID().ToString();
            line.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Lab " + people[ini + j].getLab().ToString();
            if (misMuertos) j++;

            if (ini + j == posJugador)
            {
                j++;
                line = TV.transform.GetChild(0).GetChild(2).GetChild(j);
                line.GetChild(1).GetComponent<Image>().color = Color.green;
                line.GetChild(2).GetComponent<TextMeshProUGUI>().text = (ini + j + 1).ToString();
                line.GetChild(3).GetComponent<TextMeshProUGUI>().text = "ID " + people[ini + j].getID().ToString();
                line.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Lab " + people[ini + j].getLab().ToString();
            }
        }
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
