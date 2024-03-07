using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string parentName;

    void Start()
    {
        parentName = transform.name;
        StartCoroutine(DestroyClone());
    }

    IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(20);
        if (parentName == "Section(Clone)")
        {
            while (true)
            {
                yield return new WaitForSeconds(15);
                Destroy(gameObject);
                yield return new WaitForSeconds(15);
            }
        }
    }
}