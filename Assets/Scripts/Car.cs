using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rotator rotator;

    public bool canMove = false;

    private Vector3 startPos;

    private Vector3 startRotation;

    public int moveIndex = 0;

    public Vector3[] positions;

    public float speed = 5f;

    public GameObject vfxSuccess;

    private void Awake()
    {
        startPos = transform.position;

        startRotation = transform.eulerAngles;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rotator" && collision.gameObject.GetInstanceID() != rotator.gameObject.GetInstanceID())
        {
            GameObject explosion = Instantiate(vfxSuccess, transform.position, transform.rotation);
            Destroy(explosion, .75f);
            ResetPos();

            collision.gameObject.GetComponent<Rotator>().Damanged();
        }
    }

    private void Update()
    {
        if (!canMove)
        {
            transform.RotateAround(rotator.transform.position, new Vector3(0f, 0f, -1f), 90f * Time.deltaTime);
        }
        else
        {
            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(new Vector3(transform.position.x, transform.position.y, 0f), currentPos, speed * Time.deltaTime);

            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.05f)
            {
                moveIndex++;
            }

            if (moveIndex > positions.Length - 1)
            {
                ResetPos();
            }
        }

    }

    private void ResetPos()
    {
        canMove = false;

        transform.position = startPos;

        transform.eulerAngles = startRotation;

        positions = new Vector3[] { };

        moveIndex = 0;
    }
}