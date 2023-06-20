using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using System;
using UnityEngine.Profiling;

public class APIConnection : MonoBehaviour
{
    public Listing profileList;
    public List<Profile> profiles = new List<Profile>();
    public GameObject prefab;
    public GameObject parent;
    void Start()
    {
        StartCoroutine(ConnectToApi("https://randomuser.me/api/?results=20"));
    }

    IEnumerator ConnectToApi(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error connecting to API: " + webRequest.error);
                yield break;
            }

            string jsonString = webRequest.downloadHandler.text;
            var jsonResult = JsonConvert.DeserializeObject<Root>(jsonString);

            foreach (var result in jsonResult.results)
            {
                yield return StartCoroutine(LoadSpriteFromUrl(result.picture.large, (sprite) =>
                {
                    if (sprite != null)
                    {
                        Profile profile = new Profile(
                            result.name.first + " " + result.name.last,
                            result.email,
                            result.dob.age.ToString(),
                            result.location.city,
                            result.gender,
                            sprite
                        );

                        profileList.AddProfile(profile);
                    }
                    else
                    {
                        Debug.LogError("Failed to load sprite for profile: " + result.name.first + " " + result.name.last);
                    }
                }));
            }

            foreach (Profile profile in profileList.profile)
            {
                CreateProfilePrefab(profile);
            }
        }
    }

    IEnumerator LoadSpriteFromUrl(string url, Action<Sprite> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading sprite from URL: " + webRequest.error);
                callback?.Invoke(null);
                yield break;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

            if (texture == null)
            {
                Debug.LogError("Error loading sprite from URL: Texture is null");
                callback?.Invoke(null);
                yield break;
            }

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            callback?.Invoke(sprite);
        }
    }


    void CreateProfilePrefab(Profile profile)
    {
        // Instantiate a new prefab
        GameObject prefabInstance = Instantiate(prefab, parent.transform);

        // Get the ProfileManager component from the prefab instance
        ProfileManager prefabProfileManager = prefabInstance.GetComponent<ProfileManager>();

        // Set the text fields in the prefab's ProfileManager
        prefabProfileManager.nameText.text = profile.Name;


        // Set the picture sprite in the prefab's ProfileManager
        prefabProfileManager.picture.sprite = profile.Picture;
    }
}



public class Coordinates
{
    public string latitude { get; set; }
    public string longitude { get; set; }
}

public class Dob
{
    public DateTime date { get; set; }
    public int age { get; set; }
}

public class Id
{
    public string name { get; set; }
    public string value { get; set; }
}

public class Info
{
    public string seed { get; set; }
    public int results { get; set; }
    public int page { get; set; }
    public string version { get; set; }
}

public class Location
{
    public Street street { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public object postcode { get; set; }
    public Coordinates coordinates { get; set; }
    public Timezone timezone { get; set; }
}

public class Login
{
    public string uuid { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string salt { get; set; }
    public string md5 { get; set; }
    public string sha1 { get; set; }
    public string sha256 { get; set; }
}

public class Name
{
    public string title { get; set; }
    public string first { get; set; }
    public string last { get; set; }
}

public class Picture
{
    public string large { get; set; }
    public string medium { get; set; }
    public string thumbnail { get; set; }
}

public class Registered
{
    public DateTime date { get; set; }
    public int age { get; set; }
}

public class Result
{
    public string gender { get; set; }
    public Name name { get; set; }
    public Location location { get; set; }
    public string email { get; set; }
    public Login login { get; set; }
    public Dob dob { get; set; }
    public Registered registered { get; set; }
    public string phone { get; set; }
    public string cell { get; set; }
    public Id id { get; set; }
    public Picture picture { get; set; }
    public string nat { get; set; }
}

public class Root
{
    public List<Result> results { get; set; }
    public Info info { get; set; }
}

public class Street
{
    public int number { get; set; }
    public string name { get; set; }
}

public class Timezone
{
    public string offset { get; set; }
    public string description { get; set; }
}


