using System.Collections.Concurrent;
using Grpc.Core;

namespace HappyCoding.GrpcServerWithReflection.Services;

public class SampleService : HappyCoding.GrpcServerWithReflection.SampleService.SampleServiceBase
{
    private static readonly ConcurrentBag<Person> s_people = new();

    public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloResponse { Message = $"Hello {request.Name}" });
    }

    public override Task<AddPersonResponse> AddPerson(AddPersonRequest request, ServerCallContext context)
    {
        // Validation
        if (request.Person == null)
        {
            return Task.FromResult(new AddPersonResponse()
            {
                Successful = false,
                Error = AddPersonError.AddPersonResultInvalidData
            });
        }
        
        var doesPersonExist = s_people.Any(x => x.Id == request.Person.Id);
        if (doesPersonExist)
        {
            return Task.FromResult(new AddPersonResponse()
            {
                Successful = false,
                Error = AddPersonError.AddPersonResultAlreadyExists
            });
        }

        // Execute
        s_people.Add(request.Person);
        
        return Task.FromResult(new AddPersonResponse()
        {
            Successful = true,
            AddedPerson = request.Person
        });
    }

    public override Task<GetPersonsResponse> GetPersons(GetPersonsRequest request, ServerCallContext context)
    {
        var response = new GetPersonsResponse();
        response.Persons.AddRange(s_people);
        return Task.FromResult(response);
    }
}