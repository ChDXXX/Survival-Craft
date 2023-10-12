using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Setting", menuName = "Scriptables/Lighting Setting", order = 1)]
public class LightSetting : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient fogColor;
}