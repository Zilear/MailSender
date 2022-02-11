using AutoMapper;
using BusinessObjects.Model;
using MailSender.Model;

namespace MailSender.Mapper
{
    public class MailSenderAutoMapper : Profile
    {
        /// <summary>
        /// AutoMapper constructor with no parameters 
        /// </summary>
        public MailSenderAutoMapper()
        {
            CreateMap<MailDto, Mail>();
            CreateMap<Mail, MailDto>();
        }
    }
}
