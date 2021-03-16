using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
    private Vector2 direction;
    public float hitboxSize;
    public LayerMask layersToHit;

    void Update()
    {
        Collider2D hitbox = Physics2D.OverlapCircle(transform.position, hitboxSize, layersToHit);

        if (hitbox)
        {
            if (hitbox.tag == "Button") hitbox.GetComponent<Button>().Activate();
            Destroy(gameObject, 0f);
        }

        transform.Translate(direction * Time.deltaTime);
    }

    public void Initialize(Vector3 newDirection)
    {
        direction = new Vector2(newDirection.x, newDirection.y).normalized * speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitboxSize);
    }
}
