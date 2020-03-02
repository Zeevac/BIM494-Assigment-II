using System;
using Java.Lang;

public class Person
{
    private int id;
    private string name;
    private int imageId;

    public Person(int id, string name, int imageId)
    {
        this.id = id;
        this.name = name;
        this.imageId = imageId;

    }


    public int Id   // property
    {
        get { return id; }   // get method
        set { id = value; }  // set method
    }

    public string Name   // property
    {
        get { return name; }   // get method
        set { name = value; }  // set method
    }


    public int ImageId   // property
    {
        get { return imageId; }   // get method
        set { imageId = value; }  // set method
    }

    public static explicit operator Java.Lang.Object(Person v)
    {
        throw new NotImplementedException();
    }
}