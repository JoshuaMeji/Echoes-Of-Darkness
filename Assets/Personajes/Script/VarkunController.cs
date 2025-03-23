using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VarkunController : MonoBehaviour
{
    // Atributo para las rutinas del enemigo 
    public int rutina;
    // Cronometro para el tiempo entre rutinas
    public float cronometro;
    // Recibe el animator del enemigo
    public Animator ani;
    // Angulo para rotar el enemigo
    public Quaternion angulo;
    // para detectar el grado del angulo
    public float grado;
 
    //HACER QUE EL ENEMIGO DETECTE EL JUGADOR   
    // Este atributo actuara como un target
    public GameObject target;
 
    // un boolean para cuando el enemigo este atacando
    public bool atacando;
 
 
    // Start is called before the first frame update
    void Start()
    {
        // Haremos que ani tenga el componente animator
        ani = GetComponent<Animator>();
        // Haremos que el target sea igual al personaje principal
        target = GameObject.Find("Personaje1");
    }
 
    
    public void Comportamiento_Enemigo(){
 
        // Un if para cuando la posicion del enemigo y del target (que en este caso es el jugador) esten a una distancia mayor a 5 
        // (osea cuando el jugador este en el rango fuera del enemigo)
 
        if (Vector3.Distance(transform.position, target.transform.position) > 5){
 
            // Se cancelara la animacion de correr 
            ani.SetBool("isRunning", false);
            // Haremos correr el cronometro sumando 1 por Time.deltaTime
            cronometro += 1 * Time.deltaTime;
            if(cronometro >=4){
                // Se sacara un numero random entre 0 y 2
                rutina = Random.Range(0, 2);
                // y luego se reiniciar el cronometro
                cronometro = 0;
            }
            switch (rutina){
                case 0:
                    // En este caso se quiere que el enemigo se QUEDE QUIETO entonces cancelaremos su animacion de CAMINAR
                    ani.SetBool("isWalking", false);
 
                    break;
 
                case 1:
                    // En este caso determinaremos la direccion en que se va
                    // Se escogera un numero random entre el 0 y el 360 para que se mueva aleatoriamente en una direccion
                    grado = Random.Range(0, 360);
                    // Angulo tendra el valor de grado en el eje Y
                    angulo = Quaternion.Euler(0, grado, 0);
                    // La rutina se sumara + 1
                    rutina++;
                    break;
                case 2:
                    // la rotacion del enemigo sera igual al angulo establecido
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    // Se movera adelante con la velocidad de 1 
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    // Se activara la animacion de CAMINAR
                    ani.SetBool("isWalking", true);
 
                    break;
            }
        } 
        // En caso contrario que el jugador si este en el rango del enemigo, pues que sea perseguido
        else {
 
            // Condicion if cuando este en una distancia mayor a 1 y que el atacando sea falso, entonces se pondra el codigo para que el enemigo no siga
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando){
                // Se crea variable que sera igual a la posicion del target ( es decir el jugador principal ) menos la posiocion del enemigo
                var lookPos = target.transform.position - transform.position;
                // 
                lookPos.y = 0;
                // Variable de rotacion que tendra un quaternion.lookrotation que se le entregara de parametro un lookPos
                var rotation = Quaternion.LookRotation(lookPos);
                // Ahora el enemigo rotara segun el rango de rotacion y le daremos una velocidad de 2
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                // Se cancelara la animacion de CAMINAR
                ani.SetBool("isWalking", false);
 
                // Se agregar una animacion de correr
                ani.SetBool("isRunning", true);
                // Haremos que se dezplaze hacia adelante con una velocidad mayor al caminar
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
 
                // Se pondra la animacion de falso
            } 
            //  En caso contarios que esta a menos de 1 de distancia es decir que este alapar
            else{
                // Se cancelara la animacion de CAMINAR
                ani.SetBool("isWalking", false);
                // Se cancelara la animacion de CORRER
                ani.SetBool("isRunning", false);
 
                // La animacion de ataque seran VERDADERA y el booleando de atacando sera verdadero 
                ani.SetBool("isAttacking", true);
                atacando = true;
            }
 
        }
    }
 
    // Metodo qpara cancelar tanto la animaciaon de ataque como el booleando de atacando
 
    public void Final_Ani(){
        ani.SetBool("isAttacking", false);
        atacando = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }
}