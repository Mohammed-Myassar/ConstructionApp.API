using BuisnesLogic.Data_Transfer_Object;
using Domain.Entities;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using FluentValidation;
using AutoMapper;

namespace BuisnesLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository repository;
        private readonly IValidatorService<PaymentTransactionDTO> validator;
        private readonly IMapper mapper;

        public PaymentService(
            IRepository repository,
            IValidatorService<PaymentTransactionDTO> validator,
            IMapper mapper
            ) 
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }
        public async Task<PaymentTransaction> AddTransactionAsync(int projectId,
            PaymentTransactionDTO transactionDto)
        {
            // Check the validity of values
            await validator.ValidatorAsync(transactionDto);

            // Read project by Id from DataBase
            var project = await repository.ReadProjectByIdAsync(projectId);

            // Chech Project is null
            if (project == null)
                throw new InvalidOperationException($"Project wiht ID: {projectId} not found.");

            // Mapping from view model to Entity
            var transaction = mapper.Map<PaymentTransaction>(transactionDto);

            // Assign the Project Id to Entity
            transaction.ConstructionProjectId = projectId;

            // Added Entity to DataBase
            await repository.AddTransactionAsync(transaction);

            return transaction;
        }

        public async Task<PaymentTransactionDTO>
            FindPaymentTransactionById(int projectId, int transactionId)
        {
            // Read transaction by Id and Id project from DataBase
            var transaction = await repository
                .FindPaymentTransactionByIdAsync(projectId, transactionId);

            // Chech transaction is null
            if (transaction == null)
                throw new InvalidOperationException($"Transaction with ID: {transactionId} not found");

            // Mapping from Entity to view model
            var transactionDto = mapper.Map<PaymentTransactionDTO>(transaction);

            return transactionDto;
        }
    }
}
