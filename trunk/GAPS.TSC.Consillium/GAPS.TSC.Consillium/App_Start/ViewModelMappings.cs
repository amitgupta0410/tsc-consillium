
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

        public static void Init() {
            Mapper.CreateMap<AddLeadModel, Expert>();
            Mapper.CreateMap<Expert, AddLeadModel>();
            Mapper.CreateMap<AddLeadModel, WorkExperience>();
            Mapper.CreateMap<WorkExperience, WorkExperienceModel>();
            Mapper.CreateMap<WorkExperienceModel, WorkExperience>();
            Mapper.CreateMap<ExpertRequestViewModel, ExpertRequest>();
            Mapper.CreateMap<ExpertRequest, ExpertRequestSingleViewModel>()
                .ForMember(x => x.AssignedToName, v => v.MapFrom(u => u.AssignedToId.HasValue ? u.AssignedTo.Name : string.Empty));
            Mapper.CreateMap<Expert, ExpertSingleViewModel>();
            Mapper.CreateMap<Expert, ExpertViewModel>();
            Mapper.CreateMap<ExpertRequest, ExpertRequestViewModel>();
            Mapper.CreateMap<ExpertRequest, UpdateExpertRequest>();
            Mapper.CreateMap<UpdateExpertRequest, ExpertRequest>();
            Mapper.CreateMap<CallsViewModel, Call>();
            Mapper.CreateMap<Expert, ProfileViewModel>();
            Mapper.CreateMap<ExpertNote, ExpertNoteModel>().ForMember(x => x.TeamMember, v => v.MapFrom(a => a.TeamMember.Name));
            Mapper.CreateMap<Call, CallsExpertViewModel>()
                .ForMember(x => x.CallFacilitatedBy, v => v.MapFrom(u => u.CallFacilitatedBy.Name))
                .ForMember(x => x.PaymentMode, v => v.MapFrom(u => u.PaymentMode.Name));
            Mapper.CreateMap<Call, ExpertCallsModel>()
                .ForMember(x => x.CallFacilitatedBy, v => v.MapFrom(u => u.CallFacilitatedBy.Name))
                .ForMember(x => x.PaymentMode, v => v.MapFrom(u => u.PaymentMode.Name));

            Mapper.CreateMap<Call, CallSingleViewModel>()
                .ForMember(x => x.Expert, v => v.MapFrom(a => a.Expert.Name))
                .ForMember(x => x.ExpertRequest, v => v.MapFrom(a => a.ExpertRequest.ProjectName))
                .ForMember(x => x.CallFacilitatedBy, v => v.MapFrom(a => a.CallFacilitatedBy.Name))
                .ForMember(x=>x.ProjectId,v=>v.MapFrom(a=>a.ExpertRequest.ProjectId))
                .ForMember(x=>x.PaymentMode,u=>u.MapFrom(x=>x.PaymentMode.Name));
        }

    }
}