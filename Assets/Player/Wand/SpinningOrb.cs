using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningOrb : MonoBehaviour
{
    public ParticleSystem orbPrefab;
    public ParticleSystem hitEffect;
    public EnemySpawner enemySpawner;

    [SerializeField] private float degreesPerSecond = 90f;
    [SerializeField] private float range = 1f;

    private float angle = 0f;

    private GameObject player;
    private ParticleSystem spawnedOrb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnedOrb = Instantiate(orbPrefab);
    }

    void FixedUpdate()
    {
        angle += degreesPerSecond * Time.deltaTime;
        if (angle > 360)
		{
            angle -= 360;
		}
        var rotate = Quaternion.AngleAxis(angle, Vector3.forward);

        var pos = new Vector3(range, 0f, 0f);
        pos = rotate * pos + player.transform.position;

        spawnedOrb.transform.position = pos;

        var size = 0.5f;
        var hsize = size / 2f;
        Rect rect = new Rect(pos.x - hsize, pos.y - hsize, size, size);

        var intersecting = enemySpawner.GetIntersecting(rect, 0.5f);
        if (intersecting != null)
        {
            enemySpawner.Kill(intersecting);

            Instantiate(hitEffect, pos, rotate);
        }
    }
}
