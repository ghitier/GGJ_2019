using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_useless : MonoBehaviour
{
    public Transform destroyPlace;
    float DistanceToDestroyPLace;

    private void Start()
    {
        GameManager.dropBallDelegate += MesCouilles;
    }
    // Update is called once per frame
    void MesCouilles()
    {
       // DistanceToDestroyPLace = Vector3.Distance(destroyPlace.transform.position, transform.position);
        
       // if (DistanceToDestroyPLace < 2)
       // {
        //    Destroy(gameObject );
      //  }
    }
    private void OnDestroy()
    {
        GameManager.dropBallDelegate -= MesCouilles;
    }
}
