using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{

    [Header("Data")]
    public TextMeshProUGUI nameText;
    public Image picture;

    [Header("Details")]
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI email;
    public TextMeshProUGUI age;
    public TextMeshProUGUI gender;
    public TextMeshProUGUI city;
    public Image _picture;
}
