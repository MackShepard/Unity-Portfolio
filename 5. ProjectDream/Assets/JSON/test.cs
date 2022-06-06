
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class test : MonoBehaviour
{

    
    public string itemLngData; //json база,
    public SO_GameSettings gameSettings; // Настройки игры
    public Root myDeserializedClass; 

    private void Start()
    {
        itemLngData = File.ReadAllText(Application.dataPath + "/JSON/itemReactionRu.json");
        myDeserializedClass = JsonUtility.FromJson<Root>(itemLngData);
    }


    [System.Serializable]
    public class ItemReaction
    {
        public string itemName;
        public string[] watchReaction;
    }

    [System.Serializable]
    public class Root
    {
        public ItemReaction[] itemReaction;
    }




}
