using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public Transform rayOrigin;

    public LayerMask grndMasks;

    public float detectionDistance;

    public bool rayDraw;


    public bool IsGrounded()
    {
        return Physics.CheckBox(rayOrigin.position,new Vector3(detectionDistance,.2f,detectionDistance),Quaternion.identity,grndMasks);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (rayDraw && rayOrigin != null)
        {
            Gizmos.DrawWireCube(rayOrigin.position, new Vector3(detectionDistance, .2f, detectionDistance));
        }
    }

}
