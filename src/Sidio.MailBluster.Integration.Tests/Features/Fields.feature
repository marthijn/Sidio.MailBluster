Feature: CRUD operations for fields

@cleanupField
Scenario: Create a field
    Given a field does not exist
    When the field is created
    Then the field should exist

@createField
@cleanupField
Scenario: Update a field
    Given a field exists
    When the field is updated
    Then the field should be updated

@createField
Scenario: Delete a field
    Given a field exists
    When the field is deleted
    Then the field should not exist