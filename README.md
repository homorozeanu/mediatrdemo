# Collection of different examples working with MediatR

This repository was created to get some hands-on on different use-cases where MediatR can be used.
It contains following use case examples:

* Approaches: a command line app playing with 
  * request and response messages, 
  * notifications, 
  * handlers, and so on ...
* WebApi: an ASP.NET Core 3.0 WebApi using MediatR for loosly coupling controller and domain logic
  * Use MediatR Requests and RequestHandlers to decouple Controllers from BusinessLogic
  * Use Queries and Commands from Controllers to Handlers
  * Use Response objects between API-Controllers and outside world
  * Use FluentValidation to validate Commands
  * Use IPipelineBehavior to validate the Commands
  
Use branches to navigate to the final solution.  

(This repository was presented in my company as a demo for MediatR)