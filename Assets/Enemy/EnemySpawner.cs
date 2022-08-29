using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerPos;
    public Health playerHealth;

    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float speed = 1f;

    private List<GameObject> gameObjects = new List<GameObject>();
    private float accumulator = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawn();

        Move();

        Collide();
    }

    void Spawn()
	{
        accumulator += Time.deltaTime * spawnRate;

        while (accumulator > 1f)
        {
            accumulator -= 1f;

            var position = new Vector3(Random.Range(playerPos.position.x - 10, playerPos.position.x + 10), Random.Range(playerPos.position.y - 10, playerPos.position.y + 10), 0);

            var spawned = Instantiate(enemyPrefab, position, Quaternion.identity);
            gameObjects.Add(spawned);
        }
    }

    void Move()
	{
        foreach (var obj in gameObjects)
        {
            var diff = playerPos.position - obj.transform.position;
            diff = diff.normalized * speed * Time.deltaTime;

            obj.transform.position += diff;
        }
    }

    void Collide()
	{
        var size = 0.2f;
        var hsize = size / 2;

        Rect playerRect = new Rect(playerPos.position.x - hsize, playerPos.position.y - hsize, size, size);
        var colliding = GetIntersecting(playerRect, size);
        if (colliding != null)
		{
            playerHealth.Damage(1);
        }
    }

    public GameObject GetIntersecting(Rect rect, float size)
	{
        var hsize = size / 2;
        foreach (var obj in gameObjects)
        {
            Rect objRect = new Rect(obj.transform.position.x - hsize, obj.transform.position.y - hsize, size, size);

            if (rect.Overlaps(objRect))
            {
                return obj;
            }
        }

        return null;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 20), "Enemy Count: " + gameObjects.Count);
    }

    public GameObject GetClosestTo(Vector3 point)
	{
        float bestDist = float.MaxValue;
        GameObject bestObj = null;

        foreach (var obj in gameObjects)
		{
            var diff = obj.transform.position - point;
            var dist = diff.sqrMagnitude;

            if (dist < bestDist)
			{
                bestDist = dist;
                bestObj = obj;
			}
		}

        return bestObj;
	}

    public void Kill(GameObject gameObject)
	{
        Destroy(gameObject);
        gameObjects.Remove(gameObject);
	}
}
