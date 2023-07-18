# Medical Appointment API

This API allows patients to schedule medical appointments with doctors from different specialties. The API supports functionalities such as appointment scheduling, appointment management (view, change, and cancel), and doctor's view of their patients' appointments for the day.

# Technologies Used

- NET Core
- Entity Framework
- Sql Server for database management
- Authentication and Authorization
- Moq Library for unit tests

# Getting Started

- To get started with the API locally, follow the steps below:

- Install .NET Core: Ensure that you have the .NET Core SDK installed on your machine.

- Clone the Repository: Clone the repository to your local machine.

- Create empity Database.
- Run Update-Databe commond in Infrastructue

# API Endpoints

## Patients

##### GET /Doctor: Retrieves all appointments for a specific patient.

##### POST /Doctor: Creates a new appointment for a patient.

##### PUT /Doctor: Updates an existing appointment for a patient.

##### DELETE /Doctor/{appointmentId}: Cancels an appointment for a patient.

# Doctors

##### Post /doctor Retrieves all appointments for a specific doctor on a given day.

# Schedule

##### Post /Schedule:Doctor can add his/her schedule.

# Authentication and Authorization

The API uses authentication and authorization mechanisms to secure the endpoints. You need to include an authentication token in the request headers for protected endpoints. Refer to the API documentation for details on authentication and obtaining the token.

# Testing

Unit tests are available to ensure the functionality and reliability of the API. You can run the tests using the following command:
