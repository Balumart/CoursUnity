using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [Tooltip("prefab a spawn")]
    public Transform prefabAI;
    [Tooltip("point de spawn des ia")]
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Transform ai = SpawnAI();
    }

    Transform SpawnAI()
    {
        //le game object spawn a chaque démarrage du jeu
        Transform ai = GameObject.Instantiate<Transform>(prefabAI);
        //Prend la position de la porte (transform.position) et la met sur l'ai
        ai.position = spawnPoint.position;
        ai.rotation = spawnPoint.rotation;
        return ai;
    }

    void AddPichenette(Transform ai, Vector3 pichenette)
    {
        Rigidbody rb = ai.GetComponent<Rigidbody>();
    }

    public float time = 0;
    [Range(0, 15)]
    public float timeMax = 1;

    private Vector3 lastPichenette;

    // Update is called once per frame
    void Update()
    {
        //Met un setting de temps
        time = time + Time.deltaTime;
        if (time >= timeMax)
        {
            Transform ai = SpawnAI();
            time = 0;
        }
    }
}