using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="DataCharacter", menuName = "character")]
public class CharacterScript : ScriptableObject
{
    public CharacterData Character;

    void Start()
    {
    }
}


[System.Serializable]
public class CharacterData
{
    public string Name;
    public GameObject Character;
    public Avatar Avatar;
    public Image iconChar;
    public Animator animatorPlayer;

}