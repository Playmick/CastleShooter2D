using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabelsData;

[CreateAssetMenu(fileName = "LabelInfo", menuName = "Gameplay/New LabelInfo")]
public class LabelInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private LabelType _labelType;
    [SerializeField] private Language _language;
    [SerializeField] private string _text;

    public string id => this.id;
    public LabelType labelType => this._labelType;
    public Language language => this._language;
    public string text => this._text;
}
