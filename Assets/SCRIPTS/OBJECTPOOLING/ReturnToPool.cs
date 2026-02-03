using UnityEngine;
using System.Collections;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private string poolName;
    [SerializeField] private float returnDelay = 0f;

    private ObjectPooling poolManager;

    private void Start()
    {
        poolManager = FindAnyObjectByType<ObjectPooling>();
    }

    public void Return()
    {
        if (poolManager != null)
        {
            if (returnDelay > 0)
                StartCoroutine(ReturnAfterDelay());
            else
                poolManager.ReturnToPool(poolName, gameObject);
        }
    }

    private IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSeconds(returnDelay);
        poolManager.ReturnToPool(poolName, gameObject);
    }


}
