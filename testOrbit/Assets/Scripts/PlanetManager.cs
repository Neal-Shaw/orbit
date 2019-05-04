using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject Planet;
    private GameObject newPlanet;
    private Planet thisPlanet;
    public static bool canSummon = true;
    private GameObject pc;
    public static List<Planet> planetList = new List<Planet>();
    int i = 2;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("pc");
        firstPlanet();
        pc.transform.position = new Vector3(thisPlanet.centerX - thisPlanet.orbitRadius, thisPlanet.centerY, -1f);
        PCManager.currentPlanet = thisPlanet;
        summonPlanet();
    }

    // Update is called once per frame
    void Update()
    {
        if(PCManager.state == 0)
            summonPlanet();
    }

    void firstPlanet()
    {
        newPlanet = Instantiate(Planet, transform.position, transform.rotation) as GameObject;
        newPlanet.transform.name = "Planet_1";
        thisPlanet = newPlanet.GetComponent<Planet>();
        thisPlanet.firstPlanet();
        newPlanet.transform.localScale = new Vector3(thisPlanet.planetSize, thisPlanet.planetSize, 1f);
        newPlanet.transform.position = new Vector3(thisPlanet.centerX, thisPlanet.centerY, 0f);
        newPlanet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("planet_image/earth");
        newPlanet.transform.localScale = 2 * new Vector2(thisPlanet.planetSize, thisPlanet.planetSize);
        planetList.Add(thisPlanet);
    }

    void summonPlanet()
    {
        if (canSummon)
        {
            newPlanet = Instantiate(Planet, transform.position, transform.rotation) as GameObject;
            newPlanet.transform.name = "Planet_" + i++;

            thisPlanet = newPlanet.GetComponent<Planet>();
            if (thisPlanet.planetInitialize() == -1)
            {
                Destroy(newPlanet);
                planetList.Remove(thisPlanet);
                return;
            }
            if (newPlanet != null)
            {
                newPlanet.transform.localScale = new Vector3(thisPlanet.planetSize, thisPlanet.planetSize, 1f);
                newPlanet.transform.position = new Vector3(thisPlanet.centerX, thisPlanet.centerY, 0f);
                newPlanet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("planet_image/" + thisPlanet.imageName);
                newPlanet.transform.localScale = 2 * new Vector2(thisPlanet.planetSize, thisPlanet.planetSize);
                planetList.Add(thisPlanet);
            }
        }
    }
}
