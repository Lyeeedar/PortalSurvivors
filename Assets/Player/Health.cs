using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth = 100;

    private float damageCooldown = 0f;

	private void Update()
	{
		if (damageCooldown > 0f)
		{
			damageCooldown -= Time.deltaTime;
		}
	}

	public void Damage(int amount)
	{
		if (damageCooldown <= 0f)
		{
			currHealth -= amount;
			damageCooldown = 0.1f;

            StartCoroutine("HitFlash");
        }
	}

    public IEnumerator HitFlash()
    {
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

		sprites[0].color = new Color(1f, 0f, 0f, 1f);

		yield return new WaitForSeconds(0.1f);

		sprites[0].color = new Color(1f, 1f, 1f, 1f);
	}
}
