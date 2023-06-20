using UnityEngine;

[System.Serializable]
public class Profile
{
    public string name;
    public string email;
    public string gender;
    public string city;
    public string age;
    public Sprite picture;

    
    public Profile(string name, string email, string gender, string city, string age, Sprite picture)
    {
        this.name = name;
        this.email = email;
        this.gender = gender;
        this.city = city;
        this.age = age;
        this.picture = picture;
    }

    public string Name { get { return name; } }
    public string Email { get { return email; } }
    public string Gender { get {  return gender; } }    
    public string City { get { return city; } }
    public string Age { get { return age;} }
    public Sprite Picture { get { return picture; } }

    
}
