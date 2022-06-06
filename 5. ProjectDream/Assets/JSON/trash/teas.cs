
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class teas : MonoBehaviour
{
    public ItemList mySampleFile = new();
    // Start is called before the first frame update
    void Start()
    {
        // read file as text
        string jsonStr = File.ReadAllText(Application.dataPath + "/JSON/test1.json"); // using System;
         mySampleFile = JsonUtility.FromJson<ItemList>(jsonStr);
        
        foreach (Item data in mySampleFile.item)
        {
            
        }
        print(mySampleFile.item[1].name);
    }

    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        public string watchReaction;

    }

    [System.Serializable]
    public class ItemList
    {
        public Item[] item;

    }


}
