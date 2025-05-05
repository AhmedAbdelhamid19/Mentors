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
        public IActionResult ViewDependences(int TopicId)
        {
            Topic topic = topicRepository.GetById(TopicId);
            topicRepository.LoadDependent(topic);
            ViewBag.TopicId = TopicId;
            return View("ViewDependences", topic.Dependents.ToList());
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
        public IActionResult AddDependences(int TopicId)
        {
            List<Topic> topicList = topicRepository.GetAll();
            Topic topic = topicRepository.GetById(TopicId);
            topicList.Remove(topic);

            ViewBag.TopicId = TopicId;
            return View("AddDependences", topicList);
        }
        [HttpGet]
        public IActionResult AddPrerequisites(int TopicId)
        {
            List<Topic> topicList = topicRepository.GetAll();
            Topic topic = topicRepository.GetById(TopicId);
            topicList.Remove(topic);

            ViewBag.TopicId = TopicId;
            return View("AddPrerequisites", topicList);
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
        public IActionResult SaveDependences(int depId, int TopicId)
        {
            if(depId == 0)
            {
				ModelState.AddModelError("DependentId", "Please select a Dependent topic.");
				ViewBag.TopicId = TopicId;

                return View("AddDependences", topicRepository.GetAll());
			}
			topicRepository.AddDependent(depId, TopicId);
			topicRepository.Save();

			return RedirectToAction("ViewDependences", new { TopicId = TopicId });
		}
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



        public IActionResult DeleteTopic(int TopicId)
        {
            topicRepository.Remove(TopicId);
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

			return RedirectToAction("ViewDependences", new { TopicId = TopicId });
		}
	}
}
