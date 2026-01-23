using System.Collections;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public float cooldown;
    public float damage;
    public float playerSpeedMultiplier;

    public static bool isOnCooldown;
    public static float counter;



    public Color tempColor;
    public abstract void Activate(GameObject user);

    public IEnumerator Cooldown()
    {
         counter = cooldown;
        Debug.Log(abilityName + " is in cooldown of " + counter);
        while (counter > 0)
        {
            isOnCooldown = true;
            counter -= Time.deltaTime;
            yield return null;
        }
        isOnCooldown = false;
    }

}
