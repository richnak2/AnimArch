using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Visualization.Animation;

namespace Visualization.UI
{
    public class MethodParameterManager : MonoBehaviour
    {
        [SerializeField] public GameObject ParameterName;
        [SerializeField] public GameObject ParameterType;
        [SerializeField] public GameObject ParameterValue;
        [SerializeField] public GameObject ParameterValueText;
        [SerializeField] public GameObject ErrorLabel;
        [SerializeField] public GameObject WarningLabel;
    }
}