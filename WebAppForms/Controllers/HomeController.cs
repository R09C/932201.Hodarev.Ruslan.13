using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppForms.Models;

namespace WebAppForms.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Step1()
        {
            return View(new PersonalInfo());
        }

        [HttpPost]
        public IActionResult Step1(PersonalInfo model)
        {
            if (ModelState.IsValid)
            {
                // Сохраняем данные в TempData для передачи на следующий шаг
                TempData["PersonalInfo"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("Step2");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Step2()
        {
            var personalInfoJson = TempData["PersonalInfo"] as string;
            if (string.IsNullOrEmpty(personalInfoJson))
            {
                return RedirectToAction("Step1");
            }

            // Десериализуем личную информацию
            var personalInfo = JsonConvert.DeserializeObject<PersonalInfo>(personalInfoJson);

            // Передаем пустую модель JobInfo для заполнения
            TempData.Keep("PersonalInfo"); // Сохраняем личные данные для использования на следующем шаге
            return View(new JobInfo());
        }

        [HttpPost]
        public IActionResult Step2(JobInfo model)
        {
            if (ModelState.IsValid)
            {
                // Сохраняем данные в TempData для передачи на следующий шаг
                TempData["JobInfo"] = JsonConvert.SerializeObject(model);
                TempData.Keep("PersonalInfo"); // Сохраняем личные данные для использования на следующем шаге
                return RedirectToAction("Step3");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Step3()
        {
            var personalInfoJson = TempData["PersonalInfo"] as string;
            var jobInfoJson = TempData["JobInfo"] as string;

            if (string.IsNullOrEmpty(personalInfoJson) || string.IsNullOrEmpty(jobInfoJson))
            {
                return RedirectToAction("Step1");
            }

            // Десериализуем информацию
            var personalInfo = JsonConvert.DeserializeObject<PersonalInfo>(personalInfoJson);
            var jobInfo = JsonConvert.DeserializeObject<JobInfo>(jobInfoJson);

            var finalModel = new FinalModel
            {
                PersonalInfo = personalInfo,
                JobInfo = jobInfo
            };

            TempData.Keep("PersonalInfo"); // Сохраняем данные для следующего шага
            TempData.Keep("JobInfo");

            return View(finalModel);
        }

        [HttpPost]
        public IActionResult Step3(FinalModel model)
        {
            if (ModelState.IsValid)
            {
                // Логика завершения (например, сохранение данных в базе данных)
                return RedirectToAction("Success");
            }
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}