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
                for(int i = 1; i <= 100; i++)
        {
            GameObject eb = GameObject.Find("eb (" + i.ToString() + ")");
            eb.transform.localPosition = new Vector3(0.2f * i + 5f, 0f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
           if (CharacterMove.energy <= 1)
            state = 2;
        else
        {
            for (int i = 1; i <= CharacterMove.energy; i++)
            {
                GameObject eb = GameObject.Find("eb (" + i.ToString() + ")");
                Color clr = eb.GetComponent<SpriteRenderer>().color;
                clr.a = 1f;
                eb.GetComponent<SpriteRenderer>().color = clr;
            }
            for (int i = (int)CharacterMove.energy; i <= 100; i++)
            {
                GameObject eb = GameObject.Find("eb (" + i.ToString() + ")");
                Color clr = eb.GetComponent<SpriteRenderer>().color;
                clr.a = 0f;
                eb.GetComponent<SpriteRenderer>().color = clr;
            }
        }
        pc.transform.Rotate(Vector3.forward * 150f * Time.deltaTime);
        for(int i = 1; i <= 25; i++)
        {
            int m = (int)(pc.transform.position.x / 64);
            int n = (int)(pc.transform.position.y / 36);
            GameObject bg = GameObject.Find("background (" + i.ToString() + ")");
            bg.transform.position = new Vector3(64 * (m + (i - 1) % 5 - 2), 36 * (n + (i - 1) / 5 - 2), 1f);
        }

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
                CharacterMove.energy -= 0.1f * currentPlanet.orbitRadius * deltaAngle;
                CharacterMove.score += 10f * currentPlanet.orbitRadius * deltaAngle;
                break;

            case 1:
                PlanetManager.canSummon = true;
                moveVector = new Vector3(-Mathf.Sin(planetAngle), Mathf.Cos(planetAngle), 0f);
                 float speedWeight = 1f;
                pc.transform.position += Time.deltaTime * moveSpeed * currentPlanet.orbitRadius * deltaAngle * moveVector * speedWeight;
                mainCamera.transform.position += Time.deltaTime * moveSpeed * currentPlanet.orbitRadius * deltaAngle * moveVector * speedWeight;

                CharacterMove.energy -= 0.1f * Time.deltaTime * moveSpeed * currentPlanet.orbitRadius * deltaAngle * Vector3.Magnitude(moveVector) * speedWeight;
                CharacterMove.score += 10f * Time.deltaTime * moveSpeed * currentPlanet.orbitRadius * deltaAngle * Vector3.Magnitude(moveVector) * speedWeight;
                speedWeight += Time.deltaTime * 0.3f;

                bool check = true;
                foreach (var item in PlanetManager.planetList)
                {
                    if (item != PCManager.currentPlanet && check)
                    {
                        if (Mathf.Abs(distv2(new Vector2(pc.transform.position.x, pc.transform.position.y), new Vector2(item.centerX, item.centerY)) - item.orbitRadius) <= item.offset)
                        {
                            PCManager.currentPlanet = item;
                                                        Vector2 inputVector = new Vector2(pc.transform.position.x, pc.transform.position.y) - new Vector2(PCManager.currentPlanet.centerX, PCManager.currentPlanet.centerY);
                            planetAngle = Mathf.Atan2(inputVector.y, inputVector.x);
                            if(currentPlanet.planet_type == 0)
                            {
                                PCManager.state = 0;
                                CharacterMove.energy += PCManager.currentPlanet.planetSize * 40f;
                                check = false;
                            }
                            else
                                PCManager.state = 2;
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