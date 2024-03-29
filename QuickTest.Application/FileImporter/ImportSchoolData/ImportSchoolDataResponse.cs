﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.ImportSchoolData
{
    public class ImportSchoolDataResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public ImportSummaryDto ImportSummary { get; set; }
    }
}
