using ElMentors.Models.Topics;
using ElMentors.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
        public void AddDependent(int depId, int TopicId)
        {
            Topic depTopic = GetById(depId);
            Topic topic = GetById(TopicId);
            LoadDependent(topic);
            LoadPrerequisites(depTopic);
            topic.Dependents.Add(depTopic);
            depTopic.Prerequisites.Add(topic);
		}
        public void AddPrerequisite(int preId, int TopicId)
        {
            Topic preTopic = GetById(preId);
            Topic topic = GetById(TopicId);

            LoadPrerequisites(topic);
            LoadDependent(preTopic);

            topic.Prerequisites.Add(preTopic);
            preTopic.Dependents.Add(topic);
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
            foreach (Topic depTopic in topic.Dependents)
            {
                LoadPrerequisites(depTopic);
                depTopic.Prerequisites.Remove(topic);
            }
            foreach(Topic preTopic in topic.Prerequisites)
            {
                LoadDependent(preTopic);
                preTopic.Dependents.Remove(topic);
            }
            context.Remove(topic);
        }
        public void RemovePrerequisite(int TopicId, int PrerequisiteId)
        {
			Topic? topic = GetById(TopicId);
            Topic? preTopic = GetById(PrerequisiteId);
			if (topic != null && preTopic != null) { 
			    LoadPrerequisites(topic);
                LoadDependent(preTopic);

                topic?.Prerequisites?.Remove(preTopic);
                preTopic?.Dependents?.Remove(topic);
            }
		}

        public void RemoveDependent(int TopicId, int DependentId)
        {
			Topic? topic = GetById(TopicId);
            Topic? depTopic = GetById(DependentId);
            if(topic != null && depTopic != null)
            {
			    LoadDependent(topic);
                LoadPrerequisites(depTopic);

                topic?.Dependents?.Remove(depTopic);
                depTopic?.Prerequisites?.Remove(topic);
			}
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
