using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFireRate = 0.1f;

    Coroutine fireCoroutine;

    float xMin, xMax, yMin, yMax;
    float padding = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 1;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + 1;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true) {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFireRate);
            
        }
    }

    private void Move()
    {
        transform.position = new Vector2(HorizontalMovement(), VerticaMovement());

    }

    private float VerticaMovement()
    {
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var nextYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        return nextYPosition;
    }

    private float HorizontalMovement()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //it makes the movement fps independent
        var nextXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        return nextXPosition;
    }
}
