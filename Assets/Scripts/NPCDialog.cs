using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialog", order = 1)]
public class NPCDialog : ScriptableObject
{
    [TextArea(10,14)] [SerializeField] string[] dialogs;

    public string[] GetDialog
    {
        get
        {
            return dialogs;
        }
    }
}
