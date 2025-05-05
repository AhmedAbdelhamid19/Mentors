using Elmentors.Repository;
using ElMentors.Models.Topics;
using Microsoft.AspNetCore.Mvc;

namespace ElMentors.Controllers
{
    public class TopicController : Controller
    {
        public ITopicRepository topicRepository { get; set; }

        public TopicController(ITopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
        }
        public IActionResult ViewTopics()
        {
            return View("ViewTopics", topicRepository.GetAll());
        }
        public IActionResult ViewDependences(int id)
        {
            Topic topic = topicRepository.GetById(id);
            return View("ViewDependences", topic.Dependents);
        }
        public IActionResult ViewPrerequisites(int TopicId)
        {
            Topic topic = topicRepository.GetById(TopicId);
            topicRepository.LoadPrerequisites(topic);
            ViewBag.TopicId = TopicId;
            return View("ViewPrerequisites", topic.Prerequisites.ToList());
        }
        [HttpGet]
        public IActionResult AddTopic()
        {
            return View("AddTopic");
        }
        [HttpGet]
        public IActionResult AddDependences(int Id)
        {
            ViewBag.TopicId = Id;
            return View("AddDependences");
        }
        [HttpGet]
        public IActionResult AddPrerequisites(int TopicId)
        {
            List<Topic> AllTopics = topicRepository.GetAll();
            Topic Topic = topicRepository.GetById(TopicId);
            AllTopics.Remove(Topic);

            ViewBag.TopicId = TopicId;
            return View("AddPrerequisites", AllTopics);
        }
        [HttpPost]
        public IActionResult SaveTopic(Topic topic)
        {
            if (ModelState.IsValid && topic != null)
            {
                topicRepository.Add(topic);
                topicRepository.Save();
                return RedirectToAction("ViewTopics");
            }
            return View("AddTopic", topic);
        }
        [HttpPost]
        //public IActionResult SaveDependences(Topic Dep, int TopicId)
        //{
        //    if (ModelState.IsValid && Dep != null)
        //    {
        //        topicRepository.AddDependent(Dep, TopicId);
        //        topicRepository.Save();
        //        return RedirectToAction("ViewDependences", new { id = TopicId });
        //    }
        //    ViewBag.TopicId = TopicId;
        //    return View("AddDependences", Dep);
        //}
        public IActionResult SavePrerequisites(int preId, int TopicId)
        {
            if(preId == 0)
            {
                ModelState.AddModelError("PrerequisiteId", "Please select a prerequisite topic.");
                ViewBag.TopicId = TopicId;

                return View("AddPrerequisites", topicRepository.GetAll());
            }
            topicRepository.AddPrerequisite(preId, TopicId);
            topicRepository.Save();

            return RedirectToAction("ViewPrerequisites", new { TopicId = TopicId});
        }

        public IActionResult EditTopic(int id)
        {
            Topic topic = topicRepository.GetById(id);
            return View("EditTopic", topic);
        }
        [HttpPost]
        public IActionResult SaveEdit(Topic topic)
        {
            topicRepository.Update(topic);;
            topicRepository.Save();
            return RedirectToAction("ViewTopics");
        }
        public IActionResult DeleteTopic(int id)
        {
            topicRepository.Remove(id);
            topicRepository.Save();

            return RedirectToAction("ViewTopics");
        }
		public IActionResult DeletePrerequisite(int TopicId, int PrerequisiteId)
		{
			topicRepository.RemovePrerequisite(TopicId, PrerequisiteId);
			topicRepository.Save();

			return RedirectToAction("ViewPrerequisites", new { TopicId = TopicId });
		}
		public IActionResult DeleteDependence(int TopicId, int DependentId)
		{
			topicRepository.RemoveDependent(TopicId, DependentId);
			topicRepository.Save();

			return RedirectToAction("ViewDependences", new { id = TopicId });
		}
	}
}
