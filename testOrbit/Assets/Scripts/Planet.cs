using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float planetSize;
    public float gravityScale;
    // m*v^2/r = GMm/r^2 -> v = sqrt(GM/r) -> GM = gravityScale
    // mrw^2 = GMm/r^2 -> w^2 = GM/r^3 -> sqrt(gravityScale/r^3)
    public float orbitRadius;
    public float offset;
    public float centerX, centerY;
    public string imageName;
    private GameObject pc;

    /*
    public Planet(float _planetSize, float _gravityScale, float _orbitRadius, float _offset, float _centerX, float _centerY, string _imageName)
    {
        planetSize = _planetSize;
        gravityScale = _gravityScale;
        orbitRadius = _orbitRadius;
        offset = _offset;
        centerX = _centerX;
        centerY = _centerY;
        imageName = _imageName;
    }
    */

    public void planetInitialize()
    {
        planetSize = Random.Range(0.1f, 0.3f);
        gravityScale = 600f * Mathf.Pow(planetSize, 4.3f);
        orbitRadius = 21.5f * planetSize;
        offset = 10.25f * 0.05f; // r/2 * scale
        centerX = -12f;// Random.Range(0f, 6f);
        centerY = 0f;// Random.Range(-3f, 3f); 20
        imageName = "tempPlanet";
    }

    public void firstPlanet()
    {
        planetSize = Random.Range(0.1f, 0.3f);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (this != PCManager.currentPlanet)
        {
            if (Mathf.Abs(distv2(pc, new Vector2(this.centerX, this.centerY)) - this.orbitRadius) <= offset)
            {
                PCManager.currentPlanet = this;
                PCManager.state = 0;
            }
        }
    }

    float distv2(GameObject _pc, Vector2 _planet)
    {
        float dist = Mathf.Sqrt(Mathf.Pow(_pc.transform.position.x - _planet.x, 2) + Mathf.Pow(_pc.transform.position.y - _planet.y, 2));
        return dist;
    }
}
