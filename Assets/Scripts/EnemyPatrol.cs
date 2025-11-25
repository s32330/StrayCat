using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform LeftPoint;
    public Transform RightPoint;
    public float speed = 2f;

    private Transform targetPoint;   // aktualny cel
    private float startY;            // wysokoœæ enemy – trzymamy sta³¹

    void Start()
    {
        targetPoint = RightPoint;    // na pocz¹tku idzie w prawo
        startY = transform.position.y;
    }

    void Update()
    {
        // ruch TYLKO po X
        float newX = Mathf.MoveTowards(
            transform.position.x,
            targetPoint.position.x,
            speed * Time.deltaTime
        );

        transform.position = new Vector2(newX, startY);

        // kiedy dojdzie do punktu – zmiana kierunku
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.05f)
        {
            targetPoint = (targetPoint == LeftPoint) ? RightPoint : LeftPoint;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}