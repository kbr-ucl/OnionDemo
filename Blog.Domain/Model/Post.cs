﻿using System;

namespace Blog.Domain.Model
{
    public class Post
    {
        /// <summary>
        ///     Used only by Entity framework
        /// </summary>
        public Post()
        {
        }

        public Post(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}