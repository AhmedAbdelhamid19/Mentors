using ElMentors.Models.Topics;
using ElMentors.Models;

namespace Elmentors.Repository
{
    public class TopicRepository: ITopicRepository
    {
        public Context context { get; set; }
        public TopicRepository(Context context)
        {
            this.context = context;
        }
        public void Add(Topic topic)
        {
            context.Add(topic);
        }
        public void Add(int id)
        {
            Topic topic = GetById(id);
            context.Add(topic);
        }
        public void AddDependent(Topic Dep, int TopicId)
        {
            Topic topic = GetById(TopicId);
            LoadDependent(topic);

            topic.Dependents.Add(Dep);
        }
        public void AddPrerequisite(Topic Pre, int TopicId)
        {
            Topic topic = GetById(TopicId);
            LoadPrerequisites(topic);

            topic.Prerequisites.Add(Pre);
        }
        public void Update(Topic topic)
        {
            context.Update(topic);
        }
        public void Update(int id)
        {
            Topic topic = GetById(id);
            context.Update(topic);
        }
        public void Remove(Topic topic)
        {
            List<Topic> AllTopics = GetAll();
            foreach(Topic curTopic in AllTopics)
            {
                LoadDependent(curTopic);
                LoadPrerequisites(curTopic);
                if(curTopic.Dependents.Contains(topic))
                {
                    curTopic.Dependents.Remove(topic);
                }
                if(curTopic.Prerequisites.Contains(topic))
                {
                    curTopic.Prerequisites.Remove(topic);
                }
            }
            context.Remove(topic);
        }
        public void Remove(int id)
        {
            Topic topic = GetById(id);
            context.Remove(topic);
        }
        public void RemovePrerequisite(int TopicId, int PrerequisiteId)
        {
            Topic topic = GetById(TopicId);
            LoadPrerequisites(topic);
            Topic? Prerequisite = topic.Prerequisites?.FirstOrDefault(x => x.Id == PrerequisiteId);
            topic.Prerequisites.Remove(Prerequisite);
        }
        public void RemoveDependent(int TopicId, int DependentId)
        {
            Topic topic = GetById(TopicId);
            LoadDependent(topic);
            Topic? Dependent = topic.Dependents?.FirstOrDefault(x => x.Id == DependentId);
            topic.Dependents.Remove(Dependent);
        }
        public List<Topic> GetAll()
        {
            return context.Topic.ToList();
        }
        public Topic GetById(int Id)
        {
            return context.Topic.FirstOrDefault(t => t.Id == Id);
        }
        public void LoadPrerequisites(Topic topic)
        {
            if (topic != null)
            {
                context.Entry(topic).Collection(t => t.Prerequisites).Load();
            }
        }
        public void LoadDependent(Topic topic)
        {
            if (topic != null)
            {
                context.Entry(topic).Collection(t => t.Dependents).Load();
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
