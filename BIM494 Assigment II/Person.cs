using Android.Graphics;
using Java.IO;
using Newtonsoft.Json;
using System;

public class Person : Java.Lang.Object
{
    public int id;
    public string name;
    public Bitmap image;

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

}