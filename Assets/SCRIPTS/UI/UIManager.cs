using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemCounter;
    [SerializeField] private TextMeshProUGUI timer;

    public bool inPlay;

    private void Start()
    {
        StartCoroutine(GameTimer());
    }
    private void Update()
    {
        GemCounterUpdate();
    }


    private void GemCounterUpdate()
    {
        gemCounter.text = GameManager.Instance.gemasTotales.ToString();
    }

    private IEnumerator GameTimer()
    {
        float time = 0;
        while (inPlay)
        {
            
            time += Time.deltaTime;

            timer.text = ((short)time).ToString();

            yield return null;
        }
    }

}
