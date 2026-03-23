using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float raisingSpeed;

    [SerializeField] private float timeBetweenIncrements;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;


    [SerializeField] private float delayOnStart = 1f;

    
    public bool moveCamera = true;
    private bool enableMove = false;

    [SerializeField]private GameObject fondo;
    [SerializeField]
    private float raisingSpeedFondo;


    private void Start()
    {
        StartCoroutine(IncrementSpeed());
        StartCoroutine(DelayOnStart());
    }
    private void Update()
    {
        if (enableMove)
        {
            UpwardsMovement();
            MoveBG();
        }
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

    private IEnumerator DelayOnStart()
    {
        yield return new WaitForSeconds(delayOnStart);
        enableMove = true;
    }


    [ContextMenu("SetPos")]
    public void SetToPlayerPos()
    {
        enableMove = false;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);

        StartCoroutine(DelayOnStart());

    }
}
