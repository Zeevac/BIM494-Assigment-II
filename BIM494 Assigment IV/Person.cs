using System.Collections.Generic;
using Android.Graphics;
using SQLite;

[Table("people")]
public class Person : Java.Lang.Object
{
    [PrimaryKey, Column("_id")]
    public int Id { get; set; }
    [MaxLength(250), Column("name")]
    public string Name { get; set; }
    [Column("image")]
    public byte[] Image { get; set; }

    public Person(int Id, string Name, byte[] Image)
    {
        this.Id = Id;
        this.Name = Name;
        this.Image = Image;

    }

    public Person() { }

    public static Person GetPerson(List<Person> people, string name)
    {
        foreach(var person in people)
        {
            if (name.Equals(person.Name)){
                return person;
            }
        }
        return null;
    }

}