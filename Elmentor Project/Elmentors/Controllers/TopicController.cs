using Elmentors.Models;
using Elmentors.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elmentors.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Elmentors.Controllers
{
    // to use Logs Filter 
    //[ServiceFilter(typeof(LogsFilterAttribute))]
    //[TimeOfActionAttribute]
    public class TopicController : Controller
    {
        public ITopicRepository topicRepository {get; set;}

        public TopicController(ITopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
        }




        public IActionResult ViewTopics()
		{
            return View("ViewTopics", topicRepository.GetAll());
        }
        
        
        public IActionResult ViewDependences(int Id)
        {
            Topic topic = topicRepository.GetById(Id);
            topicRepository.LoadDependent(topic);

            ViewBag.TopicId = Id;
            return View("ViewDependences", topic.Dependent);
        }
        public IActionResult ViewPrerequisites(int Id)
        {
            Topic topic = topicRepository.GetById(Id);
            topicRepository.LoadPrerequisites(topic);
            ViewBag.TopicId = Id;
            return View("ViewPrerequisites", topic.Prerequisites);
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
            if(ModelState.IsValid && topic != null)
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
            if(ModelState.IsValid && Dep != null)
            {
                topicRepository.AddDependent(Dep,TopicId);
                topicRepository.Save();
                return RedirectToAction("ViewDependences", new { id = TopicId });
            }
            ViewBag.TopicId = TopicId;
            return View("AddDependences", Dep);
        }
        public IActionResult SavePrerequisites(Topic Pre, int TopicId)
        {
            if(ModelState.IsValid && Pre != null)
            {
                topicRepository.AddPrerequisite(Pre,TopicId);
                topicRepository.Save();
                return RedirectToAction("ViewPrerequisites", new {id = TopicId});
            }
            ViewBag.TopicId = TopicId;
            return View("AddPrerequisites", Pre);
        }



        public IActionResult AddDependencesFromExists(int Id)
        {
            List<Topic> AllTopics = topicRepository.GetAll();
            Topic Topic = topicRepository.GetById(Id);
            AllTopics.Remove(Topic);

            ViewBag.alltopics = AllTopics;
            ViewBag.TopicId = Id;
            return View("AddDependencesFromExists");
        }
        public IActionResult AddPrerequisitesFromExists(int Id)
        {
            List<Topic> AllTopics = topicRepository.GetAll();
            Topic Topic = topicRepository.GetById(Id);
            AllTopics.Remove(Topic);

            ViewBag.alltopics = AllTopics;
            ViewBag.TopicId = Id;
            return View("AddPrerequisitesFromExists");
        }



        public ActionResult SaveDependencesFromExists(int DependentId, int TopicId)
        {
            if (DependentId == 0)
            {
                ModelState.AddModelError("DependentId", "You must select a valid topic.");
                ViewBag.alltopics = topicRepository.GetAll();
                ViewBag.TopicId = TopicId;

                return View("AddDependencesFromExists");
            }

            if (DependentId > 0)
            {
                Topic dependentTopic = topicRepository.GetById(DependentId);
                topicRepository.AddDependent(dependentTopic, TopicId);
                topicRepository.Save();
                return RedirectToAction("ViewDependences", new { id = TopicId });
            }
            return RedirectToAction("AddDependencesFromExists", new { Id = TopicId });
        }
        public ActionResult SavePrerequisitesFromExists(int PrerequisiteId, int TopicId)
        {
            if (PrerequisiteId == 0)
            {
                ModelState.AddModelError("PrerequisiteId", "You must select a valid topic.");
                ViewBag.alltopics = topicRepository.GetAll();
                ViewBag.TopicId = TopicId;

                return View("AddPrerequisitesFromExists");
            }
            if (PrerequisiteId > 0)
            {
                Topic PrerequisiteTopic = topicRepository.GetById(PrerequisiteId);
                topicRepository.AddPrerequisite(PrerequisiteTopic, TopicId);
                topicRepository.Save();
                return RedirectToAction("ViewPrerequisites", new { id = TopicId });
            }
            return RedirectToAction("AddPrerequisitesFromExists", new { id = TopicId });
        }



        public IActionResult EditTopic(int Id)
        {
            Topic curTopic = topicRepository.GetById(Id);
            return View("EditTopic", curTopic);
        }
        public IActionResult SaveEdit(Topic topic)
        {
            Topic curTopic = topicRepository.GetById(topic.Id);
            curTopic.Name = topic.Name;
            curTopic.Description = topic.Description;
            topicRepository.Save();
            return RedirectToAction("ViewTopics");
        }


        public IActionResult DeleteTopic(int Id)
        {
            Topic topic = topicRepository.GetById(Id);
            topicRepository.Remove(topic);
            topicRepository.Save();

            return RedirectToAction("ViewTopics");
        }
        public IActionResult DeletePrerequisite(int TopicId, int PrerequisiteId)
        {
            topicRepository.RemovePrerequisite(TopicId, PrerequisiteId);
            topicRepository.Save();

            return RedirectToAction("ViewPrerequisites", new {id = TopicId});
        }
        public IActionResult DeleteDependence(int TopicId, int DependentId)
        {
            topicRepository.RemoveDependent(TopicId, DependentId);
            topicRepository.Save();

            return RedirectToAction("ViewDependences", new { id = TopicId });
        }


        public IActionResult CheckSenstiveWordsAjax(string Name, string Description)
        {
            Description = Description == null ? "" : Description.ToUpper();
            Name = Name == null ? "" : Name.ToUpper();
            if(Name.Contains("FUCK") || Name.Contains("PITCH") || Description.Contains("FUCK") || Description.Contains("PITCH"))
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
