using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroChair : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    private Coroutine coroutine;
    void Start()
    {
        Vector3 vel = new Vector3(5.0f, 5.0f, 0.0f);
        rigidbody.angularVelocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator WaitForMovement()
    {
        while (coroutine != null)
        {
            yield return null;
        }
    }

    public void SlowDown1()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SlowDown1Coroutine());
    }
    
    IEnumerator SlowDown1Coroutine()
    {
        Vector3 startVel = new Vector3(5.0f, 5.0f, 0.0f);
        Vector3 targetVel = new Vector3(-1.5f, 2.0f, 0.0f);
        float startTime = Time.time;
        float timeToSlow = 2.0f;
        while (Time.time - startTime < timeToSlow)
        {
            Vector3 newVel = Vector3.Lerp(startVel, targetVel, (Time.time - startTime) / timeToSlow);
            rigidbody.angularVelocity = newVel;
            yield return null;
        }

        rigidbody.angularVelocity = targetVel;
        coroutine = null;
    }

    public void SpeedUp1()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SpeedUp1Coroutine());
    }
    
    IEnumerator SpeedUp1Coroutine()
    {
        Vector3 startVel = new Vector3(-1.5f, 2.0f, 0.0f);
        Vector3 targetVel = new Vector3(4.0f, -2.0f, 0.0f);
        float startTime = Time.time;
        float timeToSlow = 2.0f;
        while (Time.time - startTime < timeToSlow)
        {
            Vector3 newVel = Vector3.Lerp(startVel, targetVel, (Time.time - startTime) / timeToSlow);
            rigidbody.angularVelocity = newVel;
            yield return null;
        }

        rigidbody.angularVelocity = targetVel;
        coroutine = null;
    }

    public void SlowDown2()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SlowDown2Coroutine());
    }
    
    IEnumerator SlowDown2Coroutine()
    {
        Vector3 startVel = new Vector3(4.0f, -2.0f, 0.0f);
        Vector3 targetVel = new Vector3(2.0f, 2.0f, 0.0f);
        float startTime = Time.time;
        float timeToSlow = 2.0f;
        while (Time.time - startTime < timeToSlow)
        {
            Vector3 newVel = Vector3.Lerp(startVel, targetVel, (Time.time - startTime) / timeToSlow);
            rigidbody.angularVelocity = newVel;
            yield return null;
        }

        rigidbody.angularVelocity = targetVel;
        coroutine = null;
    }

    public void SlowDown3()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SlowDown3Coroutine());
    }
    
    IEnumerator SlowDown3Coroutine()
    {
        Vector3 startVel = new Vector3(2.0f, 2.0f, 0.0f);
        Vector3 targetVel = new Vector3(0.5f, 0.1f, 0.0f);
        float startTime = Time.time;
        float timeToSlow = 2.0f;
        while (Time.time - startTime < timeToSlow)
        {
            Vector3 newVel = Vector3.Lerp(startVel, targetVel, (Time.time - startTime) / timeToSlow);
            rigidbody.angularVelocity = newVel;
            yield return null;
        }

        rigidbody.angularVelocity = targetVel;
        coroutine = null;
    }
}
