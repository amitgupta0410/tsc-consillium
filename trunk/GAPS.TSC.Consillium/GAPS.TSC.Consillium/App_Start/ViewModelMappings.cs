
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
            Mapper.CreateMap<Expert, AddLeadModel>();
            Mapper.CreateMap<AddLeadModel, WorkExperience>();
            Mapper.CreateMap<WorkExperience, WorkExperienceModel>();
            Mapper.CreateMap<WorkExperienceModel, WorkExperience>();

            Mapper.CreateMap<ExpertRequestViewModel, ExpertRequest>();
            Mapper.CreateMap<ExpertRequest, ExpertRequestSingleViewModel>();
            Mapper.CreateMap<Expert, ExpertSingleViewModel>();
            Mapper.CreateMap<Expert, ExpertViewModel>();
            Mapper.CreateMap<ExpertRequest, ExpertRequestViewModel>();
            Mapper.CreateMap<ExpertRequest, UpdateExpertRequest>();
            Mapper.CreateMap<UpdateExpertRequest, ExpertRequest>();
        }
    
    }
}