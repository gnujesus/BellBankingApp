﻿using AutoMapper;
using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Model> : IGenericService<SaveViewModel, ViewModel, Model>
         where SaveViewModel : class
         where ViewModel : class
         where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveViewModel> Create(SaveViewModel vm)
        {
            
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.AddAsync(entity);

            SaveViewModel entityVm = _mapper.Map<SaveViewModel>(entity);

            return entityVm;
        }

        public virtual async Task Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public virtual async Task<SaveViewModel> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entityList = await _repository.GetAllAsync();

            return _mapper.Map<List<ViewModel>>(entityList);
        }

    }
}
