using System;
using System.Collections.Generic;
using System.Linq;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services {
    public interface IExpertService : IGenericService<Expert> {
        bool LeadNameExist(string name);
        bool EmailExist(string email);
        bool AddExperience(int expertId, WorkExperience workExperience);
        IEnumerable<WorkExperience> GetWorkExperiences(int expertId);
        int? DeleteWorkExperience(int id);
        int? EditWorkExperience(WorkExperience work);
        WorkExperience GetWorkExperienceById(int id);
    }

    public class ExpertService : GenericService<Expert>, IExpertService {
        private readonly IUnitOfWork _unitOfWork;

        public ExpertService(IRepository<Expert> repository, IUnitOfWork unitOfWork)
            : base(repository) {
            _unitOfWork = unitOfWork;

        }




        public bool LeadNameExist(string name) {
            var user = Get(x => x.Name == name);
            return !user.Any();
        }

        public bool EmailExist(string email) {
            var user = Get(x => x.Email == email);
            return !user.Any();
        }

        public bool AddExperience(int expertId, WorkExperience workExperience) {
            if (workExperience == null) return false;
            try {
                workExperience.ExpertId = expertId;
                _unitOfWork.WorkExperiences.Add(workExperience);

                _unitOfWork.Save();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public IEnumerable<WorkExperience> GetWorkExperiences(int expertId) {
            if (expertId < 1) return null;
            var experience = _unitOfWork.WorkExperiences.Get(x => x.ExpertId == expertId);
            if (experience.Any())
                return experience;
            return null;
        }

        public int? DeleteWorkExperience(int id) {
            if (id < 1) return null;
            try {
                var experience = _unitOfWork.WorkExperiences.Get(x => x.Id == id).FirstOrDefault();
                if (experience != null) {
                    experience.DeletedAt = DateTime.Now;
                    _unitOfWork.WorkExperiences.Update(experience);
                    _unitOfWork.Save();
                    return experience.ExpertId;
                }
                return null;
            } catch (Exception ex) {
                return null;
            }
        }

        public WorkExperience GetWorkExperienceById(int id) {
            if (id < 1) return null;
            var work = _unitOfWork.WorkExperiences.Get(x => x.Id == id).FirstOrDefault();
            return work;
        }

        public int? EditWorkExperience(WorkExperience work) {
            try {
                work.CreatedAt = DateTime.Now;
                _unitOfWork.WorkExperiences.Update(work);
                _unitOfWork.Save();
                return work.ExpertId;
            } catch (Exception ex) {
                return null;
            }
        }

    }
}