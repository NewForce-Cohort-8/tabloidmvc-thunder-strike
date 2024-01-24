using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        void AddTag(Tag tag);
        void DeleteTag(int tagId);
        Tag GetTagById(int id);
        void UpdateTag(Tag tag);
    }
}
