using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject Planet;
    private Planet thisPlanet;
    private GameObject pc;
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
    }

    void firstPlanet()
    {
        Planet = Instantiate(Planet, transform.position, transform.rotation) as GameObject;
        thisPlanet = Planet.GetComponent<Planet>();
        thisPlanet.firstPlanet();
        Planet.transform.localScale = new Vector3(thisPlanet.planetSize, thisPlanet.planetSize, 1f);
        Planet.transform.position = new Vector3(thisPlanet.centerX, thisPlanet.centerY, 0f);
    }

    void summonPlanet()
    {
        Planet = Instantiate(Planet, transform.position, transform.rotation) as GameObject;
        thisPlanet = Planet.GetComponent<Planet>();
        thisPlanet.planetInitialize();
        Planet.transform.localScale = new Vector3(thisPlanet.planetSize, thisPlanet.planetSize, 1f);
        Planet.transform.position = new Vector3(thisPlanet.centerX, thisPlanet.centerY, 0f);
    }
}
