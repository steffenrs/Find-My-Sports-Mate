﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentationLayer
{
    public class ErrorViewModel
    {
        public String errorMessage { get; set; }
        public String innerErrorMessage { get; set; }
    }
}