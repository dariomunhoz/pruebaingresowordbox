using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(fileName = "Database", menuName = "API DATA/Listing Data")]
public class Listing : ScriptableObject
{
    public List<Profile> profile;

    public void AddProfile(Profile _profile)
    {
        profile.Add(_profile);
    }
}
