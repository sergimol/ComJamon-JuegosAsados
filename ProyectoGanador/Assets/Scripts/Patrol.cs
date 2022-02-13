using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public float speed;
    float waitTime;
    public float startWaitTime;

    Transform[] moveSpots;
    private int randomSpot;

    float tiempoGiroSuave = 0.1f;
    float velocidadGiroSuave;

    float actualTime = 0f;
    float threshold = 10f, thresholdTimeToReach = 35f;
    //Jodete danlles
    //Rigidbody rigid;
    private void Awake()
    {
        moveSpots = GameManager.instance.getWalkingPoints();
    }

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        //rigid = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        //Tomamos solamente las coordenadas X y Z e ignoramos la Y, porque se le puede ir la olla
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 gotoPos = new Vector2(moveSpots[randomSpot].position.x, moveSpots[randomSpot].position.z);

        //Si estamos en el destino...
        if (Vector2.Distance(pos, gotoPos) < 0.2f || actualTime > thresholdTimeToReach)
        {
            //Paramos la velocidad
            //rigid.velocity = Vector3.zero;
            
            //Empezamos a esperar...
            if (waitTime <= 0){
                //Si ha acabado el tiempo de espera, elegimos un lugar aleatorio nuevo
                int previousSpot = randomSpot;
                bool isValid = false;
                int count = 0;
                while ((randomSpot == previousSpot || !isValid) && count < 30){
                    randomSpot = Random.Range(0, moveSpots.Length);
                    
                    isValid = Mathf.Abs(moveSpots[randomSpot].position.x - pos.x) <= threshold;
                    
                    isValid = isValid || Mathf.Abs(moveSpots[randomSpot].position.z - pos.y) <= threshold;
                    
                    count++;
                }

                if (count >= 30) Debug.Log("Mal");
                //Reseteamos futuro tiempo de espera
                waitTime = startWaitTime;
                actualTime = 0f;
            }
            else{
                waitTime -= Time.deltaTime;
            }
        }
        else{
            //La direccion será el spot actual hacia el que vamos
            Vector3 dir = moveSpots[randomSpot].position - transform.position;

            //No necesitamos movimiento en y
            dir.y = 0;
            dir.Normalize();
            dir *= speed;

            //Rotación del personaje hacia donde camina (suavizado)
            float anguloDestino = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            //Esto es raro pero Brackeys dice que funciona
            float anguloSuave = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloDestino, ref velocidadGiroSuave, tiempoGiroSuave);

            transform.rotation = Quaternion.Euler(0f, anguloSuave, 0f);

            transform.position += dir * Time.deltaTime;

            actualTime += Time.deltaTime;
        }
    }
}
