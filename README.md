# TicketingSystem

## Features

This project brings a simple implementation of a ticketing system to report problems and assign them to some user to be attended.

The system offers a login form on the starting page and the option to register a new user of the system.
The new user must be enabled by a system administrator before start using the application.

After being enabled, the user can access to the ticket dashboard where it's enabled to create new tickets, edit and delete them.
The dashboard shows by default all the tickets assigned to the user and in Open status.
The user can enter a text as filter for the tickets showed or switch to see the Closed ones.

## Solution

### Arquitecture

The application was develop using C# as main languaje, in a MVC project. 

At the start of the design of the architecure of the solution, one option was develop a backend in C# and a frontend application in React or Angulat. 
But later, realizing the short ammount of pages and the few interactions that will have the user on the page, that approach was discarted.

Anyway, the solution was structuted to allow a easy migration to that architecture. 
The controllers have only calls to the coordinators and logic about the displayment of the views.
The coordinators have the responsability to access the data layer, throught the Entity Framework Model class. So, the coordinators exposes the allowed actions of the system.
At the end we have the data layer represented by the model classes.
So, It will be easy to expose those features with Web API controllers.

### Authentication & Security

The authentication of the users was done using a simple login form with email and password. This information is sended to the server were it's validated.
First the plain password is encrypted using a hash algorithm, and the result is compared with the stored encrypted password. 
Only if they match, and the user was enabled by an admin, the user will access to the dashboard.
If one of those conditions were not truth, the system will display a "Sorry", and the user will not see the dashboard.

When the user logins in the system, an authorization cookie is sent in the response. This cookie is used in the subsequent requests. 
If this cookie is not present or it expired in a request to an action under a [Authorize], the system will redirect the user to the login form.

One possible change will be delegate the authentication to an external system like Auth0, or use Facebook or Google as logins.

### Database version control

The solution uses Entity Framework Code First approach to versionate the database changes in migrations.

### Client-Side Scripts

The current features required by the system only requires to make a simple script to filter the dashboard results dynamically.
This JavaScript contains a function that is called each time the user interacts with the search input field or the status filter.
The function takes the current filters and iterate the tickets of the dashboard to determine which of them will be displayed or not.
This was done intentionally to save request to the server. 
One important point is that the closed tickets are loaded in the view but with the style "display: none;", so, the user doesn't seen them if not change the default status filter of Open.

### Admin Page

The administrator are allowed to access to a special page where they can change which users are enabled or not to access to the system.
By default, this is the first page to be displayed when an administrator access to the system, after that the administrator can navegate to the dashboard.

To validate if a non administrator user tries to access to the admin page, an action filter attribute was implemented.
This filter validates that the current user is an administrator. If the user is not one, the system redirect the user to the dashboard.

### Dependency Injection

StructureMap was selected as IoC/DI container. The project follows the naming convenctions, so no additional configuration was required.

### Unit Testing

In other project, under the same solution, were developed the test cases of the main actions of the system.
This project uses NUnit as testing framework and Moq to create the fakes/stubs of the dependencies of the components.

Some basics test cases were implemented for the controller since they delegate the processing to the coordinators. 
So, thay mainly validates if the controller methods response with a view or a redirect. 
In those tests cases, the coordinator classes were faked.

The coordinators have the logic against the data layer, so, they were tested to validate the calls against the database context object were made according to each method.

About the model classes, only the user class has a testeable method, the one that encrypts the password.

In general, the test cases will be improved to try reach a greater coverage of the code.


