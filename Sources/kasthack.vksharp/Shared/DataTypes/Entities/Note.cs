﻿using System;
using kasthack.vksharp.DataTypes.Enums;
using kasthack.vksharp.DataTypes.Interfaces;

namespace kasthack.vksharp.DataTypes.Entities {
    public class Note : OwnedEntity {
        //UserId -> OwnerId
        public int UserId { get { return OwnerId; } set { OwnerId = value; } }
        public DateTimeOffset Date { get; set; }
        public int CommentCount { get; set; }
        public int? ReadCommentCount { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public static implicit operator ObjectContentId(Note note) => new ObjectContentId( ContentType.Note, note.Id, note.OwnerId );

        public static implicit operator ContentId( Note note ) => (ObjectContentId) note;

    }
}
