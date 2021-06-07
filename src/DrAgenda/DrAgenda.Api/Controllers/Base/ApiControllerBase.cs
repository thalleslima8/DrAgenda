using System;
using DrAgenda.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DrAgenda.Api.Controllers.Base
{
    [ApiController]
    public abstract class ApiControllerBase : Controller
    {
        protected ApiControllerBase(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork)
        {
            HostingEnvironment = hostingEnvironment;
            UnitOfWork = unitOfWork;
        }

        protected ApiControllerBase(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork, ILogger<ApiControllerBase> _logger)
        {
            HostingEnvironment = hostingEnvironment;
            UnitOfWork = unitOfWork;
            Logger = _logger;
        }

        protected ApiControllerBase(IWebHostEnvironment hostingEnvironment,
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<ApiControllerBase> logger, 
            IHttpContextAccessor httpContextAccessor)
        {
            HostingEnvironment = hostingEnvironment;
            UnitOfWork = unitOfWork;
            Logger = logger;
            HttpContextAccessor = httpContextAccessor;
        }

        public IWebHostEnvironment HostingEnvironment { get; }

        public IDrAgendaUnitOfWork UnitOfWork { get; }

        public ILogger<ApiControllerBase> Logger { get; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
    }
}