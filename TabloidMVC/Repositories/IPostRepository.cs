﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        Post GetPostById(int id);
        List<Post> GetUserPosts(int userProfileId);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        void DeletePost(int postId);
        void UpdatePost(Post post);
    }
}