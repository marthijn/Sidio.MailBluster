@fields
Feature: CRUD operations for fields

Background:
    Given a random label is created

Scenario: Create a field
    Given a field does not exist
    When the field is created
    Then the field should exist

Scenario: Update a field
    Given a field exists
    When the field is updated
    Then the field should be updated

Scenario: Delete a field
    Given a field exists
    When the field is deleted
    Then the field should not exist