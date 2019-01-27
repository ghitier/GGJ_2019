using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexteSpawner : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnSentences");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnSentences()
    {
        while (true)
        {
            GameObject newGO = new GameObject("myTextGO");
            newGO.transform.SetParent(this.transform);

            Text myText = newGO.AddComponent<Text>();
            myText.text = "" + GameManager.instance.ballData[Random.Range(0, GameManager.instance.ballData.Count)];
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
        
    }
}
