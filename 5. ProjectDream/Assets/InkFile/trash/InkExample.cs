using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExample : MonoBehaviour
{
    private Story _inkStory;
    public TextAsset inkJSONAsset;
    public int answer;
    // Start is called before the first frame update
    void Start()
    {
        _inkStory = new Story(inkJSONAsset.text);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1")) Debug.Log(_inkStory.Continue());
        
        if (_inkStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _inkStory.currentChoices.Count; ++i)
            {
                Choice choice = _inkStory.currentChoices[i];
                Debug.Log("Choice " + (i + 1) + ". " + choice.text);
            }
        }
          //  _inkStory.ChooseChoiceIndex(answer);
    }
}
