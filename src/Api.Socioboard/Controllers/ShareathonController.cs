﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Api.Socioboard.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Socioboard.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class ShareathonController : Controller
    {
        public ShareathonController(ILogger<ShareathonController> logger, Microsoft.Extensions.Options.IOptions<Helper.AppSettings> settings, IHostingEnvironment env)
        {
            _logger = logger;
            _appSettings = settings.Value;
            _redisCache = new Helper.Cache(_appSettings.RedisConfiguration);
            _env = env;
        }
        private readonly ILogger _logger;
        private Helper.AppSettings _appSettings;
        private Helper.Cache _redisCache;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// To share the facebook page post with selected facebook profile in a given period of time.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="FacebookPageId">facebookpage id of user facebook profile</param>
        /// <param name="Facebookaccountid">Facebook account id of user</param>
        /// <param name="Timeintervalminutes">Time interval provided by the user for posting</param>
        /// <returns></returns>
        [HttpPost("AddPageShareathon")]
        public IActionResult AddPageShareathon(long userId,string Facebookaccountid, int Timeintervalminutes)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger,_env);
            string FacebookPageId = Request.Form["FacebookPageId"];
            string pagedata = Repositories.ShareathonRepository.AddPageShareathon(userId, FacebookPageId, Facebookaccountid, Timeintervalminutes, _redisCache, _appSettings, dbr);
            return Ok(pagedata);
        }

        [HttpPost("EditPageShareathon")]
        public IActionResult EditPageShareathon(string PageShareathodId,long userId, string Facebookaccountid, int Timeintervalminutes)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _env);
            string FacebookPageId = Request.Form["FacebookPageId"];
            string pagedata = Repositories.ShareathonRepository.EditPageShareathon(PageShareathodId,userId, FacebookPageId, Facebookaccountid, Timeintervalminutes, _redisCache, _appSettings, dbr);
            return Ok(pagedata);
        }

        [HttpPost("DeletePageShareathon")]
        public IActionResult DeletePageShareathon(string PageShareathodId)
        {
            string pagedata = Repositories.ShareathonRepository.DeletePageShareathon(PageShareathodId, _appSettings);
            return Ok(pagedata);
        }

        [HttpGet("UserpageShareathon")]
        public IActionResult UserpageShareathon(long userId)
        {
            List<Domain.Socioboard.Models.Mongo.PageShareathon> lstPageShareathon = Repositories.ShareathonRepository.PageShareathonByUserId(userId, _appSettings, _redisCache);
            return Ok(lstPageShareathon);
        }


        [HttpPost("DeleteGroupShareathon")]
        public IActionResult DeleteGroupShareathon(string GroupShareathodId)
        {
            string pagedata = Repositories.ShareathonRepository.DeleteGroupShareathon(GroupShareathodId, _appSettings);
            return Ok(pagedata);
        }

        [HttpGet("UsergroupShareathon")]
        public IActionResult UsergroupShareathon(long userId)
        {
            List<Domain.Socioboard.Models.Mongo.GroupShareathon> lstPageShareathon = Repositories.ShareathonRepository.GroupShareathonByUserId(userId, _appSettings, _redisCache);
            return Ok(lstPageShareathon);
        }


        /// <summary>
        /// To group post with selected facebook profiles in a given period of time 
        /// </summary>
        /// <param name="userId">id of the user</param>
        /// <param name="FacebookUrl">facebook url of user</param>
        /// <param name="Facebookaccountid">Facebook account id of user</param>
        /// <param name="Timeintervalminutes">Time interval provided by the user</param>
        /// <param name="FacebookGroupId">FacebookGroup id of user facebook profile</param>
        /// <returns></returns>
        [HttpPost("AddGroupShareathon")]
        public IActionResult AddGroupShareathon(long userId,string Facebookaccountid, int Timeintervalminutes)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _env);
            string FacebookGroupId = Request.Form["FacebookGroupId"];
            string FacebookUrl = Request.Form["FacebookUrl"];
            string pagedata = Repositories.ShareathonRepository.AddGroupShareathon(userId, FacebookUrl, FacebookGroupId, Facebookaccountid, Timeintervalminutes, _redisCache, _appSettings, dbr);
            return Ok(pagedata);
        }


        [HttpPost("EditGroupShareathon")]
        public IActionResult EditGroupShareathon(string GroupShareathodId, long userId,string Facebookaccountid, int Timeintervalminutes)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _env);
            string FacebookGroupId = Request.Form["FacebookGroupId"];
            string FacebookUrl = Request.Form["FacebookUrl"];
            string pagedata = Repositories.ShareathonRepository.EditgroupShareathon(GroupShareathodId, userId, FacebookUrl, FacebookGroupId, Facebookaccountid, Timeintervalminutes, _redisCache, _appSettings, dbr);
            return Ok(pagedata);
        }

        
    }
}
