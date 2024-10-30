using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Los tres objetos a instanciar
    /*public GameObject object1;
    public GameObject object2;
    public GameObject object3;*/
    public List<GameObject> objects;

    // Lista de posiciones para generar los objetos
    public List<Transform> spawnPositions;

    // Tiempo en segundos para generar cada objeto
    private float spawnInterval = 4f;
    private float timer;

    void Start()
    {
        // Inicializa el temporizador
        timer = spawnInterval;
    }

    void Update()
    {
        // Cuenta regresiva
        timer -= Time.deltaTime;

        // Verifica si han pasado los 4 segundos
        if (timer <= 0f)
        {
            SpawnRandomObject();
            // Reinicia el temporizador
            timer = spawnInterval;
        }
    }

    void SpawnRandomObject()
    {
        // Escoge un objeto aleatorio
        GameObject selectedObject = GetRandomObject();

        // Escoge una posición aleatoria
        Transform selectedPosition = GetRandomPosition();

        // Instancia el objeto en la posición seleccionada
        GameObject instantiatedObject = Instantiate(selectedObject, selectedPosition.position, Quaternion.identity);
        
        // Asegúrate de que el objeto está activo
        instantiatedObject.SetActive(true);
    }

    GameObject GetRandomObject()
    {
        // Selecciona uno de los tres objetos
        int randomIndex = Random.Range(0, objects.Count);
        /*switch (randomIndex)
        {
            case 0: return object1;
            case 1: return object2;
            default: return object3;
        }*/
        return objects[randomIndex];
    }

    Transform GetRandomPosition()
    {
        // Selecciona una posición aleatoria de la lista
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}
