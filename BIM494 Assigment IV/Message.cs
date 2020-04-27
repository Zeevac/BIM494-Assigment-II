using System;
using Android.Graphics;
using SQLite;


namespace BIM494_Assigment_IV
{
    [Table("messages")]
    public class Message : Java.Lang.Object
    {
        [MaxLength(250), Column("text")]
        public string Text { get; set; }
        [Column("image")]
        public byte[] Image { get; set; }
        [Column("senderID")]
        public int SenderID { get; set; }
        [Column("isCurrentUser")]
        public bool BelongsToCurrentUser { get; set; }

        public Message(string text, byte[] image, int SenderID, bool BelongsToCurrentUser)
        {
            this.Text = text;
            this.SenderID = SenderID;
            this.Image = image;
            this.BelongsToCurrentUser = BelongsToCurrentUser;
        }

        public Message() { }
    }

}