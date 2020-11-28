﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScrumPokerPlanning.Context;
using ScrumPokerPlanning.Models;
using Sketch7.Core;

namespace ScrumPokerPlanning.Areas.Identity.Pages
{
    public class JoinModel : BaseModelDatabaseUser
    {

        public JoinModel(ApplicationContext context, UserManager<ApplicationUser> userManager):base(context, userManager)
        {
        }

        [BindProperty]
        [Display(Name = "Session code*")]
        public string SessionCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {           
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid Model!");
                return Page();
            }

            //todo: evaluate if user CAN JOIN first
            //todo: Check if session EXISTS after all

            int sessionCode = 0;
            if (SessionCode == null)
            {
                SessionCode = "";
            }
            else
            {
                SessionCode = SessionCode.Trim();
            }
            var query = _appContext.PlanningSession.Where(x => x.SessionCode == SessionCode);

            if (query.Any())
            {
                sessionCode = query.FirstOrDefault().Id;
            }

            if (sessionCode <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid Session!");
                return Page();
            }

            PlanningSessionUser planningSessionUser = _appContext.PlanningSessionUser.Where(x => x.UserId == userIdentity().Id && x.PlanningSessionId == sessionCode).FirstOrDefault();

            if (planningSessionUser == null)
            {
                planningSessionUser = new PlanningSessionUser()
                {
                    PlanningSessionId = sessionCode,
                    UserId = userIdentity().Id,
                    UserIsCreator = false
                };

                _appContext.PlanningSessionUser.Add(planningSessionUser);

                await _appContext.SaveChangesAsync();
            }

            //Issues already created for that session
            //Add a featureUser line for each feature existent in that sesison
            var featuresInSession = _appContext.Feature.Where(x => x.SessionId == sessionCode).ToList();

            foreach (var item in featuresInSession)
            {
                var existRelationship = _appContext.FeatureUser.Where(x => x.FeatureId == item.Id && x.UserId == userIdentity().Id).FirstOrDefault();
               
                if (existRelationship == null)
                {
                    if (item.Status == EnumFeature.Open)
                    {
                        var featureUser = new FeatureUser()
                        {   
                            UserId = userIdentity().Id,
                            FeatureId = item.Id                            
                        };

                        _appContext.FeatureUser.Add(featureUser);

                        await _appContext.SaveChangesAsync();
                    }
                }
                    
            }
              



            return RedirectToPage("./Session", new { code = SessionCode });
        }
    }
}
