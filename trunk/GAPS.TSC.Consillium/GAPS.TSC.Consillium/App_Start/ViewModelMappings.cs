﻿
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web;
using AutoMapper;
using GAPS.TSC.Consillium.Models;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.App_Start {
    public class ViewModelMappings {

        public static void Init()
        {
            Mapper.CreateMap<AddLeadModel, Expert>();
            Mapper.CreateMap<ExpertRequestViewModel, ExpertRequest>();
            Mapper.CreateMap<ExpertRequestSingleViewModel, ExpertRequest>();
        }
    
    }
}