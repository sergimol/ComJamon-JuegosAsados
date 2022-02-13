using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    Transform spawner;
    [SerializeField]
    int peopleToCreate, labNum;
    [SerializeField]
    List<Transform> prefabs;
    public float mainVolSlider = 0.2f,
                 SFXVolSlider = 0.2f,
                 musicVolSlider = 0.2f;

    private List<Transform> children;
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
        ClassificationManager.instanceCM.startCM();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InicializarPeople()
    {
        children = new List<Transform>();

        foreach (Transform child in spawner)
        {
            children.Add(child);
        }

        Mezcla(children);
        Mezcla(prefabs);

        if (peopleToCreate > children.Count || peopleToCreate > prefabs.Count)
        {
            Debug.LogError("Cagaste, crea m�s hijos o m�s refabs de personas");
            return;
        }
        for (int i = 0; i < peopleToCreate; i++)
        {
            children[i].GetComponent<Spawner>().setPrefab(prefabs[i]);
        }

        int comPorLab = spawner.childCount / labNum;
        for (int i = 0; i < spawner.childCount; i++)
        {
            if (spawner.GetChild(i).transform.childCount > 5)        //CAMBIAR CUANDO PONGAMOS MÁS HIJOS
            {               
                spawner.GetChild(i).GetChild(5).gameObject.GetComponent<StudentInfo>().SetLab(i / comPorLab);
            }
        }
    }

    public void Mezcla<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
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

        int peopleInLab = spawner.childCount / labNum;
        int firstToReestart = peopleInLab * lab;

        for (int i = 0; i < peopleInLab; i++)
        {
            spawner.GetChild(firstToReestart + i).GetComponent<Spawner>().createChild();
        }
    }

    public int getNumPeople()
    {
        return peopleToCreate;
    }

    public List<Transform> getSpawnList()
    {
        return children;
    }

    public void MainSliderState(float volume)
    {
        mainVolSlider = volume;
    }
    public void MusicSliderState(float volume)
    {
        musicVolSlider = volume;
    }
    public void SFXSliderState(float volume)
    {
        SFXVolSlider = volume;
    }
}
