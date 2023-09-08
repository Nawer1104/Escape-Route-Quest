using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Car[] cars;

    public static Rotator Instance;

    public bool canDraw = true;

    private int health = 3;

    private void Awake()
    {
        Instance = this;

    }

    private void OnMouseDown()
    {
        if (!canDraw) return;
        DrawWithMouse.Instance.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        if (!canDraw) return;
        DrawWithMouse.Instance.Updateline();
    }

    private void OnMouseUp()
    {
        if (!canDraw) return;

        Vector3[] newPos = new Vector3[DrawWithMouse.Instance.line.positionCount];

        foreach (Car car in cars)
        {
            car.positions = newPos;
            DrawWithMouse.Instance.line.GetPositions(car.positions);
            car.canMove = true;

            DrawWithMouse.Instance.ResetLine();
        }
    }

    public void Damanged()
    {
        health -= 1;

        if (health == 0)
        {
            StartCoroutine(RemoveFromList());
        }
    }

    IEnumerator RemoveFromList()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;

        foreach (Car car in cars)
        {
            car.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].rotators.Remove(gameObject);

        gameObject.SetActive(false);
    }
}