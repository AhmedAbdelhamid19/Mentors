using ElMentors.Models.Topics;

namespace Elmentors.Repository
{
    public interface ITopicRepository
    {
        public void Add(Topic topic);
        public void Add(int id);
        public void AddDependent(Topic Dep, int TopicId);
        public void AddPrerequisite(Topic Pre, int TopicId);
        public void Update(Topic topic);
        public void Remove(int id);
        public void RemovePrerequisite(int TopicId, int PrerequisiteId);
        public void RemoveDependent(int TopicId, int DependentId);
        public List<Topic> GetAll();
        public Topic GetById(int Id);
        public void LoadPrerequisites(Topic topic);
        public void LoadDependent(Topic topic);
        public void Save();
    }
}
