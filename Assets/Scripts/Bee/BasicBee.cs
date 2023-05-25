using System;
using System.Collections;
using UnityEngine;

public class BasicBee : MonoBehaviour
{
    public float scaleDuration = 1.0f;
    public float maxScale = 1.0f;
    public int Health = 100;
    public float Speed = 3;

    protected const float Delta = 0.1f;

    private Vector3 _originalScale;
    private float frame = 0f;

    protected void UpdateAnimationDirection(Rigidbody2D rigidbody) =>
        GetComponent<SpriteRenderer>().flipX = rigidbody.velocity.x < 0.01f;

    protected void MoveToTarget(GameObject target)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = rigidbody.position;
        Vector2 directionToTarget = (targetPosition - currentPosition).normalized;
        frame = frame < 100 ? frame + 0.01f : 0;
        float wobbleAngle = UnityEngine.Random.Range(-Globals.MaxWobbleAngle, Globals.MaxWobbleAngle);
        var wobbleRotation =
            Quaternion.Euler(0f, 0f, (float)Math.Sin(frame) * Globals.MaxWobbleAngle / 3 + wobbleAngle);
        Vector2 wobbledDirection = wobbleRotation * directionToTarget;

        Vector2 moveForce = wobbledDirection * Speed - rigidbody.velocity;
        rigidbody.AddForce(moveForce, ForceMode2D.Impulse);
        UpdateAnimationDirection(rigidbody);
    }

    void Start()
    {
        _originalScale = transform.localScale;
        StartCoroutine(ScaleUp());
    }

    IEnumerator ScaleUp()
    {
        transform.localScale = Vector3.zero;
        float timeElapsed = 0.0f;
        while (timeElapsed < scaleDuration)
        {
            float scaleFactor = timeElapsed / scaleDuration * maxScale;
            Vector3 newScale = _originalScale * scaleFactor;
            transform.localScale = newScale;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = _originalScale;
    }

    // проходим цикл аним
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (!Globals.InBounds(transform.position))
        {
            DestroyBee();
        }
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Globals.GameOutcome = GameOutcome.Loss;
            if (gameObject.GetComponent<HouseForBees>() != null)
                gameObject.GetComponent<HouseForBees>().Loss();
            else
                DestroyBee();
        }
    }

    public void DestroyBee()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PushAway(collision);
    }

    public void OnCollisionStay2D(Collision2D collisionInfo)
    {
        PushAway(collisionInfo);
    }


    private void PushAway(Collision2D collision)
    {
        // var rb = GetComponent<Rigidbody2D>();
        // Vector3 normal = collision.contacts[0].normal;
        // rb.AddForce(normal * CollisionForce, ForceMode2D.Impulse);
    }
}