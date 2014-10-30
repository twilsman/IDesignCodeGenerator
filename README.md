IDesignCodeGenerator
====================

A utility for generating the scaffolding of a project using the IDesign architectural pattern.

This utility is intended to remove some of the tedium from creating and updating the data access layer (DAL) of an IDesign patterned solution.  It will scan the schema of a given set of SQL database tables, and generate the following code files:

* DataContracts - Each table will generate a class file with the appropriately typed fields.
* DatabaseAccessors - Each table will generate an accessor file that inherits from a specified base class.
* Contracts - Each accessor will also have its corresponding interfact/service contracted generated.
* UnitTests - Each access will have a test class generated with stubs for the appropriate test methods.

### Configuration
The following customization options are provided in the app.config file:  
* BaseNamespace - For example if `BaseNamespace = NG.Project`, then all DatabaseAccessors will have a namespace of `NG.Project.ResourceAccess`  
* TargetDbConnectionStringName - This is the name of the connection string that will be used to connect to the database being used to generate the code files.  
* AccessorsPath - This is the path where the following generated files will be saved:  
  * TableBaseAccessor.cs (if generated)
  * DbBase.cs 
  * DbContext.cs (Entity Framework Fluent API declarations)
  * Individual [Table]Accessor.cs files  
* DbContextClassName - Customize the name of the DbContext class.  
* BaseAccessorClassName - The name of the base class individual accessors will inherit from.  
* CreateBaseAccessorClass - Boolean.  If a base accessor already exists, set this to false.  
* ContractsPath - The path where generated interfaces/service contracts will be saved.  
* UnitTestPath - The path where generated unittest files will be saved.  
* IncludedTables - This subsection should contain an entry for each table to generate classes for.  Example:  
  * `<add schema="dbo" name="MyTable" >`

### Usage

To use this utility

1. open the solution in VisualStudio
2. Set all of the configuration options
3. Make sure the `1.Clients\DALGenerator` project is set as the startup project
4. Hit F5 :)

This utility does not add files to a VisualStudio project or solution.  It can publish class files directly into a project, but they will need to be added manually with `Add -> Existing Item...`.

Rather than inserting fluent api declarations into the DbContext class, the utility generates a new one.  These declarations should be pasted into an existing DbContext manually.

Also, the translation of table names to class names is rock-stupid, so some manually renaming may need to be done.

### Templating

All code is generated using Razor templates in .cshtml files.  These can be found and modified in the `4.Shared\Templates\CodeGeneration` project directory.

All template files names are set by default, but they can be overidden with the following configuration properties:

* DatabaseAccessorTemplateFile
* DatabaseAccessorTestBaseTemplateFile
* DatabaseAccessorUnitTestTemplateFile
* DataContractTemplateFile
* DbBaseTemplateFile
* DbContextTemplateFile
* IDatabaseAccessorTemplateFile
* ITableBaseAccessorTemplateFile
* TableBaseAccessorTemplateFile

The location of the template files can be changed by setting the `TemplatesPath` config property.  The default templates directory is populated by a pre-build event on the 1.Clients\DALGenerator project.
