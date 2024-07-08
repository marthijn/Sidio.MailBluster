Feature: CRUD operations for fields

@ignore
@cleanupField
Scenario: Create a field
    Given a field does not exist
    When the field is created
    Then the field should exist