using Android.Graphics;
using System;

public class Person
{
    private int id;
    private string name;
    private Bitmap image;

    public Person(int id, string name, Bitmap image)
    {
        this.id = id;
        this.name = name;
        this.image = image;

    }

    public Person() { }


    public int Id   // property
    {
        get => id;    // get method
        set => id = value;   // set method
    }

    public string Name   // property
    {
        get => name;    // get method
        set => name = value;   // set method
    }


    public Bitmap Image   // property
    {
        get => image;    // get method
        set => image = value;   // set method
    }

    public static explicit operator Java.Lang.Object(Person v)
    {
        throw new NotImplementedException();
    }
}