﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Comment
    {
        public Comment()
        {

        }

        public Comment(string message, DateTime createTime, User author)
        {
            this.Message = message;
            this.CreateTime = createTime;
            this.Author = author;
        }
        public long CommentId { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
        public User Author { get; set; }
    }
}
