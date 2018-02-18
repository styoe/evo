using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private int cookiesEaten;
    private SphereCollider sphereColider;

    void Start()
    {
        sphereColider = GetComponent<SphereCollider>();
        cookiesEaten = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cookie")
        {
            //Make it grow
            var playerScale = this.gameObject.transform.localScale.x;
            var cookieScale = col.gameObject.transform.localScale.x;

            if (playerScale > cookieScale)
            {
                // Calculate the scale and radius by adding volumes of cookie and player
                float playerVolume = (4f / 3f) * Mathf.PI * Mathf.Pow(playerScale, 3);
                float cookieVolume = (4f / 3f) * Mathf.PI * Mathf.Pow(cookieScale, 3);
                float newPlayerVolume = playerVolume + cookieVolume;
                float newPlayerRadius = Mathf.Pow(newPlayerVolume / (4f / 3f * Mathf.PI), 1f / 3f);
                float newPlayerScale = newPlayerRadius * 2;
                float scaleIncrement = newPlayerScale - playerScale;

                Debug.Log(scaleIncrement);
                Debug.Log(newPlayerScale);

                // Set scale and radius of player
                // this.gameObject.transform.localScale = new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
                // sphereColider.radius = newPlayerRadius;

                StartCoroutine(Grow(new Vector3(scaleIncrement, scaleIncrement, scaleIncrement)));

                //destroy cookie
                Destroy(col.gameObject);

                cookiesEaten += 1;
            }
        }

        if (col.gameObject.tag == "Powerup")
        {
            //Add script
            // addScript(this.gameObject, col.gameObject.name);

            switch (col.gameObject.name)
            {
                case "Fly":
                    StartCoroutine(UsePowerup<Fly>(5));
                    break;

                case "Jump":
                    StartCoroutine(UsePowerup<Jump>(10));
                    break;
            }

            Destroy(col.gameObject);
        }
    }

    IEnumerator UsePowerup<T>(float duration) where T : MonoBehaviour
    {
        // Remove an existing powerup of the same type
        var powerup = gameObject.GetComponent<T>();
        if (powerup != null) Destroy(powerup);

        // Add a new powerup and set its expiry
        powerup = gameObject.AddComponent<T>();
        yield return new WaitForSeconds(duration);
        Destroy(powerup);
    }

    IEnumerator Grow(Vector3 targetScale, float duration = 1)
    {
        float elapsedTime = 0;

        while (elapsedTime <= duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = targetScale;
    }

    // void addScript(GameObject obj, string ScriptName)
    // {
    //     System.Type ScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
    //     if (!obj.GetComponent(ScriptName))
    //     {
    //         obj.AddComponent(ScriptType);
    //     }
    // }
}
