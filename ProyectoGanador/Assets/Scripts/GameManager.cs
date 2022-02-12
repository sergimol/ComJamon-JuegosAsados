using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    GameObject spawner;
    [SerializeField]
    int peopleToCreate, labNum;
    [SerializeField]
    List<Transform> prefabs;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)DateTime.Now.Ticks);
        InicializarPeople();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InicializarPeople()
    {
        List<Transform> childs = new List<Transform>();

        foreach (Transform child in spawner.transform)
        {
            childs.Add(child);
        }

        Mezcla(childs);
        Mezcla(prefabs);

        if (peopleToCreate > childs.Count || peopleToCreate > prefabs.Count)
        {
            Debug.LogError("Cagaste, crea más hijos o más refabs de personas");
            return;
        }
        for (int i = 0; i < peopleToCreate; i++)
        {
            childs[i].GetComponent<Spawner>().setPrefab(prefabs[i]);
        }
    }

    private void Mezcla(List<Transform> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Transform value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void ResetLabo(int lab)
    {
        if (lab > labNum)
        {
            Debug.LogError("Laboratorio invalido al resetear");
            return;
        }

        int peopleInLab = spawner.transform.GetChildCount() / labNum;
        int firstToReestart = peopleInLab * lab;

        for (int i = 0; i < peopleInLab; i++)
        {
            spawner.transform.GetChild(firstToReestart + i).GetComponent<Spawner>().createChild();
        }
    }
}
