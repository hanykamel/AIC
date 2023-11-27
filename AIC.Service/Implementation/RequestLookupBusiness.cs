using AIC.CrossCutting.ExceptionHandling;
using AIC.Data.Model;
using AIC.Data.ViewModels.Lookups;
using AIC.Repository;
using AIC.Service.Entities.CommonActions.RequestsLookup;
using AIC.Service.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Implementation
{
    public class RequestLookupBusiness : IRequestLookupBusiness
    {
        readonly private IRepository<DegreeLevel, int> _degreeLevelRepository;
        readonly private IRepository<JobTypes, int> _jobTypesRepository;
        private readonly IMapper _mapper;

        public RequestLookupBusiness(IRepository<DegreeLevel, int> degreeLevelRepository,
                                     IRepository<JobTypes, int> jobTypesRepository,
                                     IMapper mapper)
        {
            _degreeLevelRepository = degreeLevelRepository;
            _jobTypesRepository = jobTypesRepository;
            _mapper = mapper;   
        }

        public List<LookupsViewModel> ListDegreeLevel(ListDegreeLevelCommand degreeLevelCommand)
        {
            var result = _degreeLevelRepository.GetAll().ToList();
            if (result == null)
                throw new NotFoundException("NoLookupsFound");
            var mappedResult = _mapper.Map<List<LookupsViewModel>>(result);
            return mappedResult;
            //return new LookupsViewModelList { List = mappedResult, TotalCount = result.Count() };
        }

        public List<LookupsViewModel> ListJobTypes(ListJobTypesCommand jobTypesCommand)
        {
            var result = _jobTypesRepository.GetAll().ToList();
            if (result == null)
                throw new NotFoundException("NoLookupsFound");
            var mappedResult = _mapper.Map<List<LookupsViewModel>>(result);
            return mappedResult;
            //return new LookupsViewModelList { List = mappedResult, TotalCount = result.Count() };
        }
    }
}
