using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float raisingSpeed;

    [SerializeField] private float timeBetweenIncrements;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;

    public bool moveCamera = true;

    [SerializeField]private GameObject fondo;
    [SerializeField]
    private float raisingSpeedFondo;


    private void Start()
    {
        StartCoroutine(IncrementSpeed());
    }
    private void Update()
    {
        UpwardsMovement();
        MoveBG();
    }

    private void UpwardsMovement()
    {
        if (moveCamera)
        {
            transform.Translate(Vector3.up * raisingSpeed * Time.deltaTime, Space.World);

        }
    }

    private IEnumerator IncrementSpeed()
    {
        while (raisingSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(timeBetweenIncrements);
            raisingSpeed = raisingSpeed * speedMultiplier;
            raisingSpeed = Mathf.Clamp( raisingSpeed, 0, maxSpeed);
        }
    }


    private void MoveBG()
    {
        fondo.transform.Translate(Vector3.up * raisingSpeedFondo * Time.deltaTime, Space.World);
    }

}
