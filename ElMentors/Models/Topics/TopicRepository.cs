using ElMentors.Models.Topics;
using ElMentors.Models;
using Microsoft.EntityFrameworkCore;

namespace Elmentors.Repository
{
    public class TopicRepository: ITopicRepository
    {
        private Context context { get; set; }
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
            Topic? newTopic = context.Topic
                .Include(t => t.Prerequisites)
                .Include(t => t.Dependents)
                .FirstOrDefault(t => t.Id == topic.Id);

            if(newTopic != null)
            {
                newTopic.Name = topic.Name;
                newTopic.Description = topic.Description;
            }
        }

        public void Remove(int id)
        {
            Topic topic = GetById(id);
            LoadDependent(topic);
            LoadPrerequisites(topic);
            foreach(Topic depTopic in topic.Dependents)
            {
                depTopic.Prerequisites.Remove(topic);
            }
            foreach(Topic preTopic in topic.Prerequisites)
            {
                preTopic.Dependents.Remove(topic);
            }
            context.Remove(topic);
        }
        public void RemovePrerequisite(int TopicId, int PrerequisiteId)
        {
            Topic? topic = GetById(TopicId);
            LoadPrerequisites(topic);
            Topic? Prerequisite = topic.Prerequisites?.FirstOrDefault(x => x.Id == PrerequisiteId);
            if(Prerequisite != null)
                topic?.Prerequisites?.Remove(Prerequisite);
        }
        public void RemoveDependent(int TopicId, int DependentId)
        {
            Topic? topic = GetById(TopicId);
            LoadDependent(topic);
            Topic? Dependent = topic.Dependents?.FirstOrDefault(x => x.Id == DependentId);
            if(Dependent != null)
                topic?.Dependents?.Remove(Dependent);
        }

        public List<Topic> GetAll()
        {
            return context.Topic.ToList();
        }
        public Topic GetById(int Id)
        {
            Topic? topic = context.Topic.FirstOrDefault(t => t.Id == Id);
            return topic;
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
