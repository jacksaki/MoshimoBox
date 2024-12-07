using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MoshimoBox.Models
{
    public class JsonToCSConverter
    {
        public static string ConvertJsonToCSharpClass(string json, string className = "RootClass")
        {
            var jobject = JObject.Parse(json);
            var classDeclaration = GenerateClass(className, jobject);

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")))
                .AddMembers(classDeclaration);

            return compilationUnit.NormalizeWhitespace().ToFullString();
        }

        private static ClassDeclarationSyntax GenerateClass(string className, JObject jobject)
        {
            var classDeclaration = SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            foreach (var property in jobject.Properties())
            {
                var propertyType = GetPropertyType(property.Value);
                var propertyDeclaration = SyntaxFactory.PropertyDeclaration(
                    SyntaxFactory.ParseTypeName(propertyType),
                    SyntaxFactory.Identifier(property.Name))
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                    );

                classDeclaration = classDeclaration.AddMembers(propertyDeclaration);
            }

            return classDeclaration;
        }

        private static string GetPropertyType(JToken token)
        {
            return token.Type switch
            {
                JTokenType.Integer => "int",
                JTokenType.Float => "double",
                JTokenType.String => "string",
                JTokenType.Boolean => "bool",
                JTokenType.Array => "List<object>",
                JTokenType.Object => "object",
                _ => "string"
            };
        }
    }
}
