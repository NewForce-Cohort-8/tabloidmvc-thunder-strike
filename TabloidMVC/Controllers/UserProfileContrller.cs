﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
namespace TabloidMVC.Controllers
{

    public class UserProfileController : Controller
    {

        private readonly IUserProfileRepository _userProfileRepo;


        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepo = userProfileRepository;
        }
        // GET: UserProfileController

        public ActionResult Index()
        {
            List<UserProfile> userProfile = _userProfileRepo.GetAllUserProfiles();

            return View(userProfile);
        }

        // GET: UserProfileController/Details/5

        public ActionResult Details(int id)
        {
            UserProfile userProfile = _userProfileRepo.GetUserProfileById(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Edit/5
        public IActionResult Edit(int id)
        {
            UserProfile userProfile = _userProfileRepo.GetUserProfileById(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserProfile updatedProfile)
        {
            try
            {
                _userProfileRepo.UpdateUserProfile(updatedProfile);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(updatedProfile);
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}