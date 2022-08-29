using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public Vector3 direction;
    public EnemySpawner enemySpawner;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifetime = 1f;

    private float remaining;

	private void Start()
	{
        remaining = lifetime;
	}

	// Update is called once per frame
	void Update()
    {
        remaining -= Time.deltaTime;
        if (remaining <= 0f)
		{
            Destroy(gameObject);
            return;
		}

        transform.position += direction * speed * Time.deltaTime;

        var size = 0.5f;
        var hsize = size / 2f;
        Rect rect = new Rect(transform.position.x - hsize, transform.position.y - hsize, size, size);

        var intersecting = enemySpawner.GetIntersecting(rect, 0.5f);
        if (intersecting != null)
		{
            enemySpawner.Kill(intersecting);
            Destroy(gameObject);
        }
    }
}
