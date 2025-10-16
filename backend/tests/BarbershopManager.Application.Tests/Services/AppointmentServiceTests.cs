using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Application.DTOs;
using BarbershopManager.Application.Services;
using BarbershopManager.Domain.Entities;
using Moq;
using Xunit;

namespace BarbershopManager.Application.Tests.Services;

public class AppointmentServiceTests
{
    private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock = new();
    private readonly Mock<IBarberRepository> _barberRepositoryMock = new();
    private readonly Mock<IServiceOfferingRepository> _serviceRepositoryMock = new();
    private readonly AppointmentService _sut;

    public AppointmentServiceTests()
    {
        _sut = new AppointmentService(_appointmentRepositoryMock.Object, _barberRepositoryMock.Object, _serviceRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Barber_Has_Overlap()
    {
        // Arrange
        var barberId = Guid.NewGuid();
        var serviceId = Guid.NewGuid();
        var request = new CreateAppointmentRequest(barberId, serviceId, "John Doe", new DateTime(2024, 1, 1, 9, 0, 0, DateTimeKind.Utc), 60, null);
        var existing = new Appointment(barberId, serviceId, "Jane Doe", request.ScheduledAt.AddMinutes(30), TimeSpan.FromMinutes(60));

        _barberRepositoryMock.Setup(r => r.GetByIdAsync(barberId, It.IsAny<CancellationToken>())).ReturnsAsync(new Barber("Alex", "alex@example.com"));
        _serviceRepositoryMock.Setup(r => r.GetByIdAsync(serviceId, It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceOffering("Corte", "Corte clássico", 30, TimeSpan.FromMinutes(45)));
        _appointmentRepositoryMock.Setup(r => r.GetByBarberAsync(barberId, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Appointment> { existing });

        // Act
        var act = () => _sut.CreateAsync(request);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_When_Slot_Is_Free()
    {
        // Arrange
        var barberId = Guid.NewGuid();
        var serviceId = Guid.NewGuid();
        var request = new CreateAppointmentRequest(barberId, serviceId, "John Doe", new DateTime(2024, 1, 1, 11, 0, 0, DateTimeKind.Utc), 60, "Primeira visita");
        var created = new Appointment(barberId, serviceId, request.CustomerName, request.ScheduledAt, TimeSpan.FromMinutes(request.DurationMinutes), request.Notes);

        _barberRepositoryMock.Setup(r => r.GetByIdAsync(barberId, It.IsAny<CancellationToken>())).ReturnsAsync(new Barber("Alex", "alex@example.com"));
        _serviceRepositoryMock.Setup(r => r.GetByIdAsync(serviceId, It.IsAny<CancellationToken>())).ReturnsAsync(new ServiceOffering("Corte", "Corte clássico", 30, TimeSpan.FromMinutes(45)));
        _appointmentRepositoryMock.Setup(r => r.GetByBarberAsync(barberId, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Appointment>());
        _appointmentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(created);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.Equal(request.CustomerName, result.CustomerName);
        Assert.Equal(request.BarberId, result.BarberId);
        Assert.Equal(request.ScheduledAt, result.ScheduledAt);
        _appointmentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
