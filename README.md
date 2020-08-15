# dotnet-core-event-sourcing
Project to build an example to how to create an API REST using event sourcing with dotnet-core and DynamoDB as EventStore

The idea of this project is to build a REST API that allows a CRUD of users, using event-sourcing and dyanamodb as event store. Once done, projections could be created to use a CQRS approach and add these events in a reading database, modelled according to the query requirements.

For now it contains the basic structure of event-sourcing, using domain events.

To persist the events we will use DynamoDB, using the entity ID as HashKey and the event number as RangeKey, so to reconstruct an entity we will only have to do a query by HashKey and reconstruct those events in the entity.

No validations, security, exception control, logging, unit tests, integration tests, etc. are being contemplated.

There is still a lot to do!
