﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace PresentationLayer.ViewModels
{
    public class RegisterViewModel
    {
        public Domain.RegisterModel RegisterModel { get; set; }
    }
}