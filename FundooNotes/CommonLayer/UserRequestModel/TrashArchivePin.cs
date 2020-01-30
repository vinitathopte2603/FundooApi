﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooCommonLayer.UserRequestModel
{
     public class TrashArchivePin
    {
        public bool value { get; set; }
    }
    public class ColourRequest
    {
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
        public string color { get; set; }
    }
}
