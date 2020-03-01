using System;
using Java.Lang;

public class Person
{

    private string name;
    private string message;
    private int imageId;

    public Person(string name, string message, int imageId)
    {
        this.name = name;
        this.message = message;
        this.imageId = imageId;

    }

    public string Name   // property
    {
        get { return name; }   // get method
        set { name = value; }  // set method
    }

    public string Message   // property
    {
        get { return message; }   // get method
        set { message = value; }  // set method
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