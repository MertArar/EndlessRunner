using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateLevel : MonoBehaviour
{
    public static PlayerMove playerMove;
    public GameObject[] section;
    public float initialCreateSectionTime = 45f;
    private float createSectionTime = 45f;
    private float deleteSectionTime = 80f;
    public bool creatingSection = false;
    public int secNum;
    
    public string parentName;

    private void Start()
    {
        parentName = transform.name;
        createSectionTime = initialCreateSectionTime;
        StartCoroutine(GenerateSection());
        StartCoroutine(DestroyClone());
    }

    private void Update()
    {
        
    }

    IEnumerator GenerateSection()
    {
        float zPos = transform.position.z + 50f;

        while (true)
        {
            yield return new WaitForSeconds(createSectionTime);
            secNum = Random.Range(0, 4);
            Instantiate(section[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
            zPos += 100f;
            yield return new WaitForSeconds(createSectionTime);
        }
    }

    IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(80);
        if (parentName == "Section(Clone)")
        {
            while (true)
            {
                yield return new WaitForSeconds(deleteSectionTime);
                Destroy(gameObject);
                yield return new WaitForSeconds(deleteSectionTime);
            }
        }
    }
}