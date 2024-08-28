namespace Playbook.Service.Contracts;

public record PlaybookTreeResponse(
    List<DataResponse> data, 
    List<IncludedResponse> included
    );

public record DataResponse(
    string id, 
    string type, 
    AttributeResponse attributes, 
    RelationshipResponse relationships);

public record AttributeResponse(
    string nodeType, 
    int order, 
    int version, 
    string name);

public record RelationshipResponse(
    ActionResponse actions, 
    DefinitionResponse playbookDefinition);

public record ActionResponse(
    List<DataActionResponse> data);

public record DataActionResponse(
    string id, 
    string type);

public record DefinitionResponse(
    DataDefinitionResponse data);

public record DataDefinitionResponse(
    string id, 
    int type);

public record IncludedResponse(
    string id, 
    string type, 
    AttributeIncludeResponse attributes);

public record AttributeIncludeResponse(
    string name, 
    List<QueryResponse> query, 
    string queryType, 
    List<string> validationErrors);

public record QueryResponse(
    string id,
    string @operator,
    List<QueryValueResponse> values
);

public record QueryValueResponse(
    string id,
    string fieldId,
    string value,
    string @operator,
    int duration,
    string durationType
);