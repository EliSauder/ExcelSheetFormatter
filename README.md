# ExcelSheetFormatter

This project was initially designed to format Bill of Materials into a format required by other software, however it can be used for formatting any tabular Excel data into another format.

# Configuration Information

There is currently no GUI for configuring the formatting rules and must all be done manually. The configuration utilizes XML.

## Configuration File Location

### Windows

The configuration file is located at `%appdata%\BomFormatter` 
Normal Full Path: `C:\Users\USERNAME\AppData\Roaming\BomFormatter`

## Default File Content

The default configuration contains configurations for translating from Microsoft Dynamix AX's BOM to Panasonic Process Tracker's BOM format and translating from Oracle Agile's BOM format to Panasonic process Tracker's BOM format.

## Format
```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<applicationConfiguration>
  <boms>
    <bom name="" outputType="INDIVIDUAL" displayName="" outputSheetName="" enabled=true numberOfRowsToSkip=0 inputFileExtention=".xlsx">
      <fields>
        <field name="" identifierOrder=-1 splitInto=-1 isQuantity=false dataType="System.String" delimiter="" isSplit=false header="" output="" order=0 override=false enabled=false required=true>
          <populations>
            <population name="" condition="EQUALS" findValue="" setValue="" toColumn="" active=true />
          </populations>
          <cleanupActions>
            <cleanup name="" action="UPPERCASE" report=true active=true scope="CELL" condition="EQUALS" value="" />
          </cleanupActions>
        </field>
      </fields>
    </bom>
  </boms>
<parsing productRegex="" />
<directories rootDirectory="" inputFolder="" outputFolder="" oldInputFolder="" logFolder="" />
</applicationConfiguration> 
```

## Rule Execution Order

1. Load all information from input and create output table (all values and rules defined in the different fields).
2. Perform populations (for each of the columns, see if there are any population rules and perform them).
3. Perform cleanup (for each of the columns, see if there are any cleanup rules and perform them).

## Section Information

### ApplicationConfiguration
This section is responsible for storing all the other sections.
#### XML Tag
```xml
<applicationConfiguration>
    .
    .
    .
</applicationConfiguration>
```
#### Properties
There are no properties for this section.
#### Sections
- Boms
- Parsing
- Directories

---

### Boms
This section stores the list of all output formats (and their input requriements). Again, it is using the Bom term but it can be used for any excel table.
#### XML Tag
```xml
<boms>
    .
    .
    .
</boms>
```
#### Properties
There are no properties for this section.
#### Sections
- Bom

---

### Bom
This section stores all the information relating to an individual sheet. The fields, per-configuration properties, etc.
#### XML Tag
```xml
<bom name="" outputType="" displayName="" outputSheetName="" enabled=true numberOfRowsToSkip=0 inputFileExtention=".xlsx">
    .
    .
    .
</bom>
```
#### Properties
- name - This is the unique name for each bom.
    - Required
    - Unique
    - Possible Values
        - Any String
- outputType - This defines the behavior for split columns. If it is defined as individual, it will create new rows for any values it finds in a split column. For example, if you have a column split on commas that contains the info `5,6,7`, it will be converted into 3 different rows. If it is defined as grouped it will leave the input alone and not split values into different rows (grouped does not combine values, just leaves them alone).
    - Required
    - Possible Values
        - "INDIVIDUAL"
        - "GROUPED"
- displayName - This defines the name that will be displayed inside of the application.
    - Optional
    - Possible Values
        - Any String
    - Default Value
        - "" (Empty String)
- outputSheetName - Defines the name of the formatted sheet in the output file.
    - Optional
    - Possible Values
        - Any String
    - Default Value
        - "" (Empty String) - This indicates that the software will use the default naming scheme from Excel of Sheet#.
- enabled - Indicates whether the configuration should be displayed in the UI.
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default Value
        - true
- numberOfRowsToSkip - Defines the number of rows that the script should ignore before reading the data from the input sheet (e.g. if you define 0 it will start reading from row 1, if you define 10 it will start reading from row 11).
    - Optional
    - Possible Values
        - Any Integer >= 0. Range: \[0, ∞\)
    - Default Value
        - 0
- inputFileExtention - The file extension used for input files.
    - Required
    - Possible Values:
        - ".xlsx"
        - ".csv"
        - ".xls"
    - Default Value
        - ".xlsx"

#### Sections
- Fields

---

### Fields
This section contains all the fields that will be output to the output sheet as well as their source columns and any other properties/actions.
#### XML Tag
```xml
<fields>
    .
    .
    .
</fields>
```
#### Properties
There are no properties for this section.
#### Sections
- Field

---

### Field
This section contains all the information for an individual field.
#### XML Tag
```xml
<field name="" identifierOrder=-1 splitInto=-1 isQuantity=false dataType="System.String" delimiter="" isSplit=false header="" output="" order=0 override=false enabled=false required=true>
    .
    .
    .
</field>
```
#### Properties
- name - The unique name for this column
    - Required
    - Unique
    - Possible Values
        - Any String
- identifierOrder - The order in which this column appears when identifying information in an output. If left blank (set to default value), the column will not be included in the string summarizing the row in reports. For example, if you define a product number field, a part number field, and a quantity field in that order, it will output as Product Number – Part Number – Quantity.
    - Optional
    - Possible Values
        - Any Integer >= -1. Range: \[-1, ∞\)
    - Default Value
        - -1 (Indicates that it will not be used in the identifier)
- splitInto - A pre-defined number of times to split a cell into different rows. This will take the value of the cell, convert it to a string and then split it the defined number of times creating a new row for each split. If not defined (Set to the default value), this field will be ignored.\
This requires isSplit to be set to true.
    - Optional
    - Possible Values:
        - Any Integer >= -1. Range: \[-1, ∞\)
    - Default Value
        - -1 (Indicates that this feature will not be used)
- isQuantity - Sets whether this column should be updated appropriately should any splits occur. This field must contain a numeric value in the input sheet.\
For example, if the field's value is 7 and another cell is split into 5 different rows, the value of this field will be 1 for the new rows.
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default Value
        - false
- dataType - Sets the Microsoft .net type (https://docs.microsoft.com/en-us/dotnet/api/system.type)
    - Required
    - Possible Values
        - See https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types
    - Default Value
        - System.String
- delimiter - This defines the value to find in between other values to split them apart. The delimiter is defined as a Regular Expression (Regex for short). If all you are doing is defining a string to look for, no special syntax is needed. \
For example, if you have `5,6,7` and want to split on the comma, the delimiter can just be `","`. \
This requires isSplit to be set to true.
    - Optional
    - Possible Values
        - Any Valid regex string
    - Default Value
        - "" (Empty String)
- isSplit - Determines whether this column has values that should be split. The two types of splitting are delimiter splitting and split into a number. *If a delimter is defined splitInto is ignored.*
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default value
        - false
- header - This is the name of the column from the input sheet from which, all the values will e copied from.
    - Required
    - Possible Values
        - Any String
- output - This is the column name that will be used in the output, if nothing is specified it will use the value from header.
    - Optional
    - Possible Values
        - Any String
    - Default Value
        - "" (Empty String)
- order - The order in which the column will appear in the output sheet. More than one field can have the same order, if this is the case the columns will be combined. If no override is defined, the column that appears in the input first will take highest precedence.
    - Required
    - Possible Values
        - Any Integer >= 0, Range: \[0, ∞\)
- override - Whether this column will override the values of any other column defined with the same order. *Note: Behavior when two columns with the same order are both set to override is not defined. It will work, but results may vary.*
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default Value
        - false
- enabled - Whether this column should be evaluated when processing the input sheet.
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default Value
        - false
- required - Whether this column is requried to be in the input or not. If set to false, the column will be still be created in the output sheet regardless of its pressense in the input. When defined as true, it will error out if the column is not provided from the input.
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default Value
        - true
#### Sections
- Populations - Rules for populating columns after loading data.
- Cleanup Actions - Rules for delaning up/reporting data in the sheet.

---

### Populations
A list of data insertion rules that will be performed on the loaded data.
#### XML Tag
```xml
<populations>
    .
    .
    .
</populations>
```
#### Properties
There are no properties for this section.
#### Sections
- Population

---

### Population
Defines a rule on how to populate data.
#### XML Tag
```xml
<population name="" condition="EQUALS" findValue="" setValue="" toColumn="" active=true />
```
#### Properties
- name - A unique name to define the population rule.
    - Required
    - Possible Values
        - Any String
- condition - The condition to match that triggers the rule
    - Optional
    - Possible Values
        - "BEGINS_WITH" - Checks if the string begins with the search value.
        - "ENDS_WITH" - Checks if the string ends with the search value.
        - "EQUALS" - Checks if the string equals the search value.
        - "CONTAINS" - Checks if the string contains the search value.
        - "ANY" - Always performs the action.
        - "MATCHES" - Checks if the string matches the regex stored in search value.
        - "ISNUMBER" - Checks if the string is a string (ignores the search value).
        - "ISINT" - Checks if the string is an integer or not (ignores the search value).
        - "ISFLOAT" - Checks if the number is a floating-point number (ignores the search value).
        - "ISEMPTY" - Checks if the value is empty (ignores the search value).
    - Default Value
        - "EQUALS"
- findValue - The value used in the condition. This is ignored by some of the conditions.
    - Optional
    - Possible Values
        - Any String
        - If the condition is "MATCHES" it must be a valid Regular Expression (Regex).
    - Default Value
        - "" (Empty String)
- setValue - The value to put in the column if the condition is met.
    - Required
    - Possible Values
        - Any String
- toColumn - The column that setValue will be applied to for the current row.
    - Required
    - Possible Values
        - Any valid field name (references field.name)
- active - Whether the population rule should be performed or not.
    - Possible Values
        - Boolean (true, false)
    - Default Value
        - true
#### Sections
- None

---

### Cleanup Actions
Defines a list of rules and actions to performed on the loaded data.
#### XML Tag
```xml
<cleanupActions>
    .
    .
    .
</cleanupActions>
```
#### Properties 
There are no properties for this section.
#### Sections
- Cleanup

---

### Cleanup Action
#### XML Tag
```xml
<cleanup name="" action="" report=true active=true scope="" condition="" value="" />
```
#### Properties
- name - A unique name for the cleanup rule.
    - Required
    - Possible Values
        - Any String
- action - The action to be perfomed when the rule is triggered. *All values are reported*
    - Required
    - Possible Values
        - REMOVE - If the scope is ROW it will remove the row, if the scope is CELL it will set the value of the cell to "" (Empty String).
        - UPPERCASE - Sets all strings within the scope to uppercase.
        - LOWERCASE - Sets all strings within the scope to lowercase.
        - REPORT - Only reports the value to the user.
- report - Whether the action should be expanded by default in the report.
    - Optional
    - Possible Values
        - Boolean (true, false)
    - Default Values
        - true
- active - Whether this action should be performed or not.
    - Optional
    - Possible Values
        Boolean (true, false)
    - Default Value
        - true
- scope - The scope of the action.
    - Required
    - Possible Values
        - ROW - Action will be performed for the entire row.
        - CELL - Actio will be performed for only the current cell.
- condition - The condition that triggers this action.
    - Required.
    - Possible Values
        - "BEGINS_WITH" - Checks if the string begins with the search value.
        - "ENDS_WITH" - Checks if the string ends with the search value.
        - "EQUALS" - Checks if the string equals the search value.
        - "CONTAINS" - Checks if the string contains the search value.
        - "ANY" - Always performs the action.
        - "MATCHES" - Checks if the string matches the regex stored in search value.
        - "ISNUMBER" - Checks if the string is a string (ignores the search value).
        - "ISINT" - Checks if the string is an integer or not (ignores the search value).
        - "ISFLOAT" - Checks if the number is a floating-point number (ignores the search value).
        - "ISEMPTY" - Checks if the value is empty (ignores the search value).
- value - The search value to use in the trigger for the action.
    - Possible Value
        - Any String
        - If the condition is MATCHES the value must be a valid regex.
    - Default Value
        - "" (Empty String)
#### Sections
- None

---

### Parsing
Defines the validation for the "Enter Product Number" field in the user interface as a regular expression. **Enter Product Number is just the file name (no extention), nothing else.**
#### XML Tag
```xml
<parsing productRegex="" />
```
#### Properties
- productRegex - The Regular Expression (Regex) to use for parsing the product.
    - Required
    - Possible Values
        - Any valid Regular Expression (Regex)
#### Sections
- None

---
### Directories
Defines the directories where the application will read and output files. **This *can* be configured in the user interface**.
#### XML Tag
```xml
<directories rootDirectory="" inputFolder="" outputFolder="" oldInputFolder="" logFolder="" />
```
#### Properties
- rootDirectory - The root folder that will contain all other folders and any files.
    - Required
    - Possible Values
        - Any valid file path
- inputFolder - The folder within the root folder that contains all the input files. \
This can be blank. If left blank, the application will look for all input in the rootDirectory.
    - Optional
    - Possible Values
        - Any valid relative file path.
    - Default Value
        - "" (Empty String)
- outputFolder - The folder within the root folder that the applicatio nwill output files to. THis can be blank. If left blank, the application will output in the rootDirectory.
    - Optional
    - Possible Values
        - Any valid relative file path.
    - Default Value
        - "" (Empty String)
- oldInputFolder - Not used.
- logFolder - Not used.
