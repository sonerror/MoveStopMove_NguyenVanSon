using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotNamePool : MonoBehaviour
{
    [SerializeField] private GameObject botNamePrefab;
    [SerializeField] private Canvas canvas;
    private List<GameObject> poolLists = new List<GameObject>();
    public List<string> nameList = new List<string>();

    public static BotNamePool instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < LevelManager.instance.alive; i++)
        {
            GameObject obj = Instantiate(botNamePrefab);
            obj.transform.SetParent(canvas.gameObject.transform);
            obj.SetActive(false);
            poolLists.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in poolLists)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = Instantiate(botNamePrefab);
        newObj.transform.SetParent(canvas.gameObject.transform);
        poolLists.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
