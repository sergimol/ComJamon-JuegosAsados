using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetect : MonoBehaviour
{
    [SerializeField]
    int lab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<MeshRenderer>().isVisible)
        {
            //Visible code here
            GameManager.instance.ResetLabo(lab);
            AudioManager.instance.Stop(AudioManager.ESounds.eoweo);
            this.enabled = false;
        }
    }
}
