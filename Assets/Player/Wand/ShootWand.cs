using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWand : MonoBehaviour
{
    public GameObject projectilePrefab;
    public EnemySpawner enemySpawner;

    [SerializeField] private float speed = 1f;

    private GameObject player;

    private float accumulator;

	private void Start()
	{
        player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
    {
        accumulator += speed * Time.deltaTime;

        while (accumulator > 1f)
		{
            accumulator -= 1f;

            var target = enemySpawner.GetClosestTo(player.transform.position);

            var vec = target.transform.position - player.transform.position;

            var angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90f;

            GameObject obj = Instantiate(projectilePrefab, player.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            ProjectileMove projectile = obj.GetComponent<ProjectileMove>();
            projectile.enemySpawner = enemySpawner;
        }
    }
}
