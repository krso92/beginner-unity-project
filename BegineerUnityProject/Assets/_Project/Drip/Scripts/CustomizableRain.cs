using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizableRain : MonoBehaviour {

    // parametri za podesavanje

    public float leftX;
    public float rightX;
    public float topY;
    public float botY;

    // randomness
    public float minSpawnDeltaTime;
    public float maxSpawnDeltaTime;

    [Range(0.04f, .3f)]
    public float spawnDeltaTime;

    // 
    public float[] spawnTimeIncreaseSteps;
    public bool isRaining = true;
    public Transform[] children;

    float GetRandomSpawnTime() {
        return Random.Range(minSpawnDeltaTime, maxSpawnDeltaTime);
    }

    Vector2 GetRandomSpawnPosition() {
        float x = Random.Range(leftX, rightX);
        float y = Random.Range(topY, botY);
        return new Vector2(x, y);
    }

    void Awake(){
        children = new Transform[transform.childCount];
        for(int i = 0; i < children.Length; i++) {
            children[i] = transform.GetChild(i);
            children[i].gameObject.SetActive(false);
        }
    }

    // vreme za pojavljivanje sledece kapi kise
    float nextDropAt = 0f;
    
    int index = -1;
    // kontrola index-a
    int GetIndex() {
        index++;
        // ako smo izasli iz opsega
        if (index >= children.Length)
            // idemo na pocetak
            index = 0;
        return index;
    }

    void Update(){
        if(isRaining){
            float now = Time.time;
            // ako je vreme za sledecu kap
            if(now >= nextDropAt){
                // pokupimo index
                int i = GetIndex();
                // postavimo kap na poziciju
                children[i].position = GetRandomSpawnPosition();
                // aktiviramo gameObjekat
                children[i].gameObject.SetActive(true);
                // apdejtujemo vreme kada treba da padne sledeca kap
                nextDropAt = now + spawnDeltaTime;
                //nextDropAt = now + GetRandomSpawnTime();
            }
        }
    }
}
