﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using ScrumPokerPlanning.Areas.Identity.Pages;
using ScrumPokerPlanning.Context;
using ScrumPokerPlanning.ModelServices;
using System.Collections.Generic;
using System.Globalization;

namespace ScrumPokerPlanning.SessionFeaturesPartial
{
    public class SessionFeaturesModel : PageModel
    {
        private IFeatureService _featureService;
        private readonly ApplicationContext _appContext;
        public SessionFeaturesModel(IFeatureService FeatureService, ApplicationContext appContext)
        {
            _featureService = FeatureService;
            _appContext = appContext;
        }        
        public void OnGet()
        {
        }

        public PartialViewResult OnGetSessionFeatures(int sessionid)
        {
            List<Models.Feature> SessionFeatures = _featureService.GetFeatures(_appContext, sessionid);
#pragma warning disable EF1001 // Internal EF Core API usage.
            if (SessionFeatures.Any())
#pragma warning restore EF1001 // Internal EF Core API usage.
            {
                return Partial("_SessionFeaturesPartial", SessionFeatures);

            }
            else
            {
                return Partial("_SessionFeaturesPartialEmpty", SessionFeatures);

            }
        }
    }

}
