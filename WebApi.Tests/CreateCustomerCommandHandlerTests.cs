using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Auditing;
using WebApi.Commands;
using WebApi.Entities;
using WebApi.Handlers;
using WebApi.Repositories;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Tests
{
	[TestClass]
	public class CreateCustomerCommandHandlerTests
	{
		private Mock<IAudit> CreateAuditMock => new Mock<IAudit>();
		private Mock<IMapper> CreateMapperMock => new Mock<IMapper>();
		private Mock<ICustomerRepository> CreateCustomerRepositoryMock => new Mock<ICustomerRepository>();

		[TestMethod]
		public void Verify_Audit_Is_Being_Called()
		{
			var auditMock = CreateAuditMock;
			var mapperMock = CreateMapperMock;
			var customerRepositoryMock = CreateCustomerRepositoryMock;

			CancellationTokenSource source = new CancellationTokenSource();
			CancellationToken token = source.Token;
			var request = new CreateCustomerCommand(new CreateCustomerRequest()
			{
				FullName = "Customer A"
			});

			auditMock.Setup(x => x.NewUserCreatedRequest(request.Customer)).Returns(Task.FromResult(42));

			var command = new CreateCustomerCommandHandler(
				customerRepositoryMock.Object, auditMock.Object, mapperMock.Object);
			var result = command.Handle(request, token).Result;

			auditMock.Verify(x => x.NewUserCreatedRequest(request.Customer), Times.Exactly(1));
		}

		[TestMethod]
		public void Verify_Mapper_Is_Being_Called_OnRequestCustomer()
		{
			var auditMock = CreateAuditMock;
			var mapperMock = CreateMapperMock;
			var customerRepositoryMock = CreateCustomerRepositoryMock;

			CancellationTokenSource source = new CancellationTokenSource();
			CancellationToken token = source.Token;
			var request = new CreateCustomerCommand(new CreateCustomerRequest()
			{
				FullName = "Customer A"
			});
			var customer = new Customer()
			{
				Name = request.Customer.FullName
			};

			mapperMock.Setup(x => x.Map<Customer>(request.Customer)).Returns(customer);

			var command = new CreateCustomerCommandHandler(
				customerRepositoryMock.Object, auditMock.Object, mapperMock.Object);
			var result = command.Handle(request, token).Result;

			mapperMock.Verify(x=> x.Map<Customer>(request.Customer), Times.Exactly(1));
		}

		[TestMethod]
		public void Verify_Repository_Is_Being_Called()
		{
			var auditMock = CreateAuditMock;
			var mapperMock = CreateMapperMock;
			var customerRepositoryMock = CreateCustomerRepositoryMock;

			CancellationTokenSource source = new CancellationTokenSource();
			CancellationToken token = source.Token;
			var request = new CreateCustomerCommand(new CreateCustomerRequest()
			{
				FullName = "Customer A"
			});
			var customer = new Customer()
			{
				Name = request.Customer.FullName
			};
			var addedCustomer = new Customer()
			{
				Name = request.Customer.FullName
			};

			mapperMock.Setup(x => x.Map<Customer>(request.Customer)).Returns(customer);
			customerRepositoryMock.Setup(x => x.CreateCustomerAsync(customer)).Returns(Task.FromResult(addedCustomer));

			var command = new CreateCustomerCommandHandler(
				customerRepositoryMock.Object, auditMock.Object, mapperMock.Object);
			var result = command.Handle(request, token).Result;

			customerRepositoryMock.Verify(x => x.CreateCustomerAsync(customer), Times.Exactly(1));
		}

		[TestMethod]
		public void Verify_Mapper_Is_Being_Called_OnResponseCustomer()
		{
			var auditMock = CreateAuditMock;
			var mapperMock = CreateMapperMock;
			var customerRepositoryMock = CreateCustomerRepositoryMock;

			CancellationTokenSource source = new CancellationTokenSource();
			CancellationToken token = source.Token;
			var request = new CreateCustomerCommand(new CreateCustomerRequest()
			{
				FullName = "Customer A"
			});
			var customer = new Customer()
			{
				Name = request.Customer.FullName
			};
			var addedCustomer = new Customer()
			{
				Name = request.Customer.FullName
			};
			var customerResponse = new CustomerResponse()
			{
				Id = Guid.NewGuid(),
				FullName = addedCustomer.Name
			};

			mapperMock.Setup(x => x.Map<Customer>(request.Customer)).Returns(customer);
			customerRepositoryMock.Setup(x => x.CreateCustomerAsync(customer)).Returns(Task.FromResult(addedCustomer));
			mapperMock.Setup(x => x.Map<CustomerResponse>(addedCustomer)).Returns(customerResponse);

			var command = new CreateCustomerCommandHandler(
				customerRepositoryMock.Object, auditMock.Object, mapperMock.Object);
			var result = command.Handle(request, token).Result;

			mapperMock.Verify(x=> x.Map<CustomerResponse>(addedCustomer), Times.Exactly(1));
		}
	}

}
