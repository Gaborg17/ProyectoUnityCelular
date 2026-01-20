using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float raisingSpeed;

    private void Update()
    {
        UpwardsMovement();
    }

    private void UpwardsMovement()
    {
        transform.Translate(Vector3.up * raisingSpeed * Time.deltaTime,Space.World);
    }

}
