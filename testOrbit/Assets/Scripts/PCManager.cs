using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCManager : MonoBehaviour
{
    GameObject mainCamera;
    GameObject pc;
    public static Planet currentPlanet;
    public static int state = 0; // 0: orbit, 1: move, 2: die
    Vector3 moveVector = Vector3.zero;
    float moveSpeed = 100f;
    float planetAngle = -Mathf.PI;
    float deltaAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        pc = GameObject.Find("pc");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = 1;
                    break;
                }
                mainCamera.transform.position = new Vector3(currentPlanet.transform.position.x, currentPlanet.transform.position.y, -10f);
                deltaAngle = Mathf.Sqrt(currentPlanet.gravityScale / Mathf.Pow(currentPlanet.orbitRadius, 3));
                planetAngle += deltaAngle;
                pc.transform.position = currentPlanet.transform.position + new Vector3(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle), 0f) * currentPlanet.orbitRadius + new Vector3(0, 0, -1f);
                break;

            case 1:
                PlanetManager.canSummon = true;
                moveVector = new Vector3(-Mathf.Sin(planetAngle), Mathf.Cos(planetAngle), 0f);
                pc.transform.position += Time.deltaTime * moveSpeed * currentPlanet.orbitRadius * deltaAngle * moveVector;
                mainCamera.transform.position += Time.deltaTime * moveSpeed * currentPlanet.orbitRadius * deltaAngle * moveVector;

                bool check = true;
                foreach (var item in PlanetManager.planetList)
                {
                    if (item != PCManager.currentPlanet && check)
                    {
                        if (Mathf.Abs(distv2(new Vector2(pc.transform.position.x, pc.transform.position.y), new Vector2(item.centerX, item.centerY)) - item.orbitRadius) <= item.offset)
                        {
                            PCManager.currentPlanet = item;
                            PCManager.state = 0;
                            check = false;
                        }
                    }
                }
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    float distv2(Vector2 v1, Vector2 v2)
    {
        float dist = Mathf.Sqrt(Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2));
        return dist;
    }
}