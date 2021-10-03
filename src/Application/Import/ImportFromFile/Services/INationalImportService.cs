﻿using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Domain.State.Horses;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Import.ImportFromFile.Services
{
    public interface INationalImportService : IService
    {
        public IEnumerable<Horse> ImportForNational(string filePath);
    }
}
