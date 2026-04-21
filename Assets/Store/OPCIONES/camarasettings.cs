using UnityEngine;

public class MicroShakeRotacional : MonoBehaviour
{
    [Header("Ajustes del Temblor")]
    [Range(0f, 20f)]
    [Tooltip("Qué tan rápido vibra el pulso. Para un pulso nervioso, usa valores altos (10-15).")]
    public float frecuencia = 12f;

    [Range(0f, 1f)]
    [Tooltip("La amplitud de la vibración. Mantén este valor muy bajo para que sea sutil (0.01 - 0.05).")]
    public float intensidad = 0.02f;


    private Quaternion rotacionOriginal;
    private float tiempoInicial;

    void Start()
    {
        rotacionOriginal = transform.localRotation;
        tiempoInicial = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (intensidad > 0)
        {
            AplicarMicroShake();
        }
    }

    private void AplicarMicroShake()
    {
        float t = tiempoInicial + Time.unscaledTime * frecuencia;
        float shakeX = (Mathf.PerlinNoise(t, 0f) - 0.5f) * intensidad;
        float shakeY = (Mathf.PerlinNoise(0f, t) - 0.5f) * intensidad;

        float shakeZ = (Mathf.PerlinNoise(t * 0.7f, t * 0.7f) - 0.5f) * intensidad * 0.5f;


        Quaternion desviacionAngular = Quaternion.Euler(shakeX, shakeY, shakeZ);

       
        transform.localRotation = rotacionOriginal * desviacionAngular;
    }

    
    public void ActualizarRotacionBase()
    {
        rotacionOriginal = transform.localRotation;
    }
}