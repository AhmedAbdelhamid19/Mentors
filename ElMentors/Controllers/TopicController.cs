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
        public IActionResult AddPrerequisites(int Id)
        {
            ViewBag.TopicId = Id;
            return View("AddPrerequisites");
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
        public IActionResult SaveDependences(Topic Dep, int TopicId)
        {
            if (ModelState.IsValid && Dep != null)
            {
                topicRepository.AddDependent(Dep, TopicId);
                topicRepository.Save();
                return RedirectToAction("ViewDependences", new { id = TopicId });
            }
            ViewBag.TopicId = TopicId;
            return View("AddDependences", Dep);
        }
        public IActionResult SavePrerequisites(Topic Pre, int TopicId)
        {
            if (ModelState.IsValid && Pre != null)
            {
                topicRepository.AddPrerequisite(Pre, TopicId);
                topicRepository.Save();
                return RedirectToAction("ViewPrerequisites", new { id = TopicId });
            }
            ViewBag.TopicId = TopicId;
            return View("AddPrerequisites", Pre);
        }

    }
}
