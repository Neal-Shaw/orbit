using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public enum planet_image{
        apple, ball8, ball13, donut, blue_peach, burger, 
        mars, mercury, moon, peach, pig, earth
    }

    public float planetSize;
    public float gravityScale;
    // m*v^2/r = GMm/r^2 -> v = sqrt(GM/r) -> GM = gravityScale
    // mrw^2 = GMm/r^2 -> w^2 = GM/r^3 -> sqrt(gravityScale/r^3)
    public float orbitRadius;
    public float offset;
    public float centerX, centerY;
    public string imageName;
    bool canMake = true;
    private GameObject pc;

    Camera cam = null;
    float camHeight, camWidth;

    public int planetInitialize()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        planetSize = Random.Range(0.15f, 0.35f);
        gravityScale = 600f * Mathf.Pow(planetSize, 4.3f);
        orbitRadius = 21.5f * planetSize;
        offset = 10.25f * 0.05f; // r/2 * scale
        int i = 0;
        while(i++ < 2000)
        {
            canMake = true;
            centerX = Random.Range(cam.transform.position.x - (camWidth + orbitRadius / Mathf.Sqrt(2)), cam.transform.position.x + (camWidth + orbitRadius / Mathf.Sqrt(2)));
            centerY = Random.Range(cam.transform.position.y - (camHeight + orbitRadius / Mathf.Sqrt(2)), cam.transform.position.y + (camHeight + orbitRadius / Mathf.Sqrt(2)));
            foreach(var item in PlanetManager.planetList)
            {
                if(distv2(new Vector2(item.transform.position.x, item.transform.position.y), new Vector2(centerX, centerY)) < 1.6f * (item.orbitRadius + item.offset + orbitRadius + offset))
                    canMake = false;
            }
            if (canMake)
                break;
        }
        if (i >= 2000)
        {
            PlanetManager.canSummon = false;
            return -1;
        }

        imageName = ((planet_image)Random.Range(0,10)).ToString("F");
        return 0;
    }

    public void firstPlanet()
    {
        planetSize = Random.Range(0.15f, 0.35f);
        gravityScale = 600f * Mathf.Pow(planetSize, 4.3f);
        orbitRadius = 21.5f * planetSize;
        offset = 10.25f * 0.05f; // r/2 * scale
        centerX = -35f;
        centerY = 0f;
        imageName = "tempPlanet";
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("pc");
        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        if (Mathf.Abs(centerX - cam.transform.position.x) > camWidth + orbitRadius / Mathf.Sqrt(2) && Mathf.Abs(centerY - cam.transform.position.y) > camHeight + orbitRadius / Mathf.Sqrt(2))
        {
            Planet removePlanet = null;
            GameObject removeObject = null;
            foreach (var item in PlanetManager.planetList)
            {
                if (item.centerX == centerX && item.centerY == centerY)
                {
                    removePlanet = item;
                    removeObject = GameObject.Find(item.name);
                }
            }
            if (removePlanet.name != "tempPlanet")
            {
                PlanetManager.planetList.Remove(removePlanet);
                Destroy(removeObject);
            }
        }
    }

    float distv2(Vector2 v1, Vector2 v2)
    {
        float dist = Mathf.Sqrt(Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2));
        return dist;
    }
}
