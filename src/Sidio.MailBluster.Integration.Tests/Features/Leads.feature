Feature: CRUD operations for leads

@cleanupLead
Scenario: Create a lead
    Given a lead does not exist
    When the lead is created
    Then the lead should exist