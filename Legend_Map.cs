using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Legend_Map : MonoBehaviour
{
    //public enemyspawn spawner;
    float timer = 0;
    public float speed;
    [SerializeField] GameObject ShinCube;
    int afterMap = 0;
    int lateAfterMap = 0;
    //public GameObject sanginPrefab, boss;
    public AudioSource aud;
    public AudioClip startsound;
    public TMPro.TextMeshPro wave, namtime;
    //public Transform bosswichi;

    float[,] map =    {{ 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5},
                     { 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
                     { 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5},
                     { 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5}
                     };

    float[,] map2 =  {{ 1, 1, 3, 3, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1},
                    { 1, 1, 3, 3, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 3, 3},
                    { 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 3, 3},
                    { 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5},
                    { 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5},
                    { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    { 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1},
                    { 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1},
                    { 3, 3, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1},
                    { 3, 3, 1, 1, 1, 1, 7, 7, 7, 7, 1, 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 5, 5, 3, 3, 1, 1},
                    { 1, 1, 1, 1, 1, 1, 7, 7, 7, 7, 5, 5, 3, 3, 1, 1}
                    };

    float[,] map3 =  {{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
                    { 1, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 1},
                    { 1, 3, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 9, 9, 9, 9, 9, 9, 9, 9, 7, 5, 3, 1},
                    { 1, 3, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 5, 3, 1},
                    { 1, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 1},
                    { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                    };

    float[,] map4 =  {{ 1, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5 },
                    { 5, 1, 1, 5, 5, 5, 1, 5, 5, 5, 5, 1, 1, 5, 5, 5 },
                    { 5, 5, 1, 1, 5, 5, 1, 5, 5, 1, 1, 1, 1, 1, 5, 5 },
                    { 5, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5 },
                    { 5, 1, 5, 5, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5 },
                    { 5, 1, 5, 5, 1, 1, 1, 1, 5, 1, 1, 5, 5, 1, 5, 5 },
                    { 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                    { 5, 1, 5, 5, 1, 5, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5 },
                    { 1, 1, 1, 1, 1, 5, 1, 5, 5, 5, 1, 1, 1, 1, 1, 1 },
                    { 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 1, 1, 5, 5, 5, 5 },
                    { 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 1, 5, 5, 5, 1, 1 },
                    { 1, 1, 1, 5, 5, 5, 1, 5, 1, 5, 1, 5, 1, 1, 1, 1 },
                    { 5, 5, 1, 1, 1, 1, 1, 5, 1, 5, 1, 1, 1, 1, 5, 5 },
                    { 5, 5, 1, 5, 5, 5, 1, 5, 1, 1, 1, 5, 1, 5, 5, 5 },
                    { 1, 1, 1, 5, 5, 5, 1, 5, 1, 1, 1, 1, 1, 5, 5, 5 },
                    { 5, 5, 5, 5, 5, 5, 1, 5, 1, 5, 1, 1, 5, 5, 5, 5 }
                    };

    float[,] map5 =  {{5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                    {5, 5, 5, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 5, 5, 5, 5},
                    {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5}
                    };

    float[,] map6 =  {{7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    {7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    {7, 7, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 7, 7},
                    {7, 7, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 7, 7},
                    {7, 7, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 1, 1, 3, 3, 1, 1, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 1, 1, 3, 3, 1, 1, 3, 3, 3, 7, 7},
                    {7, 7, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 7, 7},
                    {7, 7, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 7, 7},
                    {7, 7, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 7, 7},
                    {7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                    {7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7}
                    };

    float[,] map7 =  {{5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                    {5, 5, 5, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 5, 5, 5, 5},
                    {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5}
                    };

    float[,] map8 =  {{5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 3},
                    {5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                    {5, 5, 5, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3},
                    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 5, 5, 5, 5},
                    {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5},
                    {1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5}
                    };

    float[,] mapStore =  {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                        };

    public GameObject[,] cubes = new GameObject[16, 16];
    public float cnt;
    private GameObject sangIn;
    void Awake()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                //print(i * 16 + j);
                cubes[i, j] = transform.GetChild(i * 16 + j).gameObject;
                Instantiate(ShinCube, transform.GetChild(i * 16 + j));
            }
        }

    }

    public int Stage = 1;
    public bool isStore = false;
    public Transform parent;

    private bool check = true;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetMap();
        }
    }

    private void SetMap()
    {
        StartCoroutine(SetMapCoroutine(Random.Range(1, 6)));
    }

    private IEnumerator SetMapCoroutine(int n)
    {
        float t = 0;
        switch (n)
        {
            case 1://
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        while (true)
                        {
                            
                            cubes[i, j].transform.localScale = new Vector3(cubes[i, j].transform.localScale.x, Mathf.Lerp(cubes[i, j].transform.localScale.y, map[i, j], t), cubes[i, j].transform.localScale.z);
                            yield return null;
                            t += 0.1f;

                            if (t > 1)
                            {
                                t = 0;
                                break;
                            }
                        }
                    }
                }
                break;
            case 2:
                //
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        while (true)
                        {
                            
                            cubes[i, j].transform.localScale = new Vector3(cubes[i, j].transform.localScale.x, Mathf.Lerp(cubes[i, j].transform.localScale.y, map2[i, j], t), cubes[i, j].transform.localScale.z);
                            yield return null;
                            t += 0.1f;

                            if (t > 1)
                            {
                                t = 0;
                                break;
                            }
                        }
                    }
                }
                break;
            case 3:
                //
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        while (true)
                        {
                            
                            cubes[i, j].transform.localScale = new Vector3(cubes[i, j].transform.localScale.x, Mathf.Lerp(cubes[i, j].transform.localScale.y, map3[i, j], t), cubes[i, j].transform.localScale.z);
                            yield return null;
                            t += 0.1f;

                            if (t > 1)
                            {
                                t = 0;
                                break;
                            }
                        }
                    }
                }
                break;
            case 4:
                //
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        while (true)
                        {
                            
                            cubes[i, j].transform.localScale = new Vector3(cubes[i, j].transform.localScale.x, Mathf.Lerp(cubes[i, j].transform.localScale.y, map4[i, j], t), cubes[i, j].transform.localScale.z);
                            yield return null;
                            t += 0.1f;

                            if (t > 1)
                            {
                                t = 0;
                                break;
                            }
                        }
                    }
                }
                break;
            case 5:
                //
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        while (true)
                        {
                            
                            cubes[i, j].transform.localScale = new Vector3(cubes[i, j].transform.localScale.x, Mathf.Lerp(cubes[i, j].transform.localScale.y, map5[i, j], t), cubes[i, j].transform.localScale.z);
                            yield return null;
                            t += 0.1f;

                            if (t > 1)
                            {
                                t = 0;
                                break;
                            }
                        }
                    }
                }
                break;
            default:
                Debug.Log("Out of range");
                break;
        }
    }
    public void SetStoreMap()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, mapStore[i, j], 0.05f), 1);
                //if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)mapStore[i, j]) > 0.01f)
                cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *(cubes[i, j].transform.localScale.y < (float)mapStore[i, j] ? 1 : -1), 1);
            }
        }
    }
    public void Last()
    {
    a:
        afterMap = Random.Range(0, 6);

        if (afterMap == lateAfterMap)
        {
            //print(lateAfterMap);
            goto a;
        }
        aud.PlayOneShot(startsound);
        //print(afterMap);
        Invoke("sp", 3.5f);
        timer = 0;
    }


    public void ChangedMap()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                switch (afterMap)
                {
                    case 0:
                        //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, map[i, j], Time.deltaTime * speed), 1);
                        //if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)map[i, j]) > 0.01f)
                        cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *
                             (cubes[i, j].transform.localScale.y < (float)map[i, j] ? 1 : -1), 1);
                        lateAfterMap = 0;
                        break;
                    case 1:
                        //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, map2[i, j], Time.deltaTime * speed), 1);
                        // if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)map2[i, j]) > 0.01f)
                        cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *
                             (cubes[i, j].transform.localScale.y < (float)map2[i, j] ? 1 : -1), 1);
                        lateAfterMap = 1;
                        break;
                    case 2:
                        //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, map3[i, j], Time.deltaTime * speed), 1);
                        // if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)map3[i, j]) > 0.01f)
                        cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *
                            (cubes[i, j].transform.localScale.y < (float)map3[i, j] ? 1 : -1), 1);
                        lateAfterMap = 2;
                        break;
                    case 3:
                        //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, map4[i, j], Time.deltaTime * speed), 1);
                        // if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)map4[i, j]) >0.01f)
                        cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *
                             (cubes[i, j].transform.localScale.y < (float)map4[i, j] ? 1 : -1), 1);
                        lateAfterMap = 3;
                        break;
                    case 4:
                        //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, map5[i, j], Time.deltaTime * speed), 1);
                        //  if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)map5[i, j]) > 0.01f)
                        cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *
                             (cubes[i, j].transform.localScale.y < (float)map5[i, j] ? 1 : -1), 1);
                        lateAfterMap = 4;
                        break;
                    case 5:
                        //cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(cubes[i, j].transform.localScale.y, map6[i, j], Time.deltaTime * speed), 1);
                        //  if (Mathf.Abs(cubes[i, j].transform.localScale.y - (float)map6[i, j]) > 0.01f)
                        cubes[i, j].transform.localScale = new Vector3(1, cubes[i, j].transform.localScale.y + Time.deltaTime * speed *
                            (cubes[i, j].transform.localScale.y < (float)map6[i, j] ? 1 : -1), 1);
                        lateAfterMap = 5;
                        break;
                }
            }
        }
    }

    public void ShinSecondRound()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(map[i, j], map2[i, j], timer), 1);
            }
        }
    }

    public void ShinThirdRound()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(map2[i, j], map3[i, j], timer), 1);
            }
        }
    }

    public void ShinFourRound()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(map3[i, j], map4[i, j], timer), 1);
            }
        }
    }

    public void ShinFiveRound()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                cubes[i, j].transform.localScale = new Vector3(1, Mathf.Lerp(map4[i, j], map5[i, j], timer), 1);
            }
        }
    }
}