Feature: CRUD operations for leads

@cleanupLead
Scenario: Create a lead
    Given a lead does not exist
    When the lead is created
    Then the lead should exist

@createLead
@cleanupLead
Scenario: Update a lead
    Given a lead exists
    When the lead is updated
    Then the lead should be updated

@createLead
Scenario: Delete a lead
    Given a lead exists
    When the lead is deleted
    Then the lead should not exist