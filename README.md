# UmbracoRestrictions

**Umbraco Restrictions** is a pluggin for UMBRACO CMS to control and manage restrictions at the content level.

## Preamble

The Umbraco backoffice offers a wide range of options / actions for users. It is possible to build complex apps with a wide range of different document types for different use. While some documents can contain text and images, some documents can contain the list of settings for the application. 

For some specific documents it is interesting to add an extra level of security to avoid these documents to be unpublished by accident. 

## Rules

There are currently different set of rules available to lock some content: 
* **RootSingletonDocumentRestriction** : allow only one instance of the defined document type at the root level. This rule is interesting to avoid multiple published repositories of settings at the root level.

* **OnlyChildDocumentRestriction** : allow only one instance of the defined document type at any level (except root level). This rule is interesting for Umbraco-as-application projects where some document types are specific forms that can't be duplicated at a specific level.

* **LeastOnceDocumentRootRestriction** : allow at least one instance of the defined document type at the root level. This rule is interesting to ensure that at least one repository settings is available at the root level.

## Instructions

The rules and the list of document types are located in the **restrictions.config** file in the **config folder** of the Umbraco project. 

```
<?xml version="1.0"?>
<restrictionsConfig>
  <rules>
    <rule name="rootSingletonDocument" errorMessage="You can't create more than one instance of this document at the root level">
      <add docTypeAlias="globalConfiguration" />
    </rule>
    <rule name="onlyChildDocument" errorMessage="You can't create more than one instance of this document at that level">
      <add docTypeAlias="LandingPage" />
    </rule>
    <rule name="leastOnceDocumentRoot" errorMessage="At least one instance of this document must be published at the root level">
      <add docTypeAlias="globalConfiguration" />
    </rule>
  </rules>
</restrictionsConfig>
```

This library has been designed to easily integrate new rules, if you think of a good one, feel free to add it. 

## Next 

- [ ] Convert the Core project into a NuGet package
- [ ] Publish the project 
