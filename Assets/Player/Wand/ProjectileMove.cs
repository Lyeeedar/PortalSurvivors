using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifetime = 1f;

    private Vector3 moveVector;

	private void Start()
	{
        moveVector = Vector3.up * speed;

        StartCoroutine(DestroyAfterTimeout());
	}

	void FixedUpdate()
    {
        transform.Translate(moveVector * Time.deltaTime);

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

    IEnumerator DestroyAfterTimeout()
	{
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
	}
}
