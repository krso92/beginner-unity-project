using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawnElements : MonoBehaviour {

    public float leftX;
    public float rightX;
    public float topY;
    public float botY;

    public float alphaTime, alphaValue, betaTime;

    public float minSpawnDeltaTime;
    public float maxSpawnDeltaTime;
    public Image Logo;

    public float[] spawnTimeIncreaseSteps;

    float GetRandomSpawnTime() {
        return Random.Range(minSpawnDeltaTime, maxSpawnDeltaTime);
    }

    Vector2 GetRandomSpawnPosition() {
        float x = Random.Range(leftX, rightX);
        float y = Random.Range(topY, botY);
        return new Vector2(x, y);
    }

    // parentuj objekte na ovaj
    public GameObject collection;
    public bool isRaining = true;

    public Transform[] children;

    // idemo kroz sve 
    int index = 0;
    int index2 = 0;

    int GetIndex() {
        int ret = index;
        index++;
        if (index >= children.Length)
            index = 0;
        return ret;
    }

    int GetIndex2()
    {
        int ret = index2;
        index2++;
        if (index2 >= children.Length)
            index2 = 0;
        return ret;
    }

    // Use this for initialization
    void Start() {
        foreach (Transform t in children) {
            // ovo nes vrv morati
            t.gameObject.SetActive(false);
        }
        StartCoroutine(Work());
        StartCoroutine(IncreaseTime());
        StartCoroutine(DecreaseAlpha(0,alphaTime));
    }

    IEnumerator Work() {
        // ovo znaci cekaj jedan frejm
        yield return null;
        while (isRaining) {
            yield return new WaitForSeconds(GetRandomSpawnTime());
            Vector2 pos = GetRandomSpawnPosition();
            int i = GetIndex();
            children[i].position = pos;
            // tvoja logika

            children[i].gameObject.SetActive(true);
            //			yield return new WaitForSeconds (.2f);
            //			children [i].gameObject.SetActive (false);
        }
    }

    IEnumerator IncreaseTime() {
        float saveMin = minSpawnDeltaTime;
        float saveMax = maxSpawnDeltaTime;
        for (int i = 0; i < spawnTimeIncreaseSteps.Length; i++) {
            // random izmedju 2 ista daje bas taj
            minSpawnDeltaTime = spawnTimeIncreaseSteps[i];
            maxSpawnDeltaTime = spawnTimeIncreaseSteps[i];
            yield return new WaitForSeconds(minSpawnDeltaTime);
        }
        minSpawnDeltaTime = saveMin;
        maxSpawnDeltaTime = saveMax;
    }
    IEnumerator DecreaseAlpha(float aValue, float aTime)
    {
        yield return new WaitForSeconds(8f);
        float alpha = 1f;
        StartCoroutine(IncreaseAlpha(betaTime));
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            int i = GetIndex2();
            SpriteRenderer Spajko = children[i].GetComponent<SpriteRenderer>();
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            Spajko.color = newColor;
            yield return null;
        }
    }

    IEnumerator IncreaseAlpha(float bTime)
    {

        float alpha = 0f;

        for (float b = 0.0f; b < 1.0f; b += Time.deltaTime / bTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1f, b));
            Logo.color = newColor;
            yield return null;
        }
        alpha = 1f;
        yield return new WaitForSeconds(2f);
        for (float b = 0.0f; b < 1.0f; b += Time.deltaTime / 3f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0f, b));
            Logo.color = newColor;
            yield return null;
        }
        Application.LoadLevel("Introduction");
    }
}
