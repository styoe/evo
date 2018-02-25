using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public bool isDestroying;
    // Use this for initialization
    void Start()
    {
        isDestroying = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Destroy()
    {
        isDestroying = true;
        StartCoroutine(DestroyRoutione());
    }

    public IEnumerator DestroyRoutione()
    {
        float totalDuration = 4;

        Animate.Scale(gameObject, new Vector3(0f, 0f, 0f), totalDuration);
        yield return new WaitForSeconds(totalDuration);

        Destroy(gameObject);
    }
}
