using System.Collections.Immutable;

namespace Cesium.Ast;

// 6.7 Declarations
public record Declaration(
    ImmutableArray<IDeclarationSpecifier> Specifiers,
    ImmutableArray<InitDeclarator>? InitDeclarators) : IBlockItem;

public record InitDeclarator(Declarator Declarator, Initializer? Initializer = null);

public interface IDeclarationSpecifier { }
// 6.7.2 Type specifiers
public record TypeSpecifier(string TypeName) : IDeclarationSpecifier;

// 6.7.3 Type qualifiers
public record TypeQualifier(string Name) : IDeclarationSpecifier;

// 6.7.7 Type names
public record AbstractDeclarator(Pointer? Pointer = null, IDirectAbstractDeclarator? DirectAbstractDeclarator = null);
public interface IDirectAbstractDeclarator
{
    IDirectAbstractDeclarator? Base { get; }
}
public record SimpleDirectAbstractDeclarator(AbstractDeclarator Declarator) : IDirectAbstractDeclarator
{
    public IDirectAbstractDeclarator? Base => null;
};
public record ArrayDirectAbstractDeclarator(
    IDirectAbstractDeclarator? Base,
    ImmutableArray<TypeQualifier>? TypeQualifiers,
    Expression? Size) : IDirectAbstractDeclarator;

// 6.7.6 Declarators
public record Declarator(Pointer? Pointer, IDirectDeclarator DirectDeclarator);
public interface IDirectDeclarator
{
    IDirectDeclarator? Base { get; }
}
public record IdentifierDirectDeclarator(string Identifier) : IDirectDeclarator
{
    public IDirectDeclarator? Base => null;
}
public record ArrayDirectDeclarator(
    IDirectDeclarator Base,
    ImmutableArray<TypeQualifier>? TypeQualifiers,
    Expression? Size) : IDirectDeclarator;
public record ParameterListDirectDeclarator(IDirectDeclarator Base, ParameterTypeList Parameters) : IDirectDeclarator;
public record IdentifierListDirectDeclarator(
    IDirectDeclarator Base,
    ImmutableArray<string>? Identifiers) : IDirectDeclarator;

public record Pointer(ImmutableArray<TypeQualifier>? TypeQualifiers = null, Pointer? ChildPointer = null);

public record ParameterTypeList(ImmutableArray<ParameterDeclaration> Parameters, bool IsVararg = false);

public record ParameterDeclaration(
    ImmutableArray<IDeclarationSpecifier> Specifiers,
    Declarator? Declarator = null,
    AbstractDeclarator? AbstractDeclarator = null);

// 6.7.9 Initialization
public abstract record Initializer;
public record AssignmentInitializer(Expression Expression) : Initializer;

// CLI extensions
public record CliImportSpecifier(string MemberName) : IDeclarationSpecifier;
