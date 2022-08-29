using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerPos;

    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float speed = 1f;

    private List<GameObject> gameObjects = new List<GameObject>();
    private float accumulator = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        accumulator += Time.deltaTime * spawnRate;

        while (accumulator > 1f)
		{
            accumulator -= 1f;

            var position = new Vector3(Random.Range(playerPos.position.x - 10, playerPos.position.x + 10), Random.Range(playerPos.position.y - 10, playerPos.position.y + 10), 0);

            var spawned = Instantiate(enemyPrefab, position, Quaternion.identity);
            gameObjects.Add(spawned);
        }

        foreach (var obj in gameObjects)
		{
            var diff = playerPos.position - obj.transform.position;
            diff = diff.normalized * speed * Time.deltaTime;

            obj.transform.position += diff;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 20), "Enemy Count: " + gameObjects.Count);
    }
}
