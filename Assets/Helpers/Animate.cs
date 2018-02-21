using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {
    static public Animate instance;

    void Awake()
    {
        instance = this;
    }

    static public Coroutine Scale(GameObject obj, Vector3 targetScale, float duration = 1)
    {
        return instance.StartCoroutine(ScaleTransform(obj, targetScale, duration));        
    }

	static private IEnumerator ScaleTransform(GameObject obj, Vector3 targetScale, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime <= duration && obj)
        {
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(obj) obj.transform.localScale = targetScale;
    }


    static public Coroutine Position(GameObject obj, Vector3 targetPosition, float duration = 1)
    {
        return instance.StartCoroutine(PositionTransform(obj, targetPosition, duration));        
    }

    static private IEnumerator PositionTransform(GameObject obj, Vector3 targetPosition, float duration){
        float elapsedTime = 0;

        while (elapsedTime <= duration && obj)
        {
            obj.transform.localScale = Vector3.Lerp(obj.transform.localPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(obj) obj.transform.localPosition = targetPosition;
    }

    static public Coroutine PositionAdd(GameObject obj, Vector3 addedPosition, float duration = 1)
    {
        return instance.StartCoroutine(PositionAddTransform(obj, addedPosition, duration));        
    }

    static private IEnumerator PositionAddTransform(GameObject obj, Vector3 addedPosition, float duration){
        float elapsedTime = 0;
        int count = 0;
        while (elapsedTime <= duration && obj)
        {

            // obj.transform.localScale = Vector3.Lerp(obj.transform.localPosition, obj.transform.localPosition + addedPosition, elapsedTime / duration);
            obj.transform.localScale = Vector3.Lerp(obj.transform.localPosition, addedPosition, elapsedTime / duration);
            // Debug.Log(obj.transform.localScale);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            count += 1;
        }
        
        if(obj) obj.transform.localPosition = obj.transform.localPosition + addedPosition;
    }

    static public Coroutine Pulse(GameObject obj, float scale, float speed, float duration)
    {
        return instance.StartCoroutine(PulseTransform(obj, scale, speed, duration));        
    }

    static public IEnumerator PulseTransform(GameObject obj, float scale, float speed, float duration)
    {
        yield return true;

        // float elapsedTime = 0;
        // Vector3 initialScale = obj.transform.localScale;

        // Debug.Log(elapsedTime);
        
        // Debug.Log(duration);
        // Debug.Log(speed);
        // Debug.Log(duration - speed);

        // while (elapsedTime <= duration - speed)
        // {
            
        //     elapsedTime += Time.deltaTime;

        //     Debug.Log("tu!");

        //     // yield return true;
        //     if(Mathf.Round(elapsedTime / speed) % 2 == 0){
        //         obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, initialScale + scale, speed);
        //         elapsedTime += Time.deltaTime;
        //         yield return new WaitForEndOfFrame();
        //     }

        //     if(Mathf.Round(elapsedTime / speed) % 2 == 1){
        //         obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, initialScale - scale, speed);
        //         elapsedTime += Time.deltaTime;
        //         yield return new WaitForEndOfFrame();
        //     }

        //     obj.transform.localScale = Vector3.Lerp(obj.transform.localPosition, obj.transform.localPosition + addedPosition, elapsedTime / duration);
        //     obj.transform.localScale = Vector3.Lerp(obj.transform.localPosition, obj.transform.localPosition, (elapsedTime - duration) / speed);
        //     Debug.Log(obj.transform.localScale);
        //     yield return new WaitForEndOfFrame();
        // }

        // if(obj) obj.transform.localScale = initialScale;
    }
}
