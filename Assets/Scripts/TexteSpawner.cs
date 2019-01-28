using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexteSpawner : MonoBehaviour
{

    public Text text;
    List<string> customList;
    

    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        customList = GameManager.instance.ballData;
        StartCoroutine("SpawnSentences");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnSentences()
    {
        while (true) {

            int myMarker = Random.Range(0, customList.Count);
            text.text ="" + customList[myMarker];
            

            customList.RemoveAt(myMarker);
            Vector3 spawnPosition = GetBottomLeftCorner(gameObject.GetComponent<RectTransform>()) - new Vector3(Random.Range(0, gameObject.GetComponent<RectTransform>().rect.x), Random.Range(0, gameObject.GetComponent<RectTransform>().rect.y), 0);
            text.transform.localPosition = spawnPosition;

            if (customList.Count == 0)
            {
                customList = GameManager.instance.ballData;
            }
            yield return new WaitForSeconds(10f);
        }
        
    }
}
