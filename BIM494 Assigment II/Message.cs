using System;
using Android.Graphics;

namespace BIM494_Assigment_II
{
    public class Message
    {
        private string text; // message body
        public Bitmap Image { get; set; }
        private Person person; // data of the user that sent this message
        private bool belongsToCurrentUser; // is this message sent by us?

        public Message(string text,Bitmap image, Person person, bool belongsToCurrentUser)
        {
            this.text = text;
            this.person = person;
            this.Image = image;
            this.belongsToCurrentUser = belongsToCurrentUser;
        }

        public bool isBelongsToCurrentUser()
        {
            return belongsToCurrentUser;
        }

        public static explicit operator Java.Lang.Object(Message v)
        {
            throw new NotImplementedException();
        }

        public Person Person()
        {
            return person;
        }

        public string GetText()
        {
            return text;
        }

        public void SetText(string text)
        {
            this.text = text;
        }
    }


}