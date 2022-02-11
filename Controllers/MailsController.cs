#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using BusinessObjects.Model;
using MailSender.Model;
using AutoMapper;
using MailKit;
using Smtp;

namespace MailSender.Controllers
{

    [ApiController]
    public class MailsController : ControllerBase
    {
        private ApplicationContext _applicationContext;
        private IMapper _mapper;
        private static async Task SendEmailAsync([FromBody] MailDto mailDto)
        {
            EmailService emailService = new EmailService();
            string[] recepients = mailDto.Recepients.Split(' ', ',',';');
            foreach(string recepient in recepients)
            {
                await emailService.SendEmailAsync(recepient,mailDto.Subject,mailDto.Body);
            }
        }
        /// <summary>
        /// public constructor of controller
        /// </summary>
        /// <param name="applicationContext"></param>
        /// <param name="mapper"></param>
        public MailsController(ApplicationContext applicationContext, IMapper mapper)
        {
            _mapper = mapper;
            _applicationContext = applicationContext;
        }
        /// <summary>
        /// Fucntion to send an email
        /// </summary>
        /// <param name="mailDto">mailDataObject object</param>
        /// <returns></returns>
        [Route("api/mails")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MailDto mailDto)
        {
            await SendEmailAsync(mailDto);
            _applicationContext.Mails.Add(_mapper.Map<Mail>(mailDto));
            await _applicationContext.SaveChangesAsync();

            return Ok(mailDto);
        }
        /// <summary>
        /// Returns a list of mails from database
        /// </summary>
        /// <returns></returns>
        [Route("api/mails")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> Get()
        {
            return await _applicationContext.Mails.ToListAsync();
        }
    }
}
